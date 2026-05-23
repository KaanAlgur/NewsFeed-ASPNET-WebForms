using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Text;
using System.Web.UI;
using NLog;

public partial class Home : System.Web.UI.Page
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadNews();
        }
    }

    private void LoadNews()
    {
        string connStr = ConfigurationManager.ConnectionStrings["NewsDBConn"].ConnectionString;
        Dictionary<string, StringBuilder> categoryHtml = new Dictionary<string, StringBuilder>();
        ArrayList newsList = new ArrayList();

        try
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                string query = "SELECT NewsID, Title, Description, Category, Author, ImageUrl, PubDate FROM News ORDER BY Category, PubDate DESC";
                
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["NewsID"]);
                            string title = reader["Title"].ToString();
                            string category = reader["Category"].ToString();
                            string desc = reader["Description"].ToString();
                            string author = reader["Author"].ToString();
                            string imageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : "";
                            string pubDateStr = reader["PubDate"] != DBNull.Value ? reader["PubDate"].ToString() : "";
                            
                            News newsObj = new News(id, title, desc, category, author, pubDateStr, imageUrl);
                            newsList.Add(newsObj);
                        }
                    }
                }
            }

            // Loop over ArrayList to display grouped by category
            Dictionary<string, int> categoryCounts = new Dictionary<string, int>();

            foreach (News n in newsList)
            {
                string category = n.Category;
                
                if (!categoryHtml.ContainsKey(category))
                {
                    categoryHtml[category] = new StringBuilder();
                    categoryCounts[category] = 0;
                }

                if (categoryCounts[category] < 20)
                {
                    // Clean HTML from description for UI display
                    string cleanDesc = System.Text.RegularExpressions.Regex.Replace(n.Description, "<.*?>", string.Empty);
                    
                    DateTime parsedDate;
                    string dateStr = "";
                    if (DateTime.TryParse(n.PubDate, out parsedDate))
                    {
                        dateStr = parsedDate.ToString("MMM dd, yyyy");
                    }
                    
                    bool isFeatured = categoryCounts[category] == 0;
                    string cardClass = isFeatured ? "news-card featured" : "news-card";
                    
                    string imgWrap;
                    if (!string.IsNullOrEmpty(n.ImageUrl))
                    {
                        imgWrap = string.Format("<div class=\"card-img-wrap\"><img src=\"{0}\" alt=\"{1}\" /></div>", n.ImageUrl, n.Title.Replace("\"", "&quot;"));
                    }
                    else
                    {
                        imgWrap = isFeatured ? "<div class=\"card-img-wrap\">&#128240;</div>" : "<div class=\"card-img-wrap\" style=\"aspect-ratio:16/8;font-size:36px;\">&#128240;</div>";
                    }
                    
                    categoryHtml[category].Append(string.Format(@"
                        <a class=""{0}"" href=""NewsDetail.aspx?id={1}"">
                            {2}
                            <div class=""card-cat"">{3}{4}</div>
                            <div class=""card-title"">{5}</div>
                            <div class=""card-desc"">{6}</div>
                            <div class=""card-byline""><span>&#9997;&#65039; {7}</span><span>&#128197; {8}</span></div>
                        </a>", 
                        cardClass, n.NewsID, imgWrap, category, isFeatured ? " &middot; Featured" : "", n.Title, cleanDesc, n.Author, dateStr));
                    
                    categoryCounts[category]++;
                }
            }

            StringBuilder tabsHtml = new StringBuilder();
            StringBuilder sectionsHtml = new StringBuilder();
            
            int index = 0;
            foreach (var kvp in categoryHtml)
            {
                string activeClass = index == 0 ? " active" : "";
                string visibleClass = index == 0 ? " visible" : "";
                
                tabsHtml.Append(string.Format("<button type=\"button\" class=\"cat-tab{0}\" onclick=\"showCat({1}, this)\">{2}</button>", activeClass, index, kvp.Key));
                
                sectionsHtml.Append(string.Format("<div class=\"news-section{0}\" id=\"cat-{1}\">", visibleClass, index));
                sectionsHtml.Append(string.Format("<div style=\"padding:40px 48px 0\"><div class=\"section-label\">{0}</div></div>", kvp.Key));
                sectionsHtml.Append("<div style=\"padding:0 48px 60px\"><div class=\"news-layout\">");
                sectionsHtml.Append(kvp.Value.ToString());
                sectionsHtml.Append("</div></div></div>");
                
                index++;
            }

            if (categoryHtml.Count == 0)
            {
                sectionsHtml.Append("<div style=\"padding:40px 48px;\"><p>No news found. Please refresh feed.</p></div>");
            }

            catTabsContainer.InnerHtml = tabsHtml.ToString();
            newsContainer.InnerHtml = sectionsHtml.ToString();
        }
        catch (Exception ex)
        {
            lblError.Text = "An error occurred while loading the news. Please check logs.";
            logger.Error(ex, "Error loading news from database in Home.aspx");
        }
    }
}

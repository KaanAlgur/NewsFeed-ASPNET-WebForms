using System;
using System.Configuration;
using System.Data.OleDb;
using NLog;

public partial class NewsDetail : System.Web.UI.Page
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string idParam = Request.QueryString["id"];
            int id;
            if (!string.IsNullOrEmpty(idParam) && int.TryParse(idParam, out id))
            {
                LoadNewsDetail(id);
            }
            else
            {
                lblError.Text = "Invalid News ID.";
            }
        }
    }

    private void LoadNewsDetail(int id)
    {
        string connStr = ConfigurationManager.ConnectionStrings["NewsDBConn"].ConnectionString;

        try
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                string query = "SELECT Title, Description, Category, Author, ImageUrl, PubDate FROM News WHERE NewsID = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("?", id);
                    conn.Open();

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            contentPanel.Visible = true;
                            lblTitle.InnerText = reader["Title"].ToString();
                            
                            // Description usually contains HTML. If it is too short, we can't do much because RSS only gives summary.
                            lblDescription.InnerHtml = string.Format("<p>{0}</p>", reader["Description"].ToString());
                            
                            string imageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : "";
                            
                            // To match the UI, we inject the image into hero
                            if (!string.IsNullOrEmpty(imageUrl))
                            {
                                heroEmoji.InnerHtml = string.Format("<img src=\"{0}\" alt=\"News Image\" style=\"width:100%;height:100%;object-fit:cover;\"/>", imageUrl);
                            }
                            else
                            {
                                heroEmoji.InnerHtml = "&#128240;"; // newspaper emoji if no image
                            }
                            
                            string category = reader["Category"].ToString();
                            lblKicker.InnerText = category;
                            lblCategory.InnerHtml = string.Format("&#127991;&#65039; <strong>{0}</strong>", category);
                            
                            if (reader["PubDate"] != DBNull.Value)
                            {
                                DateTime parsedDate;
                                string pubDateStr = reader["PubDate"].ToString();
                                if (DateTime.TryParse(pubDateStr, out parsedDate))
                                {
                                    lblDate.InnerHtml = string.Format("&#128197; <strong>{0}</strong>", parsedDate.ToString("dddd, MMM dd, yyyy"));
                                }
                                else
                                {
                                    lblDate.InnerHtml = string.Format("&#128197; <strong>{0}</strong>", pubDateStr);
                                }
                            }

                            string author = reader["Author"].ToString();
                            lblAuthor.InnerHtml = string.Format("&#9997;&#65039; By <strong>{0}</strong>", string.IsNullOrEmpty(author) ? "Unknown" : author);
                        }
                        else
                        {
                            lblError.Text = "News article not found in database.";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "An error occurred while loading news details. Please check logs.";
            logger.Error(ex, string.Format("Error loading news detail for ID: {0}", id));
        }
    }
}

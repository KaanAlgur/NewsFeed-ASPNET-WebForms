using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Xml.Linq;
using NLog;

public partial class RSSparsing : System.Web.UI.Page
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnParse_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["NewsDBConn"].ConnectionString;
        int addedCount = 0;
        
        Dictionary<string, string> rssFeeds = new Dictionary<string, string>
        {
            { "Technology", "https://rss.nytimes.com/services/xml/rss/nyt/Technology.xml" },
            { "Sports", "https://rss.nytimes.com/services/xml/rss/nyt/Sports.xml" },
            { "World", "https://rss.nytimes.com/services/xml/rss/nyt/World.xml" },
            { "Health", "https://rss.nytimes.com/services/xml/rss/nyt/Health.xml" }
        };

        ArrayList allNewsList = new ArrayList();

        try
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                foreach (var feed in rssFeeds)
                {
                    string category = feed.Key;
                    string url = feed.Value;

                    try
                    {
                        XDocument doc = XDocument.Load(url);
                        var items = doc.Descendants("item");

                        int itemIndex = 0;
                        foreach (var item in items)
                        {
                            if (itemIndex >= 20) break; // 20 limit per feed

                            string title = item.Element("title") != null ? item.Element("title").Value : "";
                            string description = item.Element("description") != null ? item.Element("description").Value : "";
                            string link = item.Element("link") != null ? item.Element("link").Value : "";
                            string pubDateStr = item.Element("pubDate") != null ? item.Element("pubDate").Value : "";
                            
                            XNamespace dc = "http://purl.org/dc/elements/1.1/";
                            string author = item.Element(dc + "creator") != null ? item.Element(dc + "creator").Value : "NYT Writer";
                            
                            XNamespace media = "http://search.yahoo.com/mrss/";
                            string imageUrl = "";
                            if (item.Element(media + "content") != null && item.Element(media + "content").Attribute("url") != null)
                            {
                                imageUrl = item.Element(media + "content").Attribute("url").Value;
                            }
                            
                            // Homework asks to pass string PubDate into the constructor, so we will use pubDateStr directly.
                            News newsObj = new News(0, title, description, category, author, pubDateStr, imageUrl);
                            
                            allNewsList.Add(newsObj);

                            // Check duplicate
                            string checkSql = "SELECT COUNT(*) FROM News WHERE Title = ?";
                            using (OleDbCommand checkCmd = new OleDbCommand(checkSql, conn))
                            {
                                checkCmd.Parameters.AddWithValue("?", title);
                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (count == 0)
                                {
                                    // Insert into DB
                                    string insertSql = "INSERT INTO News (Title, Description, Category, Author, PubDate, ImageUrl) VALUES (?, ?, ?, ?, ?, ?)";
                                    using (OleDbCommand insertCmd = new OleDbCommand(insertSql, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("?", title);
                                        insertCmd.Parameters.AddWithValue("?", description);
                                        insertCmd.Parameters.AddWithValue("?", category);
                                        insertCmd.Parameters.AddWithValue("?", author);
                                        insertCmd.Parameters.AddWithValue("?", pubDateStr);
                                        insertCmd.Parameters.AddWithValue("?", imageUrl);
                                        insertCmd.ExecuteNonQuery();
                                        addedCount++;
                                    }
                                }
                            }

                            itemIndex++;
                        }
                    }
                    catch (Exception exFeed)
                    {
                        logger.Error(exFeed, string.Format("Error parsing feed {0} from {1}", category, url));
                    }
                }
            }

            Session["NewsList"] = allNewsList;
            
            stats.Visible = true;
            sNew.InnerText = addedCount.ToString();
            sTotalSession.InnerText = allNewsList.Count.ToString();
            sTotalParsed.InnerText = (4 * 20).ToString(); // 4 feeds * 20
            
            lblStatus.Visible = true;
            lblStatus.Text = string.Format("&#10004; RSS feed fetched and processed successfully! Inserted {0} new items.", addedCount);
            logger.Info(string.Format("RSS Parsing completed. Added {0} items to DB.", addedCount));
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "&#10060; An error occurred during parsing. Check logs.";
            lblStatus.Attributes["style"] = "display:block;margin-top:20px;padding:14px 16px;background:#fef2f2;border-left:3px solid #ef4444;font-size:13px;color:#b91c1c;";
            logger.Error(ex, "Database connection or general error during RSS parsing.");
        }
    }
}

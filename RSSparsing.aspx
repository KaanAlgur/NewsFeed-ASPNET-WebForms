<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RSSparsing.aspx.cs" Inherits="RSSparsing" ResponseEncoding="utf-8" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>NewsFeed – RSS Parser</title>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@700;900&family=DM+Sans:wght@300;400;500;600&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page active" id="page-rss">
            <nav class="site-nav">
                <div class="brand">The <em>News</em>Feed</div>
                <div class="nav-date">RSS Feed Manager</div>
                <a href="Home.aspx" class="nav-pill">&larr; Home</a>
            </nav>

            <div class="parser-wrap">
                <div class="parser-card">
                    <div style="font-size:11px;letter-spacing:3px;text-transform:uppercase;color:var(--accent);margin-bottom:12px;">Feed Manager</div>
                    <h1>Fetch Latest<br/>News Articles</h1>
                    <p>Pulls up to 20 articles per category from the New York Times RSS feeds and saves them to the local Access database. Duplicate titles are automatically skipped.</p>

                    <ul class="source-list">
                        <li>nytimes.com/rss/nyt/Technology <span class="badge">&#128187; Technology</span></li>
                        <li>nytimes.com/rss/nyt/Sports      <span class="badge">&#9917; Sports</span></li>
                        <li>nytimes.com/rss/nyt/World       <span class="badge">&#127757; World</span></li>
                        <li>nytimes.com/rss/nyt/Health      <span class="badge">&#127973; Health</span></li>
                    </ul>

                    <asp:Button ID="btnParse" runat="server" Text="&#128260; Fetch & Save News" CssClass="big-btn" OnClick="btnParse_Click" />

                    <div class="stat-row" id="stats" runat="server" visible="false">
                        <div class="stat-box"><strong id="sNew" runat="server">0</strong><span>New Saved</span></div>
                        <div class="stat-box"><strong id="sTotalSession" runat="server">0</strong><span>Session Total</span></div>
                        <div class="stat-box"><strong id="sTotalParsed" runat="server">60</strong><span>Total Fetched</span></div>
                    </div>

                    <asp:Label ID="lblStatus" runat="server" style="display:block;margin-top:20px;padding:14px 16px;background:#f0faf4;border-left:3px solid #16a34a;font-size:13px;color:#15803d;" Visible="false"></asp:Label>
                </div>
            </div>
            <footer>&copy; <%= DateTime.Now.Year %> NewsFeed &middot; Powered by The New York Times RSS</footer>
        </div>
    </form>
</body>
</html>

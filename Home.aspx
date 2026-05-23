<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" ResponseEncoding="utf-8" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>The NewsFeed</title>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@700;900&family=DM+Sans:wght@300;400;500;600&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page active" id="page-home">
            <nav class="site-nav">
                <div class="brand">The <em>News</em>Feed</div>
                <div class="nav-date"><%= DateTime.Now.ToString("dddd, MMM dd, yyyy") %></div>
                <a href="RSSparsing.aspx" class="nav-pill">&#128260; Refresh Feed</a>
            </nav>

            <div class="masthead">
                <div class="masthead-top">
                    <div class="masthead-title">Breaking<br/><em>Stories.</em></div>
                    <div class="masthead-meta">
                        Powered by New York Times RSS<br/>
                        Categories &middot; Up to 20 Articles<br/>
                        Auto-updated from feed
                    </div>
                </div>
            </div>

            <div class="cat-tabs" id="catTabsContainer" runat="server">
                <!-- Tabs generated from C# -->
            </div>

            <div id="newsContainer" runat="server">
                <!-- News sections generated from C# -->
            </div>

            <asp:Label ID="lblError" runat="server" style="color:red;display:block;text-align:center;padding:20px;"></asp:Label>

            <footer>&copy; <%= DateTime.Now.Year %> NewsFeed &middot; Powered by The New York Times RSS</footer>
        </div>
    </form>

    <script>
        function showCat(i, btn){
            document.querySelectorAll('.news-section').forEach(s => s.classList.remove('visible'));
            document.querySelectorAll('.cat-tab').forEach(b => b.classList.remove('active'));
            document.getElementById('cat-' + i).classList.add('visible');
            btn.classList.add('active');
        }
    </script>
</body>
</html>

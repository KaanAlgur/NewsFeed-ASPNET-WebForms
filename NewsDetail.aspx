<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="NewsDetail" ResponseEncoding="utf-8" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>NewsDetail</title>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@700;900&family=DM+Sans:wght@300;400;500;600&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page active" id="page-detail">
            <nav class="site-nav">
                <div class="brand">The <em>News</em>Feed</div>
                <div class="nav-date">Article Detail</div>
                <a href="Home.aspx" class="nav-pill">&larr; Home</a>
            </nav>

            <div class="detail-wrap">
                <a class="back-link" href="Home.aspx">&larr; Back to Home</a>

                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

                <div id="contentPanel" runat="server" visible="false">
                    <div class="detail-kicker" id="lblKicker" runat="server">Technology</div>
                    
                    <h1 class="detail-title" id="lblTitle" runat="server"></h1>
                    
                    <hr class="detail-rule"/>
                    
                    <div class="detail-meta">
                        <span id="lblAuthor" runat="server">&#9997;&#65039; By <strong>NYT Writer</strong></span>
                        <span id="lblDate" runat="server">&#128197; <strong>Date</strong></span>
                        <span id="lblCategory" runat="server">&#127991;&#65039; <strong>Category</strong></span>
                    </div>

                    <div class="detail-hero" id="heroEmoji" runat="server">&#128240;</div>

                    <div class="detail-body" id="lblDescription" runat="server">
                    </div>

                    <div class="detail-tag-row">
                        <a href="Home.aspx" style="color:var(--accent); font-weight:bold; text-decoration:none;">&larr; Back to News</a>
                    </div>
                </div>
            </div>
            <footer>&copy; <%= DateTime.Now.Year %> NewsFeed &middot; Powered by The New York Times RSS</footer>
        </div>
    </form>
</body>
</html>

# The NewsFeed - RSS News Aggregator 📰

[![ASP.NET Web Forms](https://img.shields.io/badge/ASP.NET-Web%20Forms-blue.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/Language-C%23-green.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Database](https://img.shields.io/badge/Database-MS%20Access%20%28.mdb%29-red.svg)]()
[![Logging](https://img.shields.io/badge/Logging-NLog-orange.svg)](https://nlog-project.org/)

**The NewsFeed** is a dynamic web-based news aggregator application built with **ASP.NET Web Forms (C#)** and a **Microsoft Access Database**. It parses real-time RSS feeds from **The New York Times**, stores articles locally, and presents them in a modern, responsive, and visually appealing web interface.

This project was developed as part of the *Web Based Technologies* coursework.

---

## 🚀 Features

### 1. Home Page (`Home.aspx`)
* **Categorized Layout:** Displays news articles retrieved from the local database grouped into categories (e.g., Technology, Sports, World, Health).
* **Featured Article:** Dynamically highlights the latest breaking news in a hero card.
* **Modern UI:** Styled with a premium dark mode theme, vibrant gradients, and smooth hover effects/micro-interactions.
* **Smart Navigation:** Quick-filtering categories and easy navigation to article details.

### 2. RSS Parsing & Database Sync (`RSSparsing.aspx`)
* **Live Crawling:** Fetches up to 20 articles per category directly from the official **New York Times RSS Feeds**.
* **Duplication Prevention:** Automatically checks for existing article titles in the database and skips duplicates.
* **Real-time Session Stats:** Shows total fetched articles, newly inserted articles, and session-specific counts.
* **User Feedback:** Clear success/error notifications upon synchronization.

### 3. News Detail Page (`NewsDetail.aspx`)
* Shows the complete details of a selected news article (Title, Category, Publication Date, Author, and Description).
* Provides a direct link to read the full original story on the New York Times website.

### 4. Technical Architecture
* **`News.cs` (App_Code):** Custom class representing a news item containing properties like `ID`, `Title`, `Description`, `Link`, `PubDate`, `Category`, and `Author`. Uses `ArrayList` for internal data structures as requested.
* **Access Database Integration:** Operates on `App_Data/NewsDB.mdb` using standard `OleDbConnection`, `OleDbCommand`, and parametrized queries.
* **Logging System:** Implements **NLog** (via NuGet) for tracking RSS fetches, database transactions, and error handling.
* **Custom Styling:** Structured entirely with pure vanilla CSS (`style.css`), avoiding default browser styles for a premium user experience.

---

## 📂 Project Structure

```
Hw4NewsProject/
│
├── App_Code/
│   └── News.cs             # Core object model for News items
├── App_Data/
│   ├── NewsDB.mdb          # MS Access database storing news articles
│   └── logs/               # NLog auto-generated log files
│
├── Home.aspx               # Main landing page listing news cards
├── RSSparsing.aspx         # Admin panel to fetch and sync RSS feeds
├── NewsDetail.aspx         # Full article detail view
│
├── style.css               # Premium CSS styles (Dark mode, glassmorphism)
├── Web.config              # ASP.NET configuration file
├── NLog.config             # NLog logger settings
├── packages.config         # NuGet dependencies configuration
└── .gitignore              # Files to ignore in Git (excludes .vs, bin, obj, etc.)
```

---

## ⚙️ Installation & Running the Project

### Prerequisites
1. **Visual Studio** (2019 / 2022 recommended) with the *.NET desktop development* and *ASP.NET and web development* workloads installed.
2. **Microsoft Access Database Engine** (Required to read `.mdb` files via OLE DB drivers).

### Steps
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/KaanAlgur/NewsFeed-ASPNET-WebForms.git
   cd NewsFeed-ASPNET-WebForms
   ```
2. **Open in Visual Studio:**
   * Open Visual Studio.
   * Click on **File** ➡️ **Open** ➡️ **Web Site...**
   * Select the root directory containing the project.
3. **Restore NuGet Packages:**
   * Visual Studio should automatically restore the **NLog** package. If not, open the Package Manager Console and run:
     ```powershell
     Update-Package -reinstall
     ```
4. **Run the Application:**
   * Select your preferred web browser (e.g., IIS Express / Google Chrome) and click **Start Debugging (F5)** or **Start Without Debugging (Ctrl + F5)**.
   * Ensure `Home.aspx` is set as the start page.

---

## 🛠️ Technologies Used
* **Backend:** C# (.NET Framework 4.7.2)
* **Frontend:** ASP.NET Web Forms, HTML5, CSS3 (Custom responsive grid layout & animations)
* **Database:** MS Access (`.mdb` format via OLE DB)
* **Logging:** NLog (v5.2.8)

---

## 📜 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

**Developed by:** [@KaanAlgur](https://github.com/KaanAlgur)

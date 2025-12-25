# TripleA Personal Portfolio

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Interactive_Server-512BD4?style=flat&logo=blazor)
![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?style=flat&logo=docker)
![License](https://img.shields.io/badge/license-Apache%20License%202.0-blue)

A dynamic, full-stack personal portfolio website built with **.NET 10** and **Blazor Web App**. This application features a public-facing portfolio to showcase skills and projects, coupled with a secure Admin Dashboard for managing content directly from the browser.

## 🚀 Features

### 🎨 Public Portfolio

* **Dynamic Content:** All text, skills, and projects are fetched from the database, not hardcoded.
* **Responsive Design:** Fully responsive layout built with Bootstrap 5.
* **Social Integration:** Configurable social media links with dynamic icon rendering.

### 🔐 Admin Dashboard

* **Secure Authentication:** Built on ASP.NET Core Identity (Cookie-based auth).
* **Content Management:** Full CRUD (Create, Read, Update, Delete) capabilities for:
  * **Profile:** Update bio, job title, and resume URL.
  * **Projects:** Add new case studies with images and links.
  * **Skills:** Manage technical skills with "Major Skill" highlighting.
  * **Socials:** Reorder and update social links.
* **File Uploads:** Drag-and-drop support for project images and documents.

## 🛠️ Tech Stack

* **Framework:** .NET 10 (Blazor Web App - Interactive Server)
* **Database:** SQLite (with Entity Framework Core)
* **Frontend:** Razor Components + Bootstrap 5
* **Deployment:** Docker & Docker Compose
* **Reverse Proxy:** Nginx
* **CI/CD:** GitHub Actions

## 💻 Getting Started (Local Development)

Follow these steps to run the project on your local machine.

### Prerequisites

* [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
* (Optional) Docker Desktop if testing containers locally.

### Installation

1. **Clone the repository**

    ```bash
    git clone [https://github.com/YOUR_USERNAME/TripleAWebsite.git](https://github.com/YOUR_USERNAME/TripleAWebsite.git)
    cd TripleAWebsite
    ```

2. **Run the Application**
    navigate to the project folder and run:

    ```bash
    cd TripleAWebsite.Frontend
    dotnet run
    ```

3. **First Run Setup**
    * The application will automatically create the SQLite database (`app.db`) and apply migrations on startup.
    * Open your browser to `https://localhost:7146` (or the port shown in your terminal).

4. **Create Admin Account**
    * Navigate to `/Account/Register`.
    * Create a new account.
    * **Note:** In Development mode, email confirmation is simulated. Click the link provided on the screen to confirm your account.

## 🐳 Deployment (Docker)

This project is configured for production deployment using Docker Compose and Nginx.

### Architecture

* **App Container:** Runs the Blazor application on port 8080.
* **Nginx Container:** Acts as a reverse proxy, handling SSL and routing traffic to the app.
* **Volumes:**
  * `app_data`: Persists the SQLite database.
  * `uploads_data`: Persists user-uploaded images/PDFs.

### Deploying Manually

To test the production build locally:

```bash
docker compose up --build

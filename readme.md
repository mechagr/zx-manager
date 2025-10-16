# ZX Manager (September 2025)

ZX Manager is a full-stack web application for managing ZX Spectrum game collections, built with ASP.NET Core MVC.

This project was developed as an enhanced version of [VinylManager](https://github.com/mechagr/vinyl-manager), adapting the core collection management concepts for ZX Spectrum enthusiasts. It demonstrates Model–View–Controller architecture, Entity Framework Core integration, and secure user authentication. ZX Manager showcases the ability to build complete web applications with database persistence, RESTful routing, and user management within a modern web framework.

---

## Project Overview

The aim of ZX Manager is to enable users to build and maintain a personal ZX Spectrum game collection. Users can add, edit, delete, and organise their games by publisher, year of release, and genre. Full user authentication ensures that each user's data remains private. Additional features include game ratings, global search, and sortable collections. All information is stored in SQL Server with Entity Framework Core managing database operations.

The application demonstrates:

- Model–View–Controller (MVC) architectural pattern  
- Entity Framework Core with Code First migrations and relationships  
- ASP.NET Identity for user authentication and authorisation  
- RESTful CRUD operations with appropriate HTTP verbs and routing  
- Razor views with server-side rendering and Bootstrap styling  
- Database design with foreign keys and navigation properties  
- Search functionality with filtering across multiple data fields  

---

## Features

- **Add, Edit, and Delete Games** – complete CRUD system for managing your game collection  
- **Publisher, Year, and Genre Management** – organise your collection efficiently  
- **Game Rating System** – rate the games you’ve played  
- **Global Search** – search across game titles, publishers, and genres from the navigation bar  
- **Sortable Collections** – sort games by title, rating, publisher, year of release, or genre, in ascending or descending order  
- **User Authentication** – secure registration and login system  
- **Data Relationships** – properly linked games, publishers, and genres  
- **Dark Theme Interface** – modern, dark UI for comfortable viewing  
- **Responsive Design** – Bootstrap-powered layout works on all devices  
- **Database Seeding** – pre-populated sample game data for testing  
- **Form Validation** – client- and server-side input validation  
- **Privacy-Conscious** – GDPR-compliant data handling and user privacy controls  

---

## Technologies Used

- **Language**: C#  
- **Framework**: ASP.NET Core MVC (.NET 8)  
- **Database**: SQL Server with Entity Framework Core  
- **Authentication**: ASP.NET Identity  
- **Frontend**: Razor Views, Bootstrap 5, HTML5/CSS3, Font Awesome  
- **Concepts**: MVC pattern, ORM, authentication, RESTful APIs, responsive design, search algorithms  

---

## How to Run

Clone the repository and run the application locally:

```bash
git clone https://github.com/your-username/ZXManager.git
cd ZXManager
dotnet ef database update
dotnet run
Once running, navigate to https://localhost:5001 (or the port specified in your console) to start using ZX Manager.

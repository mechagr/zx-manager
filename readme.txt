# ZX Manager (September 2025)

ZX Manager is a full-stack web application for managing ZX Spectrum game collections, built with ASP.NET Core MVC.

This project was developed as an enhanced version of VinylManager (https://github.com/mechagr/vinyl-manager), adapting the core collection management concepts for ZX Spectrum enthusiasts such as myself. It was created to demonstrate Model–View–Controller architecture, Entity Framework Core integration, and user authentication systems. It showcases my ability to build complete web applications with database persistence, RESTful routing, and secure user management within a modern web framework.

---

## Project Overview

The goal of ZX Manager is to allow users to build and maintain a personal ZX Spectrum game collection. Users can add, edit, delete, and organise their games by publisher, year of release and genre, with full user authentication protecting their data. The application includes features such as game ratings, global search functionality, and sortable collections. Information is stored in SQL Server with Entity Framework Core handling all database operations.

The application demonstrates:

- Model–View–Controller (MVC) architectural pattern  
- Entity Framework Core with Code First migrations and relationships  
- ASP.NET Identity for user authentication and authorisation  
- RESTful CRUD operations with proper HTTP verbs and routing  
- Razor views with server-side rendering and Bootstrap styling  
- Database design with foreign keys and navigation properties  
- Search functionality with filtering across multiple data fields  

---

## Features

- **Add, Edit, and Delete Games** - complete CRUD system for game collection management  
- **Publisher, Year and Genre Management** - organise collection by publisher, year of release and genre
- **Game Rating System** - allows users to rate played games
- **Global Search** - search across game titles, publishers and genres from the navigation bar  
- **Sortable Collections** - sort games by title, rating, publisher, year of release, and genre with ascending/descending options  
- **User Authentication** - secure registration and login system  
- **Data Relationships** - properly linked games, publishers, and genres  
- **Dark Theme Interface** - modern, dark UI 
- **Responsive Design** - Bootstrap-powered UI that works on all devices  
- **Database Seeding** - automatically populate with sample game data  
- **Form Validation** - client- and server-side input validation  
- **Privacy-Conscious** - GDPR-compliant data handling and user privacy controls  

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
# AIDraw – AI-Powered Drawing Web Application

AIDraw is a web application that allows users to **create drawings**, **save them** and **generate AI-powered artwork** using the OpenAI API.  
The project is built with **ASP.NET Core Web API** (backend) and **ASP.NET Core MVC** (frontend).

---

## Features

### Drawing Features
- Draw on a canvas in the browser.
- Save user drawings to the database.

### AI Generation Features
- Send a text prompt to generate an AI-based drawing.
- Display AI-generated images inside the UI.
- Secure API communication between frontend and backend.

### Architecture
- **AIDrawWebAPI** → Backend (REST API)
- **AIDrawWebApp** → Frontend (MVC application)
- Controllers handle:
  - Saving drawings (`AIDrawController`)
  - AI image generation (`AIPromptController`)
  - Frontend rendering & UI requests (`AIDrawWebAppController`)


---

## Technologies Used

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- RESTful API

### Frontend
- ASP.NET Core MVC
- HTML, CSS, JavaScript
- Canvas API for drawing

### AI Integration
- OpenAI API (Image generation)



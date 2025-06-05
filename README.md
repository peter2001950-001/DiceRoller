# 🎲 DiceRoller Microservices

This is a simple ASP.NET Core 8 project structured as two microservices:

- **DiceRoller.Identity.API** – Handles user authentication and authorization (Register/Login).
- **DiceRoller.Engine.API** – Handles dice rolling operations and stores the roll history.

---

## 🧱 Project Structure

DiceRoller/
├── DiceRoller.Identity.API
│ └── [Login & Register endpoints]
├── DiceRoller.Engine.API
│ └── [Dice Roll & History endpoints]
├── shared configurations
└── README.md

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

---

## ⚙️ Configuration

1. **Database Setup**

   Make sure PostgreSQL is running locally.

2. **Update Connection Strings**

   In both `DiceRoller.Identity.API/appsettings.json` and `DiceRoller.Engine.API/appsettings.json`, update the `DefaultConnection` under `ConnectionStrings` to match your local PostgreSQL setup.

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=diceroller_db;Username=your_user;Password=your_password"
   }

   

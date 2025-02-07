📌 Project Overview

The Asset Tracking System is a .NET Core Console Application that helps manage company assets such as Laptops and Mobile Phones, categorized by Office Locations. The system provides features like CRUD operations, sorting, grouping, and currency conversion for asset valuation.

🎯 Features

✅ Add, Update, Delete Assets (Laptops & Mobiles)✅ Add, Update, Delete Offices✅ Sort Assets by type and purchase date✅ Group Assets by office✅ Currency Conversion based on office location✅ Asset Lifecycle Tracking (RED, YELLOW, NORMAL status based on lifespan)

🛠️ Setup Instructions

1️⃣ Clone the Repository

If you haven't already, clone this project from GitHub:

git clone https://github.com/your-username/your-repository.git

2️⃣ Open the Project in Visual Studio

Open Visual Studio.

Click Open a project or solution.

Select the AssetTrackingEntityFrameWork.sln file.

3️⃣ Install Dependencies

Ensure you have the required NuGet packages installed:
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

🛂 Database Setup with Entity Framework Core

1️⃣ Open the Package Manager Console

In Visual Studio, go to:Tools → NuGet Package Manager → Package Manager Console

2️⃣ Apply Migrations
Apply Migrations

Run the following command to create a migration:

add-migration InitialCreate

This command generates the database schema based on your Entity Framework Core models.

3️⃣ Update the Database

After creating a migration, apply it to the database:

update-database

This creates the necessary tables in your local SQL Server database.

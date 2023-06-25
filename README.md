# RedditApp

## Description

The **Reddit** project is a WebAPI developed in ASP.NET, using Entity Framework Core for database interaction. It provides a programming interface for creating a Reddit-like application where users can post, comment, and vote on content.

## Setup

1. Install Visual Studio 2022:
   - Download and install Visual Studio 2022 from [here](https://visualstudio.microsoft.com/downloads/).
   - Select the necessary components during the installation process.

2. Install PostgreSQL:
   - Download and install PostgreSQL from [here](https://www.postgresql.org/download/).
   - Follow the installation steps and set a password for the default user "postgres".

3. Create the database:
   - Open the project solution in Visual Studio 2022.
   - In the Package Manager Console, run the following command to create the database using Entity Framework Core:

     ``` powershell
     Update-Database
     ```

   - This will apply the necessary migrations and create the database structure.

## Software Versions Used

- Visual Studio 2022
  - ASP.NET and web development
  - .NET desktop development
- PostgreSQL

## Connection Configuration

The connection string is specified in the `AppDbContext` class. Follow these steps to configure the connection:

1. Open the `AppDbContext.cs` file located in your project.
2. Locate the `OnConfiguring` method within the class. It should look like this:

   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       optionsBuilder
           .UseNpgsql("Server=localhost;Database=RedditApp;User Id=postgres;Password=postgres;TrustServerCertificate=True;")
           .LogTo(Console.WriteLine);
   }
   ```

3. If needed, modify the connection string parameters to match your specific configuration. For example, you may need to update the server address, database name, or authentication credentials.

With these steps, the connection string specified in the `AppDbContext` class will be used when the application runs.
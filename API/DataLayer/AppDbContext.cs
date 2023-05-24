using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class AppDbContext : DbContext
{
    // this is where tables are defined
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // SqlServer authentication
        // optionsBuilder.UseSqlServer("Data Source = myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;").LogTo(Console.WriteLine);

        // Windows authentication (the one we use)
        optionsBuilder
            .UseSqlServer("Server=localhost;Database=RedditApp;Integrated Security=SSPI;TrustServerCertificate=True;")
            .LogTo(Console.WriteLine);
    }
}
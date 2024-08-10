using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData;

public class PubContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = 
            "data source=localhost,1533;initial catalog=PublisherApp;" +
            "user id=sa;password=Patient0Zero;" +
            "Encrypt=True;TrustServerCertificate=True;" +
            "App=EntityFramework";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
                     new Author { Id = 1, FirstName = "Rhoda", LastName = "Lerman1", Gender = "Female"});

        var authorList = new Author[]{
                new Author {Id = 2, FirstName = "Ruth", LastName = "Ozeki", Gender = "Unknown" },
                new Author {Id = 3, FirstName = "Sofia", LastName = "Segovia", Gender = "Unknown"  },
                new Author {Id = 4, FirstName = "Ursula K.", LastName = "LeGuin", Gender = "Unknown"  },
                new Author {Id = 5, FirstName = "Hugh", LastName = "Howey", Gender = "Unknown"  },
                new Author {Id = 6, FirstName = "Isabelle", LastName = "Allende", Gender = "Unknown"  }
            };
        modelBuilder.Entity<Author>().HasData(authorList);
    }
}
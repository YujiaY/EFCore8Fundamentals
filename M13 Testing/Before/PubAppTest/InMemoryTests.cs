using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherData;
using PublisherDomain;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace PubAppTest;

[TestClass]
public class InMemoryTests
{
    const string connectionString = 
        "data source=localhost,1533;initial catalog=PublisherAppTestDb;" +
        "user id=sa;password=Patient0Zero;" +
        "Encrypt=True;TrustServerCertificate=True;" +
        "App=EntityFramework";
    
    [TestMethod]
    public void CanInsertAuthorIntoDatabase()
    {
        var builder = new DbContextOptionsBuilder<PubContext>();
        
        var _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        
        builder.UseSqlite(_connection);

        using (var context = new PubContext(builder.Options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var author = new Author { FirstName = "a", LastName = "b" };
            context.Authors.Add(author);
            //Debug.WriteLine($"Before save: {author.AuthorId}");
            context.SaveChanges();
            //Debug.WriteLine($"After save: {author.AuthorId}");

            Assert.AreNotEqual(0, author.AuthorId);
        }

    }

    [TestMethod]
    public void ChangeTrackerIdentifiesAddedAuthor()
    {
        var builder = new DbContextOptionsBuilder<PubContext>().UseSqlServer(connectionString);
         
        using var context = new PubContext(builder.Options);
        var author = new Author { FirstName = "a", LastName = "b" };
        context.Authors.Add(author);
        Assert.AreEqual(EntityState.Added, context.Entry(author).State);
    }
}
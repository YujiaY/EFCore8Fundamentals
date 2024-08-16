using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using PublisherData;
using PublisherDomain;

public static class AuthorEndpoints
{
    public static void MapAuthorEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/AuthorEndpoints").WithTags(nameof(Author));

        group.MapGet("/", async (PubContext db) =>
        {
            return await db.Authors.AsNoTracking().ToListAsync();
        })
        .WithName("GetAllAuthors")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Author>, NotFound>> (int authorid, PubContext db) =>
        {
            return await db.Authors.AsNoTracking()
                .FirstOrDefaultAsync(model => model.AuthorId == authorid)
                is { } model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAuthorById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int authorid, Author author, PubContext db) =>
        {
            var affected = await db.Authors
                .Where(model => model.AuthorId == authorid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.AuthorId, author.AuthorId)
                  .SetProperty(m => m.FirstName, author.FirstName)
                  .SetProperty(m => m.LastName, author.LastName)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAuthor")
        .WithOpenApi();

        group.MapPost("/", async (Author author, PubContext db) =>
        {
            db.Authors.Add(author);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/AuthorEndpoints/{author.AuthorId}", author);
        })
        .WithName("CreateAuthor")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int authorid, PubContext db) =>
        {
            var affected = await db.Authors
                .Where(model => model.AuthorId == authorid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAuthor")
        .WithOpenApi();
    }
}
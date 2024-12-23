﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class insertauthorprocedure_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.AuthorInsert
                @firstname nvarchar(100),
                @lastname nvarchar(100),
                @id int OUT
                AS
                BEGIN
                  INSERT into [Authors] (FirstName, LastName)
                  OUTPUT Inserted.[AuthorId]
                  Values(@firstname, @lastname);

               END");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.AuthorInsert");
        }
    }
}

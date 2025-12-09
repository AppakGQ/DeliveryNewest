using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryNew.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        // The 'Up' method applies the changes to the database.
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creating the 'DeliveryItems' table to store products.
            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    // Primary Key (Auto-incrementing ID)
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    // Product Name (max 100 chars)
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    // Product Description
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    // Product Price
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    // Availability status
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    // Creation Date
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryItems");
        }
    }
}

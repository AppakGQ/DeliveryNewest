using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryNew.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adding 'ImageUrl' column to 'DeliveryItems' table.
            // This allows storing the path to the product image.
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DeliveryItems",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DeliveryItems");
        }
    }
}

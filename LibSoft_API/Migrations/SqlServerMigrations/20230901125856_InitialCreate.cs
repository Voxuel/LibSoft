using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibSoft_API.Migrations.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Genre", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "John R Sanders", "John Sanders tour of the seven seas and what he faced along the way", "Autobiography", "Over the ocean", 2011 },
                    { 2, "Dr Emilia Wing", "The moments in medicine and its challenging nature", "Autobiography", "Patience", 2023 },
                    { 3, "Martin Andersson", "Set in 1800 england, a story about a beggars rise to become an assassin fora dark brotherhood created to dismantle the rich", "Fiction/Thriller", "The Highborn's demise", 2015 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}

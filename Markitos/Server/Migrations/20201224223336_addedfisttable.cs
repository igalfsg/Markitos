using Microsoft.EntityFrameworkCore.Migrations;

namespace Markitos.Server.Migrations
{
    public partial class addedfisttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    PostID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareWithFamOnly = table.Column<bool>(type: "bit", nullable: false),
                    ShareAnon = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.PostID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stories");
        }
    }
}

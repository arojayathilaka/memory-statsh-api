using Microsoft.EntityFrameworkCore.Migrations;

namespace memory_stash.Migrations
{
    public partial class updatememoryimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "MemoryImages",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "MemoryImages",
                newName: "ImageUrl");
        }
    }
}

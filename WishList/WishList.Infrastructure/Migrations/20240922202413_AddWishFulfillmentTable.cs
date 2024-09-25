using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WishList.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWishFulfillmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishFulfillments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WishGranterId = table.Column<int>(type: "INTEGER", nullable: false),
                    WishId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishFulfillments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishFulfillments_Users_WishGranterId",
                        column: x => x.WishGranterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishFulfillments_Wishes_WishId",
                        column: x => x.WishId,
                        principalTable: "Wishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishFulfillments_WishGranterId",
                table: "WishFulfillments",
                column: "WishGranterId");

            migrationBuilder.CreateIndex(
                name: "IX_WishFulfillments_WishId",
                table: "WishFulfillments",
                column: "WishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishFulfillments");
        }
    }
}

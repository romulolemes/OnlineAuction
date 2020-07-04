using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineAuction.API.Data.Migrations
{
    public partial class AddTableAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    InitialValue = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    User = table.Column<string>(maxLength: 200, nullable: false),
                    InitialDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auction");
        }
    }
}

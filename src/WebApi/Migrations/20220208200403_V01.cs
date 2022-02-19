using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class V01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Highscores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Time = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highscores", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Highscores",
                columns: new[] { "Id", "Name", "Time" },
                values: new object[,]
                {
                    { new Guid("1098f549-e790-4e9f-aa16-18c2292a2ee9"), "Bill", 190L },
                    { new Guid("1198f549-e790-4e9f-aa16-18c2292a2ee9"), "Winston Wolf", 200L },
                    { new Guid("1298f549-e790-4e9f-aa16-18c2292a2ee9"), "Jackie Brown", 210L },
                    { new Guid("1398f549-e790-4e9f-aa16-18c2292a2ee9"), "Marsellus Wallace", 220L },
                    { new Guid("1498f549-e790-4e9f-aa16-18c2292a2ee9"), "Mr Pink", 230L },
                    { new Guid("1598f549-e790-4e9f-aa16-18c2292a2ee9"), "Zed", 240L },
                    { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Vincent Vega", 160L },
                    { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Hattori Hanzo", 150L },
                    { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Beatrix Kiddo", 170L },
                    { new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), "Calvin Candie", 180L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Highscores");
        }
    }
}

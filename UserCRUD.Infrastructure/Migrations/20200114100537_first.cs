using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserCRUD.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCRUD",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCRUD", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserCRUD",
                columns: new[] { "Id", "Birthdate", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 1, 14, 11, 5, 37, 632, DateTimeKind.Local).AddTicks(2320), "Usuario 1" },
                    { 2L, new DateTime(2020, 1, 14, 11, 5, 37, 633, DateTimeKind.Local).AddTicks(4202), "Usuario 2" },
                    { 3L, new DateTime(2020, 1, 14, 11, 5, 37, 633, DateTimeKind.Local).AddTicks(4214), "Usuario 3" },
                    { 4L, new DateTime(2020, 1, 14, 11, 5, 37, 633, DateTimeKind.Local).AddTicks(4217), "Usuario 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCRUD");
        }
    }
}

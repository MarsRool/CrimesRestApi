using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CrimesRestApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "crime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UUID = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Solved = table.Column<bool>(type: "boolean", nullable: false),
                    RequirePolice = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_crime_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_crime_UserId",
                table: "crime",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crime");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoggerMessage",
                columns: table => new
                {
                    LogMessageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicationId = table.Column<int>(type: "integer", nullable: true),
                    MessageLog = table.Column<string>(type: "text", nullable: true),
                    LogLevel = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggerMessage", x => x.LogMessageId);
                    table.ForeignKey(
                        name: "FK_LoggerMessage_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggerMessage_ApplicationId",
                table: "LoggerMessage",
                column: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggerMessage");

            migrationBuilder.DropTable(
                name: "Application");
        }
    }
}

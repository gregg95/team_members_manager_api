using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedTeamMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "CreatedAtDateTime", "Email", "Name", "PhoneNumber", "State" },
                values: new object[,]
                {
                    { new Guid("5caf1fda-c0fd-49dd-8622-058e98b0f2b9"), new DateTime(2024, 1, 6, 16, 36, 25, 826, DateTimeKind.Utc).AddTicks(5089), "marshall.simmons@example.com", "Marshall Simmons", "(309) 822-2653", 0 },
                    { new Guid("71a01fea-76d2-4eb4-b305-f3d5c0fea561"), new DateTime(2024, 1, 6, 16, 36, 25, 826, DateTimeKind.Utc).AddTicks(5099), "victoria.west@example.com", "Victoria West", "(603) 232-6206", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: new Guid("5caf1fda-c0fd-49dd-8622-058e98b0f2b9"));

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: new Guid("71a01fea-76d2-4eb4-b305-f3d5c0fea561"));
        }
    }
}

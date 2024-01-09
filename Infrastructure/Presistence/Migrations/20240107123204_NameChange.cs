using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class NameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: new Guid("5caf1fda-c0fd-49dd-8622-058e98b0f2b9"));

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: new Guid("71a01fea-76d2-4eb4-b305-f3d5c0fea561"));

            migrationBuilder.RenameColumn(
                name: "State",
                table: "TeamMembers",
                newName: "Status");

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "CreatedAtDateTime", "Email", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("054fbf21-a875-4296-bc56-0cd6b056a310"), new DateTime(2024, 1, 7, 12, 32, 4, 411, DateTimeKind.Utc).AddTicks(9232), "victoria.west@example.com", "Victoria West", "(603) 232-6206", 1 },
                    { new Guid("2ccb5426-b94e-44dd-a5f9-31478baaac89"), new DateTime(2024, 1, 7, 12, 32, 4, 411, DateTimeKind.Utc).AddTicks(9222), "marshall.simmons@example.com", "Marshall Simmons", "(309) 822-2653", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: new Guid("054fbf21-a875-4296-bc56-0cd6b056a310"));

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: new Guid("2ccb5426-b94e-44dd-a5f9-31478baaac89"));

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TeamMembers",
                newName: "State");

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "CreatedAtDateTime", "Email", "Name", "PhoneNumber", "State" },
                values: new object[,]
                {
                    { new Guid("5caf1fda-c0fd-49dd-8622-058e98b0f2b9"), new DateTime(2024, 1, 6, 16, 36, 25, 826, DateTimeKind.Utc).AddTicks(5089), "marshall.simmons@example.com", "Marshall Simmons", "(309) 822-2653", 0 },
                    { new Guid("71a01fea-76d2-4eb4-b305-f3d5c0fea561"), new DateTime(2024, 1, 6, 16, 36, 25, 826, DateTimeKind.Utc).AddTicks(5099), "victoria.west@example.com", "Victoria West", "(603) 232-6206", 1 }
                });
        }
    }
}

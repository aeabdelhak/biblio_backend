using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioPfe.Migrations
{
    /// <inheritdoc />
    public partial class orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Rules",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 10, 14, 16, 19, 843, DateTimeKind.Local).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 7, 10, 14, 16, 19, 843, DateTimeKind.Local).AddTicks(7070), "DBC02E7BCB99B54FA2C55B239B93E9BAA3D900536ABD21D05668CD2C7B436C68E31EB503EF7AA2D1108F20F303591C845642F611161F920F46D54CD843A67D0C", new byte[] { 6, 2, 131, 71, 169, 58, 86, 165, 210, 44, 47, 46, 32, 230, 141, 25, 209, 4, 189, 219, 83, 187, 167, 127, 186, 80, 155, 206, 92, 20, 176, 113, 31, 250, 106, 170, 232, 88, 141, 222, 61, 158, 109, 206, 138, 67, 35, 65, 41, 150, 143, 58, 198, 65, 123, 70, 150, 69, 168, 31, 216, 63, 133, 172 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Rules",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 10, 1, 2, 25, 25, DateTimeKind.Local).AddTicks(3170));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 7, 10, 1, 2, 25, 25, DateTimeKind.Local).AddTicks(2730), "F485C3E93D02E7DCA6DC3A8F234D668CF568B755399CEC551749F597B8A6C5D8B4F0E9270D3A9D343062FA973968AB8C3C69637A958B054BA9A0C33BA9C06E74", new byte[] { 54, 169, 40, 207, 189, 220, 161, 93, 218, 142, 34, 32, 127, 115, 229, 8, 214, 32, 132, 68, 85, 19, 176, 62, 160, 174, 28, 6, 224, 86, 93, 69, 136, 2, 202, 254, 220, 208, 140, 159, 177, 3, 220, 210, 30, 89, 31, 208, 77, 222, 167, 169, 105, 115, 149, 177, 208, 242, 165, 246, 197, 89, 136, 6 } });
        }
    }
}

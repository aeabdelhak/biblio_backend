using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioPfe.Migrations
{
    /// <inheritdoc />
    public partial class addressChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "DeliveryAddresses",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Rules",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 13, 1, 32, 13, 806, DateTimeKind.Local).AddTicks(8620));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 7, 13, 1, 32, 13, 806, DateTimeKind.Local).AddTicks(8080), "84B78CBEFF33858335B41736B0FC29AA35B5F980F7CBA74FC2CBD6D6D14AD6671FA2AAA3A0EDBCAFC228EAE58D207F25ED5E825C2C6175D5A77164434EB7598C", new byte[] { 89, 143, 223, 189, 12, 32, 110, 45, 53, 174, 3, 20, 168, 102, 7, 4, 27, 42, 42, 187, 34, 176, 142, 97, 224, 27, 157, 252, 52, 114, 150, 156, 172, 68, 136, 177, 202, 135, 143, 209, 157, 242, 216, 64, 44, 223, 97, 22, 7, 69, 92, 218, 215, 97, 197, 219, 100, 237, 223, 27, 224, 106, 83, 193 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "DeliveryAddresses");

            migrationBuilder.UpdateData(
                table: "Rules",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 12, 23, 37, 57, 143, DateTimeKind.Local).AddTicks(8120));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 7, 12, 23, 37, 57, 143, DateTimeKind.Local).AddTicks(7610), "368F81170000949B64EA2A0FF7E160AF3A909E23CBA976AC100853F29664B7D2CBE7A2830A32609E3D97187CCD40A648C6E3DD7965D5637069FD147B5F0F97B0", new byte[] { 216, 250, 8, 161, 83, 175, 78, 214, 244, 67, 60, 12, 70, 114, 121, 254, 29, 235, 96, 125, 158, 189, 231, 33, 9, 155, 87, 250, 113, 155, 26, 107, 23, 45, 248, 49, 66, 100, 211, 57, 108, 71, 82, 214, 142, 40, 72, 255, 237, 32, 187, 13, 46, 56, 152, 44, 63, 190, 43, 99, 37, 127, 162, 250 } });
        }
    }
}

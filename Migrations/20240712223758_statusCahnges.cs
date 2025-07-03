using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioPfe.Migrations
{
    /// <inheritdoc />
    public partial class statusCahnges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MadeById",
                table: "OrderStatuses",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CustomClient",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomClient",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CustomClient",
                type: "longtext",
                nullable: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CustomClient");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomClient");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CustomClient");

            migrationBuilder.AlterColumn<Guid>(
                name: "MadeById",
                table: "OrderStatuses",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

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
    }
}

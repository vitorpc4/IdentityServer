using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations
{
    public partial class Adicionandorotaregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "12396121-2f4a-45fb-a52b-479bd6f2e43c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "09748fd3-8d01-4e48-a790-66f046db9b58", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a45bd004-9416-4545-a801-7fab7bbee171", "AQAAAAEAACcQAAAAEB0RmJr3fiwuixQTZhJp+UYZJ0MlwMQ/QlfYSG4e4ttZXgfxboip9PXZqpUEjRJUag==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "49fefbd0-ddcd-4cc5-bcff-89e7bf55b41f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2e5d7fab-4ff6-48dd-a2b2-2b5a8c8a9192", "AQAAAAEAACcQAAAAEFNktXVJhlAwVvLbSADXP1Jssi4HK3DhwxDvCSJonepJm6fVl3vJpzNs41ALMgfFNQ==" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineMindApi.Migrations
{
    public partial class alimentarTablaPlants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "PlantId", "Name" },
                values: new object[] { "EDI00226", "Planta de produccion 1 " });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "PlantId", "Name" },
                values: new object[] { "EDI00228", "Planta de produccion 2 " });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "PlantId",
                keyValue: "EDI00226");

            migrationBuilder.DeleteData(
                table: "Plants",
                keyColumn: "PlantId",
                keyValue: "EDI00228");
        }
    }
}

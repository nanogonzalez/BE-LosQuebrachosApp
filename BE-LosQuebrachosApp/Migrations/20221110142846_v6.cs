using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_LosQuebrachosApp.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choferes_Transportes_TransporteId",
                table: "Choferes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Transportes_TransporteId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_TransporteId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Choferes_TransporteId",
                table: "Choferes");

            migrationBuilder.DropColumn(
                name: "TransporteId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "TransporteId",
                table: "Choferes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransporteId",
                table: "Vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransporteId",
                table: "Choferes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_TransporteId",
                table: "Vehiculos",
                column: "TransporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Choferes_TransporteId",
                table: "Choferes",
                column: "TransporteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choferes_Transportes_TransporteId",
                table: "Choferes",
                column: "TransporteId",
                principalTable: "Transportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Transportes_TransporteId",
                table: "Vehiculos",
                column: "TransporteId",
                principalTable: "Transportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

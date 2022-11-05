using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_LosQuebrachosApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Choferes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choferes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesDeCargas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinoCarga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinoDescarga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaHoraCarga = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMercaderia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesDeCargas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chasis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acoplado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacidadTN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choferes");

            migrationBuilder.DropTable(
                name: "OrdenesDeCargas");

            migrationBuilder.DropTable(
                name: "Transportes");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}

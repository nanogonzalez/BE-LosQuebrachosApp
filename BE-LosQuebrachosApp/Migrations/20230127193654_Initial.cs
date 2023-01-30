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
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinosDeDescarga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false),
                    NombreEstablecimiento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinosDeDescarga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinosDeCarga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false),
                    NombreEstablecimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinosDeCarga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinosDeCarga_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choferes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choferes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choferes_Transportes_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CapacidadTN = table.Column<int>(type: "int", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Transportes_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesDeCargas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinoDeCargaId = table.Column<int>(type: "int", nullable: false),
                    DestinoDeDescargaId = table.Column<int>(type: "int", nullable: false),
                    DistanciaViaje = table.Column<int>(type: "int", nullable: false),
                    DiaHoraCarga = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMercaderia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesDeCargas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesDeCargas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesDeCargas_DestinosDeCarga_DestinoDeCargaId",
                        column: x => x.DestinoDeCargaId,
                        principalTable: "DestinosDeCarga",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesDeCargas_DestinosDeDescarga_DestinoDeDescargaId",
                        column: x => x.DestinoDeDescargaId,
                        principalTable: "DestinosDeDescarga",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdenesDeGasoil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: false),
                    ChoferId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    Litros = table.Column<int>(type: "int", nullable: false),
                    Estacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesDeGasoil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesDeGasoil_Choferes_ChoferId",
                        column: x => x.ChoferId,
                        principalTable: "Choferes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesDeGasoil_Transportes_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transportes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesDeGasoil_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choferes_TransporteId",
                table: "Choferes",
                column: "TransporteId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinosDeCarga_ClienteId",
                table: "DestinosDeCarga",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCargas_ClienteId",
                table: "OrdenesDeCargas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCargas_DestinoDeCargaId",
                table: "OrdenesDeCargas",
                column: "DestinoDeCargaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCargas_DestinoDeDescargaId",
                table: "OrdenesDeCargas",
                column: "DestinoDeDescargaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeGasoil_ChoferId",
                table: "OrdenesDeGasoil",
                column: "ChoferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeGasoil_TransporteId",
                table: "OrdenesDeGasoil",
                column: "TransporteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeGasoil_VehiculoId",
                table: "OrdenesDeGasoil",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_TransporteId",
                table: "Vehiculos",
                column: "TransporteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenesDeCargas");

            migrationBuilder.DropTable(
                name: "OrdenesDeGasoil");

            migrationBuilder.DropTable(
                name: "DestinosDeCarga");

            migrationBuilder.DropTable(
                name: "DestinosDeDescarga");

            migrationBuilder.DropTable(
                name: "Choferes");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Transportes");
        }
    }
}

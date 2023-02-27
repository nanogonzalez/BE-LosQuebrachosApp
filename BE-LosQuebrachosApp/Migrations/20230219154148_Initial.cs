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
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    NombreEstablecimiento = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordExpiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinosDeCarga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false),
                    NombreEstablecimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinosDeCarga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinosDeCarga_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Choferes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cuit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransporteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choferes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choferes_Transportes_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transportes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acoplado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacidadTN = table.Column<int>(type: "int", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Transportes_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transportes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdenesDeCargas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinoDeCargaId = table.Column<int>(type: "int", nullable: true),
                    DestinoDeDescargaId = table.Column<int>(type: "int", nullable: true),
                    DistanciaViaje = table.Column<int>(type: "int", nullable: false),
                    DiaHoraCarga = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMercaderia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
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
                    NumeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: true),
                    ChoferId = table.Column<int>(type: "int", nullable: true),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    Litros = table.Column<int>(type: "int", nullable: false),
                    Estacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Usuarios");

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

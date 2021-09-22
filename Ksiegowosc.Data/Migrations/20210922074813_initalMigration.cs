using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksiegowosc.Data.Migrations
{
    public partial class initalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adres",
                columns: table => new
                {
                    IdAdresu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Miasto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodPocztowy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adres", x => x.IdAdresu);
                });

            migrationBuilder.CreateTable(
                name: "Dokumenty",
                columns: table => new
                {
                    IdDokumentu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaDokumentu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlDokumentu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenty", x => x.IdDokumentu);
                });

            migrationBuilder.CreateTable(
                name: "Kontrachenci",
                columns: table => new
                {
                    IdKontrachenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NipLubPesel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlatnikVat = table.Column<bool>(type: "bit", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkrotNazwy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dostawca = table.Column<bool>(type: "bit", nullable: false),
                    Odbiorca = table.Column<bool>(type: "bit", nullable: false),
                    Zalezny = table.Column<bool>(type: "bit", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumerKonta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAdresu = table.Column<int>(type: "int", nullable: false),
                    AdresIdAdresu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrachenci", x => x.IdKontrachenta);
                    table.ForeignKey(
                        name: "FK_Kontrachenci_Adres_AdresIdAdresu",
                        column: x => x.AdresIdAdresu,
                        principalTable: "Adres",
                        principalColumn: "IdAdresu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kontrachenci_AdresIdAdresu",
                table: "Kontrachenci",
                column: "AdresIdAdresu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokumenty");

            migrationBuilder.DropTable(
                name: "Kontrachenci");

            migrationBuilder.DropTable(
                name: "Adres");
        }
    }
}

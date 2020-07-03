﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class AddAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    IdBuilding = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.IdBuilding);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Campaings",
                columns: table => new
                {
                    IdCampaign = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PricePerSquareMeter = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    FromIdBuilding = table.Column<int>(nullable: false),
                    ToIdBuilding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaings", x => x.IdCampaign);
                    table.ForeignKey(
                        name: "FK_Campaings_Buildings_FromIdBuilding",
                        column: x => x.FromIdBuilding,
                        principalTable: "Buildings",
                        principalColumn: "IdBuilding",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campaings_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campaings_Buildings_ToIdBuilding",
                        column: x => x.ToIdBuilding,
                        principalTable: "Buildings",
                        principalColumn: "IdBuilding",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    IdAdvertisement = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    IdCampaign = table.Column<int>(nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.IdAdvertisement);
                    table.ForeignKey(
                        name: "FK_Banners_Campaings_IdCampaign",
                        column: x => x.IdCampaign,
                        principalTable: "Campaings",
                        principalColumn: "IdCampaign",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_IdCampaign",
                table: "Banners",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Campaings_FromIdBuilding",
                table: "Campaings",
                column: "FromIdBuilding");

            migrationBuilder.CreateIndex(
                name: "IX_Campaings_IdClient",
                table: "Campaings",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Campaings_ToIdBuilding",
                table: "Campaings",
                column: "ToIdBuilding");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Campaings");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

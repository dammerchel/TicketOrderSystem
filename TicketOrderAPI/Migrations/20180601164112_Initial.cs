using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TicketOrderAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartureAirport = table.Column<string>(nullable: true),
                    DestinationAirport = table.Column<string>(nullable: true),
                    FlightDate = table.Column<DateTime>(nullable: false),
                    FlightDescription = table.Column<string>(nullable: true),
                    FlightName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}

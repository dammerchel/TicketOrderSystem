using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TicketOrderAPI.Migrations.Ticket
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientID = table.Column<int>(nullable: false),
                    FlightID = table.Column<int>(nullable: false),
                    TicketDescription = table.Column<string>(nullable: true),
                    TicketName = table.Column<int>(nullable: false),
                    TicketPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");
        }
    }
}

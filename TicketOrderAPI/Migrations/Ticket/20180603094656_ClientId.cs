using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TicketOrderAPI.Migrations.Ticket
{
    public partial class ClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Ticket",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClientID",
                table: "Ticket",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

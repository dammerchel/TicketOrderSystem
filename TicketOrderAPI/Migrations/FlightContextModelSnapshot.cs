﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TicketOrderAPI.Model;

namespace TicketOrderAPI.Migrations
{
    [DbContext(typeof(FlightContext))]
    partial class FlightContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TicketOrderAPI.Model.Flight", b =>
                {
                    b.Property<int>("FlightID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DepartureAirport");

                    b.Property<string>("DestinationAirport");

                    b.Property<DateTime>("FlightDate");

                    b.Property<string>("FlightDescription");

                    b.Property<int>("FlightName");

                    b.HasKey("FlightID");

                    b.ToTable("Flight");
                });
#pragma warning restore 612, 618
        }
    }
}

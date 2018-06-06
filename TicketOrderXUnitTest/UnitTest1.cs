using System;
using Xunit;
using TicketOrderAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace TicketOrderXUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()             //Test sprawdzaj¹cy poprawne usuniêcie obiektu Ticket z bazy danych przez metodê DeleteFlight()
        {
            Ticket testTicket = new Ticket();
            int ticketID = 0;
            Flight flight = new Flight
            {
                FlightID=25,    
                FlightName=FlightType.Boeing737,
                FlightDescription="Lot testowy",
                FlightDate=new DateTime(2018,6,3),
                DestinationAirport="Edinburgh",
                DepartureAirport ="Radom"
            };
            var optionsBuilder = new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ticket-1;Trusted_Connection=True;MultipleActiveResultSets=true");
            TicketContext services = new TicketContext(optionsBuilder.Options);           
            
            ticketID = flight.DeleteFlight();
            services.Find<Ticket>(ticketID);
            Assert.Null(testTicket.TicketDescription);
        }
        [Fact]
        public void Test2()             //Test sprawdzaj¹cy poprawne utworzenie instancji do komunikacji z baz¹ danych przez metodê CreateFlight
        {
            Flight flight = new Flight
            {
                FlightID = 25,
                FlightName = FlightType.Boeing737,
                FlightDescription = "Lot testowy",
                FlightDate = new DateTime(2018, 6, 3),
                DestinationAirport = "Edinburgh",
                DepartureAirport = "Radom"
            };
            TicketContext services = flight.CreateFlight();

            Assert.IsNotType<DbContext>(services);
            Assert.IsType<TicketContext>(services);

        }

        [Fact]
        public void Test3()             //Test sprawdzaj¹cy poprawne utworzenie instancji do komunikacji z baz¹ danych przez metodê CreateFlight
        {
            Flight flight = new Flight
            {
                FlightID = 25,
                FlightName = FlightType.Boeing737,
                FlightDescription = "Lot testowy",
                FlightDate = new DateTime(2018, 6, 3),
                DestinationAirport = "Edinburgh",
                DepartureAirport = "Radom"
            };
            Ticket testTicket = Ticket.CreateTicket(flight, TicketType.Business, 9);

            Assert.NotNull(testTicket);
            Assert.IsNotType<string>(testTicket.TicketName);
            Assert.IsType<TicketType>(testTicket.TicketName);
            Assert.Equal(8000M, testTicket.TicketPrice);
            Assert.Equal(9, testTicket.TicketSeat);

        }
    }
}

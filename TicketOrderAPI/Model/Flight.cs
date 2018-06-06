using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketOrderAPI.Model;
using System.Data.Entity;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace TicketOrderAPI.Model
{
    public class Flight                                 //Instacje Flight to kolejne loty realizowane przez przewoźnika. Każdemu lotowi odpowiada zestaw biletów, które mogą kupić klienci.
    {
        [Display(Name = "ID lotu")]
        public int FlightID { get; set; }               //Identyfikator lotu
        [Display(Name = "Typ samolotu")]
        public FlightType FlightName { get; set; }      //Typ samolotu, jakim odbędzie się lotu. Od typu samolotu zależy, ile i jakie bilety będą dostepne do kupienia.
        [Display(Name = "Opis lotu")]
        public string FlightDescription { get; set; }   //Krótki opis lotu
        [Display(Name = "Data lotu")]
        [DataType(DataType.Date)]
        public DateTime FlightDate { get; set; }        //Data lotu
        [Display(Name = "Lotnisko docelowe")]
        public string DestinationAirport { get; set; }  //Lotnisko docelowe
        [Display(Name = "Lotnisko wylotu")]
        public string DepartureAirport { get; set; }    //Lotnisko wylotu
        

        public TicketContext CreateFlight()                      //Metoda tworząca zestaw biletów odpowiadających lotowi. Ilość i rodzaj tworzonych biletów zależy od typu samolotu. Bilety zapisywane są w odpowiedniej dla nich bazie danych Ticket.
        {
            int EconomicTickets=0;
            int BusinessTickets=0;
            int FirstClassTickets=0;
            var optionsBuilder=new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ticket-1;Trusted_Connection=True;MultipleActiveResultSets=true");            
            TicketContext services = new TicketContext(optionsBuilder.Options);
            
            if(FlightName.Equals(FlightType.Boeing787))
            {
                EconomicTickets = 5;
                BusinessTickets = 2;
                FirstClassTickets = 1;
            }
            else if(FlightName.Equals(FlightType.Boeing737))
            {
                EconomicTickets = 4;
                BusinessTickets = 5;
                FirstClassTickets = 5;
            }
            else if(FlightName.Equals(FlightType.Embraer))
            {
                EconomicTickets = 3;
                BusinessTickets = 2;
                FirstClassTickets = 1;
            }

            for (int i = 1; i <= EconomicTickets; i++)
            {
                
                
                services.Add(Ticket.CreateTicket( this, TicketType.Economic, i));
            }
            for (int i = 1; i <= BusinessTickets; i++)
            {
                services.Add(Ticket.CreateTicket( this, TicketType.Business, i));
            }
            for (int i = 1; i <= FirstClassTickets; i++)
            {
                services.Add(Ticket.CreateTicket(this, TicketType.FirstClass, i));
            }

            services.SaveChanges();
            return services;
            
        }
        
        public int DeleteFlight()                  //Metoda usuwajaca zestaw biletów odpowiadających określonemu lotowi. Wywoływana przy kasowaniu lotu. Usuwa obiekty z bazy danych Ticket.
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ticket-1;Trusted_Connection=True;MultipleActiveResultSets=true");
            TicketContext services = new TicketContext(optionsBuilder.Options);
            int testTicket = 0;
            foreach(var item in services.Ticket)
            {
                if (item.FlightID == this.FlightID)
                {
                    testTicket = item.TicketID;
                    services.Remove(item);                    
                }
                

            }
            services.SaveChanges();
            return testTicket;
        }



    }

    public enum FlightType          //Zbiór typów samolotów, jakimi może odbyć się lot. 
    {
        Boeing787=1,
        Boeing737=2,
        Embraer=3
    }

    
}

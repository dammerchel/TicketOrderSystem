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
    /// <summary>
    /// Instacje klasy Flight to kolejne loty realizowane przez przewoźnika. Każdemu lotowi odpowiada zestaw biletów, które mogą kupić klienci.
    /// </summary>
    public class Flight                                 
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
        

        /// <summary>
        /// Metoda tworząca zestaw biletów odpowiadających lotowi. Ilość i rodzaj tworzonych biletów zależy od typu samolotu. Bilety zapisywane są w odpowiedniej dla nich bazie danych Ticket.
        /// </summary>
        /// <returns></returns>
        public TicketContext CreateFlight()                      
        {
            int economicTickets=0;
            int businessTickets=0;
            int firstClassTickets=0;
            var optionsBuilder=new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ticket-1;Trusted_Connection=True;MultipleActiveResultSets=true");            
            TicketContext services = new TicketContext(optionsBuilder.Options);

            switch (FlightName)     //W zależności od rodzaju samolotu zostanie utworzony zestaw biletów zawierający określone ilości biletów danego typu. Ilości biletów ograniczono dla zachowania przejrzystości strony FlightTickets.
            {

                case FlightType.Boeing787:
                    economicTickets = 5;
                    businessTickets = 2;
                    firstClassTickets = 1;
                    break;

                case FlightType.Boeing737:
                    economicTickets = 4;
                    businessTickets = 5;
                    firstClassTickets = 5;
                    break;

                case FlightType.Embraer:
                    economicTickets = 3;
                    businessTickets = 2;
                    firstClassTickets = 1;
                    break;                   
            }

            for (int i = 1; i <= economicTickets; i++)
            {      
                services.Add(Ticket.CreateTicket( this, TicketType.Economic, i));
            }
            for (int i = 1; i <= businessTickets; i++)
            {
                services.Add(Ticket.CreateTicket( this, TicketType.Business, i));
            }
            for (int i = 1; i <= firstClassTickets; i++)
            {
                services.Add(Ticket.CreateTicket(this, TicketType.FirstClass, i));
            }
            services.SaveChanges();
            return services;            
        }

        /// <summary>
        /// Metoda usuwajaca zestaw biletów odpowiadających określonemu lotowi. Wywoływana przy kasowaniu lotu. Usuwa obiekty z bazy danych Ticket.
        /// </summary>
        /// <returns></returns>
        public int DeleteFlight()                  
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

    /// <summary>
    /// Zbiór typów samolotów, jakimi może odbyć się lot. 
    /// </summary>
    public enum FlightType          
    {
        Boeing787=1,
        Boeing737=2,
        Embraer=3
    }

    
}

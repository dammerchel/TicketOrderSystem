using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketOrderAPI.Data;

namespace TicketOrderAPI.Model
{
    public class Ticket
    {
        [Display(Name = "ID biletu")]
        public int TicketID { get; set; }               //ID biletu
        [Display(Name = "Typ biletu")]
        public TicketType TicketName { get; set; }      //Typ biletu: Business, Economic, FirstClass   
        [Display(Name = "Cena biletu")]
        public decimal TicketPrice { get; set; }        //Cena biletu
        [Display(Name = "Opis biletu")]
        public string TicketDescription { get; set; }   //Krótki opis biletu
        [Display(Name = "ID lotu")]
        public int FlightID { get; set; }               //ID lotu
        [Display(Name = "ID klienta")]
        public string ClientID { get; set; }            //ID klienta, który kupił bilet
        [Display(Name = "Numer siedzenia")]
        public int TicketSeat { get; set; }             //Numer miejsca w samolocie

        public static Ticket CreateTicket(Flight flight, TicketType ticketName, int ticketSeat)
        {
            Ticket ticket = null; ;


            if (ticketName.Equals(TicketType.FirstClass))
            {
                ticket = new Ticket
                {
                    TicketName = ticketName,
                    TicketPrice = 20000M,
                    TicketDescription = "Najwyższa jakość jaką można sobie wyobrazić." +
                    " Zapewniamy osobną lożę, posiłek przygotowany przez najlepszych kucharzy " +
                    "według twojego życzenia oraz wszelkie wygody, jakich zapragniesz. " +
                    "Korzystanie z wygód możesz rozpocząć już na lotnisku, w specjalnie przygotowanej loży VIP.",
                    FlightID = flight.FlightID,
                    TicketSeat = ticketSeat
                };
            }
            else if (ticketName.Equals(TicketType.Business))
            {
                ticket=new Ticket
                {
                    TicketName = ticketName,
                    TicketPrice = 8000M,
                    TicketDescription = "Zapewnij sobie przestrzeń, na którą zasługujesz! Wygodne, szerokie fotele, szeroki wybór potraw i dostęp do mediów. Korzystanie z wygód możesz rozpocząć już na lotnisku, w specjalnie przygotowanej loży VIP.",
                    FlightID = flight.FlightID,
                    TicketSeat = ticketSeat
                };
            }
            else if (ticketName.Equals(TicketType.Economic))
            {
                ticket=new Ticket
                {
                    TicketName = ticketName,
                    TicketPrice = 2000M,
                    TicketDescription = "Komfortowa podróż w komfortowej cenie. Zapewniamy regulowane siedzenia, przekąski, dostęp do mediów, a na dłuższych trasach darmowy posiłek.",
                    FlightID = flight.FlightID,
                    TicketSeat = ticketSeat
                };
            }
            return ticket;

            
            

        }
        
        
    
    }



    public enum TicketType      //Typy oferowanych biletów. Typ biletu decyduje o jego cenie i oferowanych usługach.
    {
        Economic = 1,
        Business = 2,
        FirstClass = 3
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOrderAPI.Model;

namespace TicketOrderAPI.Pages.Flights
{
    public class FlightTicketsModel : PageModel
    {
        
        private readonly TicketOrderAPI.Model.TicketContext _context;
        private readonly TicketOrderAPI.Model.FlightContext _flightcontext;

        public FlightTicketsModel(TicketOrderAPI.Model.TicketContext ticketContext, TicketOrderAPI.Model.FlightContext flightcontext)
        {            
            _context = ticketContext;
            _flightcontext = flightcontext;
        }

        public Flight Flight { get; set; }

        public IList<Ticket> Ticket { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Flight = await _flightcontext.Flight.SingleOrDefaultAsync(m => m.FlightID == id);
            Ticket = await _context.Ticket.ToListAsync();
        }
    }
}
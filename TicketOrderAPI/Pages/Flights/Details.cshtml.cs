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
    public class DetailsModel : PageModel
    {
        private readonly TicketOrderAPI.Model.FlightContext _context;

        public DetailsModel(TicketOrderAPI.Model.FlightContext context)
        {
            _context = context;
        }

        public Flight Flight { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flight = await _context.Flight.SingleOrDefaultAsync(m => m.FlightID == id);

            if (Flight == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

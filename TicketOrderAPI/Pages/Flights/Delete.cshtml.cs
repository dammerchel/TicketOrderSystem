using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOrderAPI.Model;

namespace TicketOrderAPI.Pages.Flights
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly TicketOrderAPI.Model.FlightContext _context;

        public DeleteModel(TicketOrderAPI.Model.FlightContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flight = await _context.Flight.FindAsync(id);

            if (Flight != null)
            {
                Flight.DeleteFlight();                      //Usuwanie zestawu biletów odpowiadajacego usuniêtemu lotowi
                _context.Flight.Remove(Flight);
                await _context.SaveChangesAsync();
                
            }

            return RedirectToPage("./Employees");
        }
    }
}

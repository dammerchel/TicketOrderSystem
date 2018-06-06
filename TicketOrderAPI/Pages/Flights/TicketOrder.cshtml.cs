using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketOrderAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using TicketOrderAPI.Data;

namespace TicketOrderAPI.Pages.Flights
{
    [Authorize]
    public class TicketOrderModel : PageModel
    {
        private readonly TicketOrderAPI.Model.TicketContext _context;
        

        public TicketOrderModel(TicketOrderAPI.Model.TicketContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.TicketID == id);

            if (Ticket == null)
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

            Ticket = await _context.Ticket.FindAsync(id);
            
            if (Ticket != null)
            {
                string username=User.Identity.Name;
                Ticket.ClientID = username;
                await _context.SaveChangesAsync();

            }

            return RedirectToPage("/Account/Manage/TicketsList");
        }
    }
}
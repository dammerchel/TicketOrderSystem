using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketOrderAPI.Model;

namespace TicketOrderAPI.Pages.Account.Manage
{
    public class TicketsListModel : PageModel
    {
        private readonly TicketOrderAPI.Model.TicketContext _context;        

        public TicketsListModel(TicketOrderAPI.Model.TicketContext ticketContext)
        {
            _context = ticketContext;            
        }
               

        public IList<Ticket> Ticket { get; set; }

        public async Task OnGetAsync(int? id)
        {
            
            Ticket = await _context.Ticket.ToListAsync();
        }
    }
}
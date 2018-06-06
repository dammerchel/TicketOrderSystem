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
    public class EmployeesModel : PageModel
    {
        
        
            private readonly TicketOrderAPI.Model.FlightContext _context;

            public EmployeesModel(TicketOrderAPI.Model.FlightContext context)
            {
                _context = context;
            }

            public IList<Flight> Flight { get; set; }

            public async Task OnGetAsync()
            {
                Flight = await _context.Flight.ToListAsync();
            }
        
    }
}
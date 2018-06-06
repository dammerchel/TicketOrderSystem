using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketOrderAPI.Model;
using Microsoft.Extensions.DependencyInjection;
using TicketOrderAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TicketOrderAPI.Pages.Flights
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly TicketOrderAPI.Model.FlightContext _context;

        public CreateModel(TicketOrderAPI.Model.FlightContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Flight Flight { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Flight.Add(Flight);
            await _context.SaveChangesAsync();

            Flight.CreateFlight();              //Tworzenie zestawu biletów odpowiadajacego stworzonemu lotowi

            return RedirectToPage("./Employees");
        }
    }
}
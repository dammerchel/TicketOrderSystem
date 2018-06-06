using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOrderAPI.Model
{
    public class FlightContext:DbContext
    {
                    public FlightContext(DbContextOptions<FlightContext> options)
                    : base(options)
            {
            }
        

            public DbSet<Flight> Flight { get; set; }
        }
    
}

﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOrderAPI.Model
{
    
        public class TicketContext : DbContext
        {
            public TicketContext(DbContextOptions<TicketContext> options)
                    : base(options)
            {
            }
        
            public DbSet<Ticket> Ticket { get; set; }
        }
    
}

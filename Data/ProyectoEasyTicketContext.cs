using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoEasyTicket.Models;

namespace ProyectoEasyTicket.Data
{
    public class ProyectoEasyTicketContext : DbContext
    {
        public ProyectoEasyTicketContext (DbContextOptions<ProyectoEasyTicketContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoEasyTicket.Models.Ticket> Ticket { get; set; } = default!;
    }
}

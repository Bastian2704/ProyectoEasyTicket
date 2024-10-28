using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoEasyTicket.Data;
using ProyectoEasyTicket.Models;

namespace ProyectoEasyTicket.Controllers
{
    public class Tickets1Controller : Controller
    {
        private readonly ProyectoEasyTicketContext _context;

        public Tickets1Controller(ProyectoEasyTicketContext context)
        {
            _context = context;
        }

        // GET: Tickets1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticket.ToListAsync());
        }



        // GET: Tickets1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Evento,Fecha,Lugar,ButacaSeccion,Precio,Telefono,Vendido,Contrasenia")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketID,Evento,Fecha,Lugar,ButacaSeccion,Precio,Telefono,Vendido,Contrasenia")] Ticket ticket)
        {
            if (id != ticket.TicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketID == id);
        }

        public async Task<IActionResult> ConfirmarClave(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.TicketID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }


        // POST: Confirmar Clave

        [HttpPost]
        public IActionResult ConfirmarClave(int ticketId, string contra)
        {
            var ticket = _context.Ticket.FirstOrDefault(t => t.TicketID == ticketId);
            if (ticket == null)
            {
                return NotFound("Ticket no encontrado.");
            }

            // Validar la contraseña
            if (ticket.Contrasenia == contra)
            {
                // Aquí puedes redirigir al usuario a la página de edición o eliminación
                return RedirectToAction("Edit", new { id = ticketId });
            }
            else
            {
                // Si la contraseña es incorrecta
                ModelState.AddModelError(string.Empty, "La contraseña es incorrecta.");
                return View(); 
            }
        }

        [HttpPost]
        public IActionResult Comprar(Ticket ticket)
        {
             if (ModelState.IsValid)
            {
                ticket.Vendido = true;

                _context.Update(ticket);
                _context.SaveChanges();

               
                return RedirectToAction("ConfirmarCompra", new { id = ticket.TicketID });
            }

            return View(ticket); 
        }

        public IActionResult ConfirmarCompra(int id)
        {
            var ticket = _context.Ticket.Find(id); 
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }



    }
}

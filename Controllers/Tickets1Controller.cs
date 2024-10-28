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
        public IActionResult ConfirmarClave(int id)
        {
            var ticket = _context.Ticket.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View();
        }


        // POST: Confirmar Clave
        [HttpPost]
        public IActionResult ConfirmarClave(int id, string contra, string action)
        {
            var ticket = _context.Ticket.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Verifica la clave del ticket
            if (ticket.Contrasenia == contra)
            {

                return View(Delete);

            }

            // Clave incorrecta, muestra error
            ModelState.AddModelError(string.Empty, "Clave incorrecta.");
            return View(ticket);
        }
        public async Task<IActionResult> Comprar(int? id)
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

        [HttpPost]
        public IActionResult Comprar(Ticket ticket)
        {
             if (ModelState.IsValid)
            {
                // Lógica para marcar el ticket como vendido
                ticket.Vendido = true;

                // Guardar el ticket en la base de datos (asegúrate de tener el contexto)
                _context.Update(ticket);
                _context.SaveChanges();

                // Redirigir a la vista de confirmación con la contraseña
                return RedirectToAction("ConfirmarCompra", new { id = ticket.TicketID });
            }

            return View(ticket); // Retorna la vista con el modelo si hay errores
        }

        public IActionResult ConfirmarCompra(int id)
        {
            var ticket = _context.Ticket.Find(id); // O usa cualquier lógica para obtener el ticket
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }



    }
}

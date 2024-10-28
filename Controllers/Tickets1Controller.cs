using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> ConfirmarClave(int id, string clave)
        {
            var ticket = _context.Ticket.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Verifica la clave del ticket
            if (ticket.Contrasenia == clave)
            {

                var ticketParaEditar = await _context.Ticket.FindAsync(id);
                return View("Edit", ticketParaEditar);

            }

            // Clave incorrecta, muestra error
            ModelState.AddModelError("", "La clave ingresada es incorrecta. Por favor, intente nuevamente.");
            return View(ticket);
        }
        public async Task<IActionResult> ConfirmarClave2(int? id)
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
        public async Task<IActionResult> ConfirmarClave2(int id, string clave)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Verifica la clave del ticket
            if (ticket.Contrasenia == clave)
            {
                TempData["ClaveValidada"] = true;
                return RedirectToAction("Delete", new { id });

            }

            // Clave incorrecta, muestra error
            ModelState.AddModelError("", "La clave ingresada es incorrecta. Por favor, intente nuevamente.");
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
        public IActionResult Comprar(int ticketID)
        {
            Debug.WriteLine("El botón de comprar fue presionado.");

            var ticket = _context.Ticket.Find(ticketID);

            if (ticket == null)
            {
                return NotFound(); 
            }
            ticket.Vendido = true;


                _context.Update(ticket);
                _context.SaveChanges();

              

                TempData["SuccessMessage"] = "¡Venta exitosa! Gracias por su compra.";

                return RedirectToAction("ConfirmacionVenta");

        
        }

        public IActionResult ConfirmacionVenta()
        {
            return View();
        }



    }
}

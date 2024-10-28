using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEasyTicket.Data;
using ProyectoEasyTicket.Models;
using System.Diagnostics;

namespace ProyectoEasyTicket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProyectoEasyTicketContext _context;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ProyectoEasyTicketContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
     
                 return View(await _context.Ticket.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

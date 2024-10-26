using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ProyectoEasyTicket.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string? Evento { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Lugar { get; set; }
        public string? ButacaSeccion { get; set; }
        public decimal? Precio { get; set; }

        public string? Usuario { get; set; } 
        public bool Vendido { get; set; }

    }
}

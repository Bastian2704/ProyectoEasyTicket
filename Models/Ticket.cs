using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace ProyectoEasyTicket.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        [Required]
        public string? Evento { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Lugar { get; set; }
        public string? ButacaSeccion { get; set; }
        public decimal? Precio { get; set; }

        public string? Telefono { get; set; } 
        public bool Vendido { get; set; }
        public string? Contrasenia { get; set; }
        
        public List<Reviews>? Reviewss {  get; set; }
    }
}

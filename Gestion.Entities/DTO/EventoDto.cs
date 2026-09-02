using System.ComponentModel.DataAnnotations;
namespace Gestion.Entities.DTO
{
    public class EventoDto
    {
        public int Codigo { get; set; }
        [Required, StringLength(100, MinimumLength = 5)]
        public string NombreEvento { get; set; } = string.Empty;
        [Required]
        public DateTime FechaEvento { get; set; }
        [Required, StringLength(100, MinimumLength = 5)]
        public string LugarEvento { get; set; } = string.Empty;
    }
}
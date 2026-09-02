using System.ComponentModel.DataAnnotations;
namespace Gestion.Entities.DTO
{
    public class ParticipanteDto
    {
        public int Codigo { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string NombreParticipante { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int EventoId { get; set; }
    }
}
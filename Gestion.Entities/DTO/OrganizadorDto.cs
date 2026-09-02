using System.ComponentModel.DataAnnotations;
namespace Gestion.Entities.DTO
{
    public class OrganizadorDto
    {
        public int Codigo { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string NombreOrganizador { get; set; } = string.Empty;
        [Required, StringLength(50, MinimumLength = 3)]
        public string Cargo { get; set; } = string.Empty;
        [Required]
        public int EventoId { get; set; }
    }
}
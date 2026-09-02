namespace Gestion.Entities.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Email { get; set; }
        public int EventoId { get; set; }
    }
}
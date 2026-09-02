namespace Gestion.Entities.Models
{
    public class Organizador
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Cargo { get; set; }
        public int EventoId { get; set; }
    }
}
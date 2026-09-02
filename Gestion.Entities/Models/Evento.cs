namespace Gestion.Entities.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required DateTime Fecha { get; set; }
        public required string Lugar { get; set; }
    }
}
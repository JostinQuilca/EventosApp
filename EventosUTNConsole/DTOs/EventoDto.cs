namespace EventosUTN.EventosUTNConsole.DTOs
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public short TipoEventoId { get; set; } // Debe ser short
    }
}
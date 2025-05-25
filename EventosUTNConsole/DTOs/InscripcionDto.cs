namespace EventosUTN.EventosUTNConsole.DTOs
{
    public class InscripcionDto
    {
        public int Id { get; set; }
        public int ParticipanteId { get; set; }
        public int EventoId { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public short EstadoId { get; set; } // Debe ser short
    }
}
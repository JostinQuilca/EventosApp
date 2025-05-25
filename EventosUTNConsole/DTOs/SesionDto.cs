namespace EventosUTN.EventosUTNConsole.DTOs
{
    public class SesionDto
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int SalaId { get; set; }
        public string Titulo { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
    }
}
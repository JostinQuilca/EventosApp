namespace EventosApp.Models;

public class Inscripcion
{
    public int Id { get; set; }
    public int EventoId { get; set; }
    public Evento? Evento { get; set; }
    public int ParticipanteId { get; set; }
    public Participante? Participante { get; set; }
    public int EstadoId { get; set; }
    public EstadoInscripcion? Estado { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public Pago? Pago { get; set; }
    public Certificado? Certificado { get; set; }
}
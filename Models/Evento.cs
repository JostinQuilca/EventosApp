namespace EventosApp.Models;

public class Evento
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public string? Lugar { get; set; }
    public int TipoEventoId { get; set; }
    public TipoEvento? TipoEvento { get; set; }
    public ICollection<Inscripcion>? Inscripciones { get; set; }
    public ICollection<Sesion>? Sesiones { get; set; }
    public ICollection<EventoPonente>? EventoPonentes { get; set; }
}
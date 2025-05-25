namespace EventosApp.Models;

public class EventoPonente
{
    public int EventoId { get; set; }
    public Evento? Evento { get; set; }
    public int PonenteId { get; set; }
    public Ponente? Ponente { get; set; }
}
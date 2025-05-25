using EventosApp.Models;

namespace EventosApp.Models;

public class Sesion
{
    public int Id { get; set; }
    public int EventoId { get; set; }
    public Evento? Evento { get; set; }
    public int SalaId { get; set; }
    public Sala? Sala { get; set; }
    public string? Titulo { get; set; }
    public DateTime HoraInicio { get; set; }
    public DateTime HoraFin { get; set; }
}
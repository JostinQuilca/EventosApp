using EventosApp.Models;

namespace EventosApp.Models;

public class TipoEvento
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public ICollection<Evento>? Eventos { get; set; }
}
using EventosApp.Models;

namespace EventosApp.Models;

public class Participante
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public ICollection<Inscripcion>? Inscripciones { get; set; }
}
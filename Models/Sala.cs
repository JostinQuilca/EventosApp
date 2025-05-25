using EventosApp.Models;

namespace EventosApp.Models;

public class Sala
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Ubicacion { get; set; }
    public int Capacidad { get; set; }
}
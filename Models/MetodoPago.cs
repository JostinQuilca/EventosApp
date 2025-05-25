using EventosApp.Models;

namespace EventosApp.Models;

public class MetodoPago
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public ICollection<Pago>? Pagos { get; set; }
}
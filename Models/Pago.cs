using EventosApp.Models;

namespace EventosApp.Models;

public class Pago
{
    public int Id { get; set; }
    public int InscripcionId { get; set; }
    public Inscripcion? Inscripcion { get; set; }
    public int MetodoPagoId { get; set; }
    public MetodoPago? MetodoPago { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaPago { get; set; }
}
using EventosApp.Models;

namespace EventosApp.Models;

public class Certificado
{
    public int Id { get; set; }
    public int InscripcionId { get; set; }
    public Inscripcion? Inscripcion { get; set; }
    public string? UrlCertificado { get; set; }
    public DateTime FechaEmision { get; set; }
}
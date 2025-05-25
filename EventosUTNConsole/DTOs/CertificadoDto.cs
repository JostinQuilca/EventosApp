namespace EventosUTN.EventosUTNConsole.DTOs
{
    public class CertificadoDto
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public DateTime FechaEmision { get; set; }
        public string UrlCertificado { get; set; }
    }
}
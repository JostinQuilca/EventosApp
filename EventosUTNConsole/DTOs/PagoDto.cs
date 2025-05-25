namespace EventosUTN.EventosUTNConsole.DTOs
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public short MetodoPagoId { get; set; } // Debe ser short
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
    }
}
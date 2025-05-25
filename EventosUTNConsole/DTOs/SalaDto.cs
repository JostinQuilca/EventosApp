namespace EventosUTN.EventosUTNConsole.DTOs
{
    public class SalaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public short Capacidad { get; set; } // Debe ser short
    }
}
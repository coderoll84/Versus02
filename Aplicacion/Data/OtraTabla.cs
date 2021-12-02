namespace Aplicacion.Data
{
    public class OtraTabla
    {
        public int OtraTablaId { get; set; }
        public string? TextoOtra { get; set; }

        
        public int TablaId { get; set; }
        public Tabla Tabla { get; set; }
    }
}
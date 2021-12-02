namespace Aplicacion.Data
{
    public class TablaDto
    {
        public int TablaId { get; set; }
        public string? Texto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Moneda { get; set; }
        public bool Boleano { get; set; }
        public DateTime FechaHora { get; set; }
        public Guid Guid { get; set; }


        public int OtraTablaId { get; set; }
        public string? TextoOtra { get; set; }
    }
}
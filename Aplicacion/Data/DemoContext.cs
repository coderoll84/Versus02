using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tabla> Tabla { get; set; }
        public DbSet<OtraTabla> OtraTabla { get; set; }
    }
}
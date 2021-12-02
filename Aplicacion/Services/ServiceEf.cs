using Aplicacion.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Services
{
    public class ServiceEf : IServiceEf
    {
        private readonly DemoContext _context;

        public ServiceEf(DemoContext context)
        {
            _context = context;
        }

        public async Task<Tabla> Get(int id)
        {
            return await _context.Tabla
                .AsNoTracking().FirstOrDefaultAsync(x => x.TablaId == id).ConfigureAwait(false);
        }

        public async Task<List<Tabla>> GetSome(int from, int to)
        {
            return await _context.Tabla.Where(x => x.TablaId >= from && x.TablaId <= to)
                .AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TablaDto[]> GetDto(int _from, int to)
        {
            var result = await (from t in _context.Tabla
                        join o in _context.OtraTabla on t.TablaId equals o.TablaId
                        where o.TablaId >= _from && o.TablaId <= to
                        select new TablaDto
                        {
                            TablaId = t.TablaId,
                            Texto = t.Texto,
                            Fecha = t.Fecha,
                            Moneda = t.Moneda,
                            Boleano = t.Boleano,
                            FechaHora = t.FechaHora,
                            Guid = t.Guid,
                            OtraTablaId = o.OtraTablaId,
                            TextoOtra = o.TextoOtra
                        }).AsNoTracking().ToArrayAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<Tabla> GetSp(int id)
        {
            var param1 = new SqlParameter("@id", id);
            var tab = await _context.Tabla.FromSqlRaw("usp_get @id", param1)
                .AsNoTracking().AsAsyncEnumerable().FirstOrDefaultAsync().ConfigureAwait(false);
            return tab;
        }

        public async Task<List<Tabla>> GetSomeSp(int from, int to)
        {
            var param1 = new SqlParameter("@from", from);
            var param2 = new SqlParameter("@to", to);

            return await _context.Tabla.FromSqlRaw("usp_some @from, @to", param1, param2)
                .AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<TablaDto>> GetDtoSp(int from, int to)
        {
            var param1 = new SqlParameter("@from", from);
            var param2 = new SqlParameter("@to", to);
            var dtos = new List<TablaDto>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_someDto @from, @to";
                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                _context.Database.OpenConnection();

                using var result = command.ExecuteReader();
                while (await result.ReadAsync().ConfigureAwait(false))
                {
                    var tablaDto = new TablaDto();
                    tablaDto.TablaId = result.GetInt32(0);
                    tablaDto.Texto = result.GetString(1);
                    tablaDto.Fecha = result.GetDateTime(2);
                    tablaDto.Moneda = result.GetDecimal(3);
                    tablaDto.Boleano = result.GetBoolean(4);
                    tablaDto.FechaHora = result.GetDateTime(5);
                    tablaDto.Guid = result.GetGuid(6);
                    tablaDto.OtraTablaId = result.GetInt32(7);
                    tablaDto.TextoOtra = result.GetString(8);

                    dtos.Add(tablaDto);
                }
                // do something with result
            }

            return dtos;
        }
    }
}
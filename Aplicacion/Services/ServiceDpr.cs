using Aplicacion.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Aplicacion.Services
{
    public class ServiceDpr : IServiceDpr
    {
        private readonly IConfiguration _config;

        public ServiceDpr(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("dbConnection"));
            }
        }

        public async Task<Tabla> Get(int id)
        {
            string sql = @"	SELECT	TablaId,Texto,Fecha,Moneda,Boleano,FechaHora,Guid
	                        FROM	Tabla
	                        where	TablaId = @id";

            using var connection = Connection;
            {
                return await connection.QueryFirstOrDefaultAsync<Tabla>(sql, new { id }).ConfigureAwait(false);
            }
        }

        public async Task<List<Tabla>> GetSome(int from, int to)
        {
            string sql = @"	SELECT	TablaId,Texto,Fecha,Moneda,Boleano,FechaHora,Guid
	                        FROM	Tabla
	                        where	TablaId >= @From and TablaId <= @To";
            var parameters = new { From = from, To = to };
            using var connection = Connection;
            connection.Open();
            
            return (await connection.QueryAsync<Tabla>(sql, parameters).ConfigureAwait(false)).ToList();
        }

        public async Task<TablaDto[]> GetDto(int from, int to)
        {
            string sql = @"	SELECT	Tabla.TablaId,Texto,Fecha,Moneda ,Boleano,FechaHora,Guid
                                    ,OtraTablaId,TextoOtra
	                        FROM	Tabla
			                        inner join OtraTabla on Tabla.TablaId = OtraTabla.TablaId
	                        where	OtraTabla.TablaId >= @From
			                        and OtraTabla.TablaId <= @To";
            var parameters = new { From = from, To = to };
            using var connection = Connection;
            connection.Open();

            return (await connection.QueryAsync<TablaDto>(sql, parameters).ConfigureAwait(false)).ToArray();
        }

        public async Task<Tabla> GetSp(int id)
        {
            using var connection = Connection;
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<Tabla>("usp_get", new { id },
                            commandType: CommandType.StoredProcedure) 
            .ConfigureAwait(false);
        }

        public async Task<List<Tabla>> GetSomeSp(int from, int to)
        {
            var parameters = new { from, to };
            using var connection = Connection;
            connection.Open();

            return (await connection.QueryAsync<Tabla>("usp_some @from, @to", parameters).ConfigureAwait(false)).ToList();
        }

        public async Task<List<TablaDto>> GetDtoSp(int from, int to)
        {
            var parameters = new { from, to };
            using var connection = Connection;
            connection.Open();

            return (await connection.QueryAsync<TablaDto>("usp_someDto @from, @to", parameters).ConfigureAwait(false)).ToList();
        }
    }
}

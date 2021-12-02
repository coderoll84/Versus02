using Aplicacion.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoDb;
using System.Data;

namespace Aplicacion.Services
{
    public class ServiceRpo : IServiceRpo
    {
        private readonly IConfiguration _config;

        public ServiceRpo(IConfiguration config)
        {
            _config = config;
            SqlServerBootstrap.Initialize();
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("dbConnection"));
            }
        }

        public async Task<Tabla> Get(int id)
        {
            using var connection = Connection;
            return (await connection.QueryAsync<Tabla>(x => x.TablaId == id)).FirstOrDefault();
        }

        public async Task<List<Tabla>> GetSome(int from, int to)
        {
            using var connection = Connection;
            return (await connection
                .QueryAsync<Tabla>(x => x.TablaId >= from && x.TablaId <= to)).ToList();
        }

        public async Task<TablaDto[]> GetDto(int from, int to)
        {
            string sql = @"	SELECT	Tabla.TablaId,Texto,Fecha,Moneda ,Boleano,FechaHora,Guid
                                    ,OtraTablaId,TextoOtra
	                        FROM	Tabla
			                        inner join OtraTabla on Tabla.TablaId = OtraTabla.TablaId
	                        where	OtraTabla.TablaId >= @From
			                        and OtraTabla.TablaId <= @To";
            var param = new { from, to };

            using var connection = Connection;
            return (await connection.ExecuteQueryAsync<TablaDto>(sql, param)).ToArray();
        }

        public async Task<Tabla> GetSp(int id)
        {
            using var connection = Connection;
            return (await connection.ExecuteQueryAsync<Tabla>
                ("usp_get", new { id }, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                    .FirstOrDefault();
        }

        public async Task<List<Tabla>> GetSomeSp(int from, int to)
        {
            using var connection = Connection;
            return (await connection.ExecuteQueryAsync<Tabla>
                ("usp_some", new { from, to}, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                .ToList();
        }

        public async Task<List<TablaDto>> GetDtoSp(int from, int to)
        {
            using var connection = Connection;
            return (await connection.ExecuteQueryAsync<TablaDto>
                ("usp_someDto", new { from, to }, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                .ToList();
        }
    }
}
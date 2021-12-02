using Aplicacion.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Aplicacion.Services
{
    public class ServiceAdo : IServiceAdo
    {
        private readonly IConfiguration _config;

        public ServiceAdo(IConfiguration config)
        {
            _config = config;
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
            Tabla tabla = default;
            string sql = @"	SELECT	TablaId,Texto,Fecha,Moneda,Boleano,FechaHora,Guid
	                        FROM	Tabla
	                        where	TablaId = @id";

            using var connection = Connection;
            {
                using SqlCommand cmd = new(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();

                using SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                    tabla = new Tabla
                    {
                        TablaId = dr.GetInt32(0),
                        Texto = dr.GetString(1),
                        Fecha = dr.GetDateTime(2),
                        Moneda = dr.GetDecimal(3),
                        Boleano = dr.GetBoolean(4),
                        FechaHora = dr.GetDateTime(5),
                        Guid = dr.GetGuid(6)
                    };
                }
            }
            return tabla;
        }

        public async Task<List<Tabla>> GetSome(int from, int to)
        {
            var list = new List<Tabla>();
            string sql = @"	SELECT	TablaId,Texto,Fecha,Moneda,Boleano,FechaHora,Guid
	                        FROM	Tabla
	                        where	TablaId >= @From and TablaId <= @To";

            using var connection = Connection;
            {
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                await connection.OpenAsync();

                using SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new Tabla
                    {
                        TablaId = dr.GetInt32(0),
                        Texto = dr.GetString(1),
                        Fecha = dr.GetDateTime(2),
                        Moneda = dr.GetDecimal(3),
                        Boleano = dr.GetBoolean(4),
                        FechaHora = dr.GetDateTime(5),
                        Guid = dr.GetGuid(6)
                    });
                }
            }

            return list;
        }

        public async Task<TablaDto[]> GetDto(int from, int to)
        {
            var list = new List<TablaDto>();

            string sql = @"	SELECT	Tabla.TablaId,Texto,Fecha,Moneda ,Boleano,FechaHora,Guid
                                    ,OtraTablaId,TextoOtra
	                        FROM	Tabla
			                        inner join OtraTabla on Tabla.TablaId = OtraTabla.TablaId
	                        where	OtraTabla.TablaId >= @From
			                        and OtraTabla.TablaId <= @To";

            using var connection = Connection;
            {
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                await connection.OpenAsync();

                using SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new TablaDto
                    {
                        TablaId = dr.GetInt32(0),
                        Texto = dr.GetString(1),
                        Fecha = dr.GetDateTime(2),
                        Moneda = dr.GetDecimal(3),
                        Boleano = dr.GetBoolean(4),
                        FechaHora = dr.GetDateTime(5),
                        Guid = dr.GetGuid(6),
                        OtraTablaId = dr.GetInt32(7),
                        TextoOtra = dr.GetString(8)
                    });
                }
            }
            return list.ToArray();
        }

        public async Task<Tabla> GetSp(int id)
        {
            Tabla tabla = default;

            using var connection = Connection;
            {
                using SqlCommand cmd = new("usp_get", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();

                using SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                    tabla = new Tabla
                    {
                        TablaId = dr.GetInt32(0),
                        Texto = dr.GetString(1),
                        Fecha = dr.GetDateTime(2),
                        Moneda = dr.GetDecimal(3),
                        Boleano = dr.GetBoolean(4),
                        FechaHora = dr.GetDateTime(5),
                        Guid = dr.GetGuid(6)
                    };
                }
            }
            return tabla;
        }

        public async Task<List<Tabla>> GetSomeSp(int from, int to)
        {
            var list = new List<Tabla>();

            using var connection = Connection;
            {
                using var cmd = new SqlCommand("usp_some", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                await connection.OpenAsync();

                using SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new Tabla
                    {
                        TablaId = dr.GetInt32(0),
                        Texto = dr.GetString(1),
                        Fecha = dr.GetDateTime(2),
                        Moneda = dr.GetDecimal(3),
                        Boleano = dr.GetBoolean(4),
                        FechaHora = dr.GetDateTime(5),
                        Guid = dr.GetGuid(6)
                    });
                }
            }
            return list;
        }

        public async Task<List<TablaDto>> GetDtoSp(int from, int to)
        {
            var list = new List<TablaDto>();
            using var connection = Connection;
            {
                using var cmd = new SqlCommand("usp_someDto", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                await connection.OpenAsync();

                using SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (await dr.ReadAsync().ConfigureAwait(false))
                {
                    list.Add(new TablaDto
                    {
                        TablaId = dr.GetInt32(0),
                        Texto = dr.GetString(1),
                        Fecha = dr.GetDateTime(2),
                        Moneda = dr.GetDecimal(3),
                        Boleano = dr.GetBoolean(4),
                        FechaHora = dr.GetDateTime(5),
                        Guid = dr.GetGuid(6),
                        OtraTablaId = dr.GetInt32(7),
                        TextoOtra = dr.GetString(8),
                    });
                }
            }
            return list;
        }
    }
}
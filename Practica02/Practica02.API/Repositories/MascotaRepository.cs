using Practica02.API.Data;
using Practica02.API.Models;
using Dapper;
using System.Data;

namespace Practica02.API.Repositories
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MascotaRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Mascota>> ObtenerTodas()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = @"
                    SELECT IdMascota, Nombre, Especie, Raza, Peso, IdCliente 
                    FROM dbo.Mascotas";
                return await connection.QueryAsync<Mascota>(query);
            }
        }

        public async Task<Mascota?> ObtenerPorId(long idMascota)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = @"
                    SELECT IdMascota, Nombre, Especie, Raza, Peso, IdCliente 
                    FROM dbo.Mascotas 
                    WHERE IdMascota = @IdMascota";
                return await connection.QueryFirstOrDefaultAsync<Mascota>(query, new { IdMascota = idMascota });
            }
        }

        public async Task<IEnumerable<Mascota>> ObtenerPorIdCliente(long idCliente)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = @"
                    SELECT IdMascota, Nombre, Especie, Raza, Peso, IdCliente 
                    FROM dbo.Mascotas 
                    WHERE IdCliente = @IdCliente";
                return await connection.QueryAsync<Mascota>(query, new { IdCliente = idCliente });
            }
        }

        public async Task<int> ContarPorEspecie(string especie)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = @"
            SELECT COUNT(*)
            FROM dbo.Mascotas
            WHERE Especie = @Especie";

                return await connection.ExecuteScalarAsync<int>(query, new { Especie = especie });
            }
        }

        public async Task<long> Crear(Mascota mascota)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = "dbo.sp_InsertarMascota";

                var id = await connection.QuerySingleAsync<long>(query, new
                {
                    mascota.Nombre,
                    mascota.Especie,
                    mascota.Raza,
                    mascota.Peso,
                    mascota.IdCliente
                }, commandType: CommandType.StoredProcedure);

                return id;
            }
        }

        public async Task<bool> Actualizar(Mascota mascota)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = @"
                    UPDATE dbo.Mascotas 
                    SET Nombre = @Nombre, Especie = @Especie, Raza = @Raza, Peso = @Peso, IdCliente = @IdCliente
                    WHERE IdMascota = @IdMascota";

                var result = await connection.ExecuteAsync(query, mascota);
                return result > 0;
            }
        }

        public async Task<bool> Eliminar(long idMascota)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                const string query = "DELETE FROM dbo.Mascotas WHERE IdMascota = @IdMascota";
                var result = await connection.ExecuteAsync(query, new { IdMascota = idMascota });
                return result > 0;
            }
        }

        public async Task<IEnumerable<MascotaConsulta>> ConsultarMascotas()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<MascotaConsulta>(
                    "dbo.sp_ConsultarMascotas",
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}

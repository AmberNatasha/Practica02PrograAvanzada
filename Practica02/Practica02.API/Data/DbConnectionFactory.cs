using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Practica02.API.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionStringName = "Practica02";

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var cs = _configuration.GetConnectionString(_connectionStringName);
            if (string.IsNullOrWhiteSpace(cs))
            {
                throw new InvalidOperationException($"Connection string '{_connectionStringName}' not found.");
            }

            return new SqlConnection(cs);
        }
    }
}

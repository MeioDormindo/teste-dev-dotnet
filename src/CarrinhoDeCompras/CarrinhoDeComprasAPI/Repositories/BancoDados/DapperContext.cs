using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace CarrinhoDeComprasAPI.Repositories.BancoDados
{
    public class DapperContext : IDapperContext
    {
        private readonly string? connectionString;

        public DapperContext(IConfiguration configuration)
        {
            this.connectionString = configuration.GetSection("ConnectionStrings")["connection"];
        }
        public DapperContext(string connection)
        {
            this.connectionString = connection;
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(this.connectionString);
        }
    }
}

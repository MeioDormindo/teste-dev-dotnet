using System.Data.Common;

namespace CarrinhoDeComprasAPI.Repositories.BancoDados
{
    public interface IDapperContext
    {
        DbConnection CreateConnection();
    }
}

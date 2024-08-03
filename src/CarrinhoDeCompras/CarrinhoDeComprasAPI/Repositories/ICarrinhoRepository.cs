using CarrinhoDeComprasAPI.Models;

namespace CarrinhoDeComprasAPI.Repositories
{
    public interface ICarrinhoRepository
    {
        public Task<Carrinho> ObterCarrinho(int carrinhoId);
        public Task<ItemCarrinho> AdicionarItemCarrinho(int? carrinhoId, ItemCarrinho item);

        public Task<string> RemoverItemCarrinho(int idItem);

        public Task<string> AtualizarQuantidadeItem(int carrinhoId, ItemCarrinho item);
    }
}

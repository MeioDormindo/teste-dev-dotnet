using CarrinhoDeComprasWEB.Models;

namespace CarrinhoDeComprasWEB.Services.Contracts
{
    public interface IItemCarrinhoService
    {
        Task<IEnumerable<ItensCarrinhoViewModel>> ObterCarrinho(int? id);
        Task<ItensCarrinhoViewModel> AdicionarItemCarrinho(int? idcarrinho, ItensCarrinhoViewModel item);
        Task<ItensCarrinhoViewModel> AtualizarQuantidadeItem(int? idcarrinho, ItensCarrinhoViewModel item);
        Task<bool> RemoverItemCarrinho(int id);
    }
}

using CarrinhoDeComprasAPI.Models;
using CarrinhoDeComprasAPI.Repositories.BancoDados;
using Dapper;
using System.Data;

namespace CarrinhoDeComprasAPI.Repositories
{
    public class CarrinhoRepository :  ICarrinhoRepository
    {
        private readonly IDapperContext _contexto;

        public CarrinhoRepository(IDapperContext contexto)
        {
            this._contexto = contexto;
        }

        private static List<Carrinho> _db = new List<Carrinho>()
        {
            new Carrinho(){
            Id = 1,
            Itens = new List<ItemCarrinho>()
                { new ItemCarrinho() {Id = 1, IdCarrinho = 1, PrecoUnitario = 5, Produto = "Chocolate", Quantidade = 3 }
                }
            },
            new Carrinho(){
            Id = 2,
            Itens = new List<ItemCarrinho>()
                { new ItemCarrinho() {Id = 2, IdCarrinho = 2, PrecoUnitario = 5, Produto = "Chocolate", Quantidade = 3 },
                  new ItemCarrinho() {Id = 3, IdCarrinho = 2, PrecoUnitario = 9, Produto = "Bala", Quantidade = 5 }
                }
            }
        };

        public async Task<Carrinho> ObterCarrinho(int carrinhoId)
        {
            using (var _connection = _contexto.CreateConnection())
            {
                var itens = await _connection.QueryAsync<ItemCarrinho>("SELECT IdItem Id, * FROM dbo.FN_ObterCarrinho(@Id)", new { Id = carrinhoId });
                return new Carrinho() { Id = carrinhoId, Itens = itens.ToList<ItemCarrinho>() };
            }
        }

        public async Task<ItemCarrinho> AdicionarItemCarrinho(int? carrinhoId, ItemCarrinho item)
        {
            using (var _connection = _contexto.CreateConnection())
            {
                item.Id = await _connection.QueryFirstOrDefaultAsync<int>("PC_AdicionarItemCarrinho",
                new
                {
                    IdCarrinho = carrinhoId.GetValueOrDefault(0),
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario,
                    PrecoTotal = item.PrecoTotalGet
                }, commandType: CommandType.StoredProcedure);
            }
            return item;
        }

        public async Task<string> AtualizarQuantidadeItem(int carrinhoId, ItemCarrinho item)
        {
            using (var _connection = _contexto.CreateConnection())
            {
                return await _connection.QueryFirstOrDefaultAsync<string>("PC_AtualizarQuantidadeItem",
                 new
                 {
                     IdCarrinho = carrinhoId,
                     IdItemCarrinho = item.Id,
                     Quantidade = item.Quantidade
                 }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<string> RemoverItemCarrinho(int idItem)
        {
            using (var _connection = _contexto.CreateConnection())
            {
                return await _connection.QueryFirstOrDefaultAsync<string>("PC_RemoverItem",
                 new
                 {
                     IdItemCarrinho = idItem
                 }, commandType: CommandType.StoredProcedure);
            }
        }


        public Carrinho ObterCarrinhoFaker(int carrinhoId)
        {
            return _db.FirstOrDefault(carrinho => carrinho.Id == carrinhoId);
        }

        public void AdicionarItemCarrinhoFaker(int? carrinhoId, ItemCarrinho item)
        {
            _db.FirstOrDefault(carrinho => carrinho.Id == carrinhoId).Itens.Add(item);
        }

        public void AtualizarQuantidadeItemFaker(int? carrinhoId, ItemCarrinho item)
        {
            _db.FirstOrDefault(carrinho => carrinho.Id == carrinhoId).Itens.FirstOrDefault(x => x.Id == item.Id).Quantidade = item.Quantidade;
        }

        public void RemoverItemCarrinhoFaker(int? carrinhoId, ItemCarrinho item)
        {
            var itemRemover = _db.FirstOrDefault(carrinho => carrinho.Id == carrinhoId).Itens.FirstOrDefault(x => x.Id == item.Id);
            _db.FirstOrDefault(carrinho => carrinho.Id == carrinhoId).Itens.Remove(itemRemover);
        }
    }
}

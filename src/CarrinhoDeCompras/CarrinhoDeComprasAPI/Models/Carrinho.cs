namespace CarrinhoDeComprasAPI.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        private int TotalItens => Itens.Count;
        private decimal ValorTotal => Itens.Sum(item => item.PrecoTotalGet);
        public List<ItemCarrinho> Itens { get; set; }

        public decimal TotalItensGet
        {
            get
            {
                return TotalItens;
            }
        }

        public decimal ValorTotalGet
        {
            get
            {
                return ValorTotal;
            }
        }
    }
}

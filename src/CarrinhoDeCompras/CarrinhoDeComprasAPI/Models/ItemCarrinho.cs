namespace CarrinhoDeComprasAPI.Models
{   
        public class ItemCarrinho
        {
            public int Id { get; set; }
            public int? IdCarrinho { get; set; }
            public string Produto { get; set; }
            public int Quantidade { get; set; }
            public decimal PrecoUnitario { get; set; }
            private decimal PrecoTotal => Quantidade * PrecoUnitario;

            public decimal PrecoTotalGet
            {
                get
                {
                    return PrecoTotal;
                }
            }

        }
    
}

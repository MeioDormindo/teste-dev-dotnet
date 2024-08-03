using System.ComponentModel.DataAnnotations;

namespace CarrinhoDeComprasWEB.Models
{
    public class ItensCarrinhoViewModel
    {
        public int Id { get; set; }
        public int IdCarrinho { get { return 1; } }

        public string Produto { get; set; }
        [Required]

        public int Quantidade { get; set; }
        [Required]

        public decimal PrecoUnitario { get; set; }
        [Required]

        private decimal PrecoTotalCalc => Quantidade * PrecoUnitario;
        public decimal PrecoTotal
        {
            get
            {
                return PrecoTotalCalc;
            }
        }
        public decimal Total { get; set; }
        public List<ItensCarrinhoViewModel> Itens { get; set; } = new List<ItensCarrinhoViewModel>();
    }
}

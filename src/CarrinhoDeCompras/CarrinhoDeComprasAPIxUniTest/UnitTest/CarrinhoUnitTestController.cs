using CarrinhoDeComprasAPI.Controllers;
using CarrinhoDeComprasAPI.Models;
using CarrinhoDeComprasAPI.Repositories.BancoDados;
using CarrinhoDeComprasAPI.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CarrinhoDeComprasAPIxUniTest.UnitTest
{
    public class CarrinhoUnitTestController
    {
        private readonly CarrinhosController _carrinho;

        /// <summary>
        /// Conexão ao banco de homologação
        /// </summary>
        public CarrinhoUnitTestController()
        {
            _carrinho = new CarrinhosController(new CarrinhoRepository
                (new DapperContext("Server=MeioDormindo;Database=CarrinhoCompras;Persist Security Info=False;Pooling=False;Encrypt=False;Trusted_Connection=True;")));
        }
        [Fact]
        public async Task ObterCarrinho_OKResult()
        {
            var carrinhoId = 1;

            var data = await _carrinho.ObterCarrinho(carrinhoId);

            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ObterCarrinho_NotFoundResult()
        {
            var carrinhoId = 390000;

            var data = await _carrinho.ObterCarrinho(carrinhoId);

            var result = Assert.IsType<NotFoundObjectResult>(data);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task ObterCarrinho_BadRequest()
        {
            var carrinhoId = -1;

            var data = await _carrinho.ObterCarrinho(carrinhoId);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AdicionarItemCarrinhoOKResult()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { PrecoUnitario = 1, Quantidade = 10, Produto = "Controle" }; ;
            var data = await _carrinho.AdicionarItemCarrinho(carrinhoId, item);

            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AdicionarItemCarrinhoBadRequestTudoNulo()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { };
            var data = await _carrinho.AdicionarItemCarrinho(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AdicionarItemCarrinhoBadRequestProdutoNulo()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { PrecoUnitario = 1, Quantidade = 10 };
            var data = await _carrinho.AdicionarItemCarrinho(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AdicionarItemCarrinhoBadRequestPrecoNulo()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", Quantidade = 10 };
            var data = await _carrinho.AdicionarItemCarrinho(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AdicionarItemCarrinhoBadRequestQuantidadeNula()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", PrecoUnitario = 1 };
            var data = await _carrinho.AdicionarItemCarrinho(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task AtualizarQuantidadeItemOKResulte()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { Quantidade = 9, Id = 1 };
            var data = await _carrinho.AtualizarQuantidadeItem(carrinhoId, item);

            var result = Assert.IsType<OkObjectResult>(data);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AtualizarQuantidadeItemBadRequestIdItemNaoExiste()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", PrecoUnitario = 1, Quantidade = 9, Id = 6565656 };
            var data = await _carrinho.AtualizarQuantidadeItem(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task AtualizarQuantidadeItemBadRequestIdCarrinhoNaoExiste()
        {
            var carrinhoId = 999999999;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", PrecoUnitario = 1, Quantidade = 9, Id = 1 };
            var data = await _carrinho.AtualizarQuantidadeItem(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public async Task AtualizarQuantidadeItemBadRequestQuantidadeNula()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", PrecoUnitario = 1, Id = 1 };
            var data = await _carrinho.AtualizarQuantidadeItem(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AtualizarQuantidadeItemBadRequestCarrinhoNulo()
        {
            var carrinhoId = 0;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", PrecoUnitario = 1, Quantidade = 3, Id = 1 };
            var data = await _carrinho.AtualizarQuantidadeItem(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AtualizarQuantidadeItemBadRequestItemNulo()
        {
            var carrinhoId = 1;
            ItemCarrinho item = new ItemCarrinho() { Produto = "Bolacha", PrecoUnitario = 1, Quantidade = 3 };
            var data = await _carrinho.AtualizarQuantidadeItem(carrinhoId, item);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task RemoverItemCarrinhoBadRequestIdItemNulo()
        {
            ItemCarrinho item = new ItemCarrinho();
            var data = await _carrinho.RemoverItemDoCarrinho(item.Id);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task RemoverItemCarrinhoBadRequestItemNaoExiste()
        {
            ItemCarrinho item = new ItemCarrinho() { Id = 9556666 };
            var data = await _carrinho.RemoverItemDoCarrinho(item.Id);

            var result = Assert.IsType<BadRequestObjectResult>(data);
            Assert.Equal(400, result.StatusCode);
        }
    }
}


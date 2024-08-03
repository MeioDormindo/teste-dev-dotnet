using CarrinhoDeComprasAPI.Models;
using CarrinhoDeComprasAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarrinhoDeComprasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CarrinhosController : ControllerBase
    {
        private readonly ICarrinhoRepository _repository;

        public CarrinhosController(CarrinhoRepository repositorio)
        {
            _repository = repositorio;
        }

        /// <summary>
        /// Obtem um carrinho de compra e seus itens
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um carrinho e seus itens</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ObterCarrinho(int? id)
        {
            if (id.GetValueOrDefault(0) <= 0)
                return BadRequest("Id do carrinho inválido");

            try
            {
                var carrinho = await _repository.ObterCarrinho(id.GetValueOrDefault());

                if (carrinho.Itens.Count < 1)
                    return NotFound("Carrinho não encontrado.");
                else
                {
                    var retorno = new
                    {
                        Itens = carrinho.Itens.Select(i => new
                        {
                            Id = i.Id,
                            Produto = i.Produto,
                            Quantidade = i.Quantidade,
                            PrecoUnitario = i.PrecoUnitario,
                            PrecoTotal = i.PrecoTotalGet,
                            IdCarrinho = i.IdCarrinho
                        }),
                        totalItens = carrinho.TotalItensGet,
                        valorTotal = carrinho.ValorTotalGet
                    };

                    return Ok(retorno);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao consultar carrinho - {ex.Message}");
            }
        }


        /// <summary>
        /// Adicionar novo item no carrinho
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>Adiciona um novo item no carrinho</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AdicionarItemCarrinho(int? id, [FromBody] ItemCarrinho item)
        {
            if (item.PrecoUnitario <= 0)
                return BadRequest("O preço unitário precisa ser maior que zero!");
            if (item.Quantidade <= 0)
                return BadRequest("A quantidade precisa ser maior que zero!.");
            if (item.Produto == null || item.Produto.Equals(""))
                return BadRequest("O produto precisar ter um nome!.");
            try
            {
                item = await _repository.AdicionarItemCarrinho(id, item);

                var retorno = new
                {
                    mensagem = "Item adicionado com sucesso",
                    item = new
                    {
                        id = item.Id,
                        nomeProduto = item.Produto,
                        quantidade = item.Quantidade,
                        precoUnitario = item.PrecoUnitario,
                        precoTotal = item.PrecoTotalGet
                    }
                };
                return Ok(retorno);
            }
            catch (Exception ex) { return BadRequest($"Erro ao adicionar o item - {ex.Message}"); }
        }

        /// <summary>
        /// Atualiza a quantidade de itens
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>Atualiza a quantidade de itens de um item especifico do carrinho</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AtualizarQuantidadeItem(int? id, [FromBody] ItemCarrinho item)
        {
            if (item.Quantidade <= 0)
                return BadRequest("A quantidade precisa ser maior que zero!");
            if (id.GetValueOrDefault(0) <= 0)
                return BadRequest("Id do carrinho inválido");
            if (item.Id <= 0)
                return BadRequest("Id do item invalido");

            try
            {
                var result = await _repository.AtualizarQuantidadeItem(id.GetValueOrDefault(), item);

                if (result != null && result.Contains("Erro"))
                    return BadRequest($"Erro ao atualizar a quantidade do item - {result}");

                var retorno = new
                {
                    mensagem = "Quantidade atualizada com sucesso",
                    item = new
                    {
                        id = item.Id,
                        nomeProduto = item.Produto,
                        quantidade = item.Quantidade,
                        precoUnitario = item.PrecoUnitario,
                        precoTotal = item.PrecoTotalGet
                    }
                };
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar a quantidade do item - {ex.Message}");
            }
        }
        /// <summary>
        /// Remove item do carrinho
        /// </summary>
        /// <param name="idItem"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoverItemDoCarrinho(int id)
        {
            if (id <= 0)
                return BadRequest("Id do item invalido");

            try
            {
                var result = await _repository.RemoverItemCarrinho(id);

                if (result != null && result.Contains("Erro"))
                    return BadRequest($"Erro ao remover item - {result}");

                return Ok(new { mensagem = $"Item removido com sucesso" });
            }
            catch (Exception ex) { return BadRequest($"Erro ao Remover item - {ex.Message}"); }
        }
    }
}

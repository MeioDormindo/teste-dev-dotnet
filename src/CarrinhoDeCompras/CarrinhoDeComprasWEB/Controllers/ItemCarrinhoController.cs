using CarrinhoDeComprasWEB.Models;
using CarrinhoDeComprasWEB.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarrinhoDeComprasWEB.Controllers
{
    public class ItemCarrinhoController : Controller
    {
        private readonly IItemCarrinhoService _itemCarrinhoService;

        public ItemCarrinhoController(IItemCarrinhoService itemCarrinhoService)
        {
            _itemCarrinhoService = itemCarrinhoService;
        }

        [HttpGet]
        public async Task<ActionResult<ItensCarrinhoViewModel>> Index()
        {
            var result = await _itemCarrinhoService.ObterCarrinho(1);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AdicionarItemCarrinho()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItemCarrinho(ItensCarrinhoViewModel item)
        {
            if (ModelState.IsValid)
            {
                var result = await _itemCarrinhoService.AdicionarItemCarrinho(item.IdCarrinho, item);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarQuantidadeItem(int idCarrinho, ItensCarrinhoViewModel item)
        {
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarQuantidadeItem(ItensCarrinhoViewModel item)
        {

            var result = await _itemCarrinhoService.AtualizarQuantidadeItem(item.IdCarrinho, item);

            if (result != null)
                return RedirectToAction(nameof(Index));

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> RemoverItemDoCarrinho(int idCarrinho, ItensCarrinhoViewModel item)
        {
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> RemoverItemDoCarrinho(ItensCarrinhoViewModel item)
        {
            var result = await _itemCarrinhoService.RemoverItemCarrinho(item.Id);

            if (result != null)
                return RedirectToAction(nameof(Index));

            return View();
        }

    }
}


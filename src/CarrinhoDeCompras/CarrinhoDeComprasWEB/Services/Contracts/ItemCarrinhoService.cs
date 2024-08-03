using CarrinhoDeComprasWEB.Models;
using System.Text.Json;
using System.Text;

namespace CarrinhoDeComprasWEB.Services.Contracts
{
    public class ItemCarrinhoService : IItemCarrinhoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/Carrinhos/";
        private readonly JsonSerializerOptions _options;
        private ItensCarrinhoViewModel itemVM;

        public ItemCarrinhoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ItensCarrinhoViewModel>> ObterCarrinho(int? id)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    itemVM = await JsonSerializer
                        .DeserializeAsync<ItensCarrinhoViewModel>(apiResponse, _options);
                }
                else
                    return null;

            }
            return itemVM.Itens;
        }

        public async Task<ItensCarrinhoViewModel> AdicionarItemCarrinho(int? idCarrinho, ItensCarrinhoViewModel item)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            StringContent content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint + idCarrinho, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    itemVM = await JsonSerializer
                        .DeserializeAsync<ItensCarrinhoViewModel>(apiResponse, _options);
                }
                else
                    return null;

            }
            return itemVM;
        }

        public async Task<ItensCarrinhoViewModel> AtualizarQuantidadeItem(int? idCarrinho, ItensCarrinhoViewModel item)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            ItensCarrinhoViewModel itemAtualizar = new ItensCarrinhoViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint + idCarrinho, item))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    itemAtualizar = await JsonSerializer
                        .DeserializeAsync<ItensCarrinhoViewModel>(apiResponse, _options);
                }
                else
                    return null;

            }
            return itemAtualizar;
        }

        public async Task<bool> RemoverItemCarrinho(int id)
        {
            var client = _clientFactory.CreateClient("CarrinhoAPI");
            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

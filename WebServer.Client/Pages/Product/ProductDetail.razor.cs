using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServer.Models.Product;

namespace WebServer.Client.Pages.Product
{

    public partial class ProductDetail
    {
        [Parameter]
        public string id { get; set; }

        private ProductModel product;

        protected override async Task OnInitializedAsync()
        {
            product = await Http.GetFromJsonAsync<ProductModel>($"api/products/{id}");
        }


    }
}

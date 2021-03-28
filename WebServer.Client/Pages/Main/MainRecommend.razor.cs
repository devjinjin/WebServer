using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;
using WebServer.Service.Products;

namespace WebServer.Client.Pages.Main
{
    public partial class MainRecommend
    {
        public List<ProductModel> products;
        public MetaData MetaData { get; set; } = new MetaData();

        private ProductParameters _productParameters = new ProductParameters();

        [Inject]
        public IProductHttpRepository repository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetProducts();
        }

        private async Task GetProducts()
        {

            var pagingResponse = await repository.GetItems(_productParameters);
            products = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;
using WebServer.Service.Products;

namespace WebServer.Client.Pages.Admin.Popup
{
    public partial class AdminPopupList
    {
        public List<ProductModel> ProductList { get; set; } = new List<ProductModel>();
        public MetaData MetaData { get; set; } = new MetaData();

        private ProductParameters _productParameters = new ProductParameters();

        [Inject]
        public IProductHttpRepository repository { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GetProduct();
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProduct();
        }

        private async Task GetProduct()
        {
            var pagingResponse = await repository.GetItems(_productParameters);
            ProductList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _productParameters.PageNumber = 1;
            _productParameters.SearchTerm = searchTerm;
            await GetProduct();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _productParameters.OrderBy = orderBy;
            await GetProduct();
        }
    }
}

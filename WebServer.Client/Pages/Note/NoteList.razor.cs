using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Service.Notes;

namespace WebServer.Client.Pages.Note
{
    public partial class NoteList
    {

        public List<WebServer.Models.Product.ProductModel> productList { get; set; } = new List<WebServer.Models.Product.ProductModel>();
        public MetaData MetaData { get; set; } = new MetaData();

        private ProductParameters _productParameters = new ProductParameters();

        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await GetProducts();
        }
        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProducts();
        }

        private async Task GetProducts()
        {

            var pagingResponse = await ProductRepo.GetProducts(_productParameters);
            productList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _productParameters.PageNumber = 1;
            _productParameters.SearchTerm = searchTerm;
            await GetProducts();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _productParameters.OrderBy = orderBy;
            await GetProducts();
        }

        public void MoveCreate()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/create");

        }

        public void MoveUpdate(Guid id)
        {
            this.NavigationManager.NavigateTo("/note/update" + id);

      

        }

        public void MoveDelete(Guid id)
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/delete/" + id);

        }

        public void MoveDetail()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/detail/" + 1);

        }
    }
}

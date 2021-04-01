using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServer.Models.Product;
using WebServer.Service.Products;

namespace WebServer.Client.Pages.Admin.Product
{
    public partial class AdminProductEdit
    {
        [Parameter]
        public string id { get; set; }
        ProductRequestModel requestModel = new ProductRequestModel();
        private ProductModel _product = null;
        private ProductSuccessNotification _notification;
        private string originFilePath = "";
        public bool IsNewImage { get; set; }
        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _product = await Http.GetFromJsonAsync<ProductModel>($"api/products/{id}");


           
        }

        private async Task Modify()
        {
            
            if (_product.ImageUrl != null && _product.ImageUrl.Length > 0)
            {
                requestModel.Id = _product.Id;
                requestModel.Name = _product.Name;
                requestModel.Discription = _product.Discription;
                requestModel.Name = _product.Name;
                requestModel.Supplier = _product.Supplier;
                requestModel.Price = _product.Price;
                requestModel.ImageUrl = _product.ImageUrl;
                requestModel.IsSoldOut = _product.IsSoldOut;
                requestModel.TotalCount = _product.TotalCount;
                requestModel.ImageUrl = _product.ImageUrl;
                if (requestModel.IsNewImage) {
                    requestModel.OldFilePath = originFilePath;
                }
                
                await ProductRepo.Edit(requestModel);

                _notification.Show();
            }
        }

        private void AssignImageUrl(string imgUrl)
        {
            requestModel.IsNewImage = true;
            _product.ImageUrl = imgUrl;
        }

        private void DeleteImage()
        {
            originFilePath = _product.ImageUrl;
            _product.ImageUrl = "";
        }
    }
}

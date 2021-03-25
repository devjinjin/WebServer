using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebServer.Client.Shared;
using WebServer.Models.Product;
using WebServer.Service.Products;

namespace WebServer.Client.Pages.Product
{

    public partial class ProductCreate
    {

        private ProductModel _product = new ProductModel();
        private ProductSuccessNotification _notification;

        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }

        private async Task Create()
        {
            if (_product.ImageUrl != null) {
             
                await ProductRepo.CreateProduct(_product);
                _notification.Show();
            }
        }

        private void AssignImageUrl(string imgUrl) {
            _product.ImageUrl = imgUrl;
        }
    }
}

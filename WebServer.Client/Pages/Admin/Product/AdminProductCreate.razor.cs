using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebServer.Client.Pages.Product;
using WebServer.Models.Product;
using WebServer.Service.Products;

namespace WebServer.Client.Pages.Admin.Product
{
    public partial class AdminProductCreate
    {
        private ProductModel _product = new ProductModel();
        private ProductSuccessNotification _notification;

        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }

        private async Task Create()
        {
            if (_product.ImageUrl != null)
            {

                await ProductRepo.Create(_product);
                _notification.Show();
            }
        }

        private void AssignImageUrl(string imgUrl)
        {
            _product.ImageUrl = imgUrl;
        }
    }
}

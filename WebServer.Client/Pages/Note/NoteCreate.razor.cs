using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebServer.Client.Shared;
using WebServer.Models.Product;
using WebServer.Service.Notes;
using WebServer.Client.Pages.Component;

namespace WebServer.Client.Pages.Note
{

    public partial class NoteCreate
    {

        private ProductModel _product = new ProductModel();
        private SuccessNotification _notification;

        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }

        private async Task Create()
        {
            await ProductRepo.CreateProduct(_product);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) => _product.ImageUrl = imgUrl;
    }
}

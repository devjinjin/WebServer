using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServer.Models.Product;

namespace WebServer.Client.Pages.Note
{
    public partial class NoteDetail
    {
        [Parameter]
        public string id { get; set; }

        private ProductModel proudct;

        protected override async Task OnInitializedAsync()
        {
            proudct = await Http.GetFromJsonAsync<ProductModel>($"api/products/{id}");
        }


    }
}

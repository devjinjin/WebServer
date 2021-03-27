using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using WebServer.Service.Common.Categories;
using WebServer.Service.Common.Notices;
using WebServer.Service.Notes;
using WebServer.Service.Places;
using WebServer.Service.Products;
using WebServer.Service.Upload;

namespace WebServer.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<INoticeHttpRepository, NoticeHttpRepository>();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

            builder.Services.AddScoped<ICategoryHttpRepository, CategoryHttpRepository>();
            builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();
            builder.Services.AddScoped<INoteHttpRepository, NoteHttpRepository>();
            builder.Services.AddScoped<IPlaceHttpRepository, PlaceHttpRepository>();
            builder.Services.AddScoped<IUploadHttpRepository, UploadHttpRepository>();
            builder.Services.AddScoped<IPlaceImageHttpRespository, PlaceImageHttpRespository>();
       

    

            await builder.Build().RunAsync();
        }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using WebServer.Service.Places;

namespace WebServer.Client.Pages.Admin.Place
{
    public partial class AdminPlaceImageUpload
    {
        private ElementReference _input;

        [Parameter]
        public string ImgUrl { get; set; }


        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        [Inject]
        public IFileReaderService FileReaderService { get; set; }
        [Inject]
        public IPlaceImageHttpRespository Repository { get; set; }

        private async Task HandleSelected()
        {
            foreach (var file in await FileReaderService.CreateReference(_input).EnumerateFilesAsync())
            {
                if (file != null)
                {
                    var fileInfo = await file.ReadFileInfoAsync();
                    using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), "image", fileInfo.Name);

                        var response = await Repository.UploadImage(content);


                        var serverUrl = "/Temp/Temp/";
                        ImgUrl = $"{serverUrl}{response.FileName}";
                        await OnChange.InvokeAsync(response.FileName);
                    }
                }
            }
        }
    }
}

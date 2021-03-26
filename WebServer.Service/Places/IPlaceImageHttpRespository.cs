using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Service.Places
{
    public interface IPlaceImageHttpRespository
    {
        Task<string> UploadImage(MultipartFormDataContent content);
    }
}

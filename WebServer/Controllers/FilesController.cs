using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FilesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            // 파일을 업로드할 폴더: 서버루트\\files
        

            if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
            {
                _environment.WebRootPath = Directory.GetCurrentDirectory();
            }

            var uploadFolder = Path.Combine(_environment.WebRootPath, "Files");

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // 파일명 
                    var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                    using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            return Ok(new { message = "OK" });
        }
    }
}

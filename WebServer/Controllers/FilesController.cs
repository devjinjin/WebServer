using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FilesController> logger;

        public FilesController(IWebHostEnvironment environment, ILogger<FilesController> logger)
        {
            _environment = environment;
            this.logger = logger;
        }
        //[HttpPost]
        //[Consumes("application/json", "multipart/form-data")]
        //public async Task<IActionResult> Post(List<IFormFile> files)
        //{
        //    // 파일을 업로드할 폴더: 서버루트\\files


        //    if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
        //    {
        //        _environment.WebRootPath = Directory.GetCurrentDirectory();
        //    }

        //    var uploadFolder = Path.Combine(_environment.WebRootPath, "Files");

        //    foreach (var file in files)
        //    {
        //        if (file.Length > 0)
        //        {
        //            // 파일명 
        //            var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

        //            using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    }

        //    return Ok(new { message = "OK" });
        //}
  

        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
                {
                    _environment.WebRootPath = Directory.GetCurrentDirectory();
                }

                if (Request.Form.Files.Count > 0) {
                    var file = Request.Form.Files[0];

                    var uploadFolder = Path.Combine(_environment.WebRootPath, "Files"); //실제 사용 폴더
                    var uploadFolderProduct = Path.Combine(uploadFolder, "Temp");
                    if (!Directory.Exists(uploadFolderProduct))
                    {
                        Directory.CreateDirectory(uploadFolderProduct);
                    }

                    if (file.Length > 0)
                    {
                        //파일 이름 만들기 UserId + DateTime + extension
                        string extension = Path.GetExtension(Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"')));
                        var fileName = Guid.NewGuid() + extension;
                        //var dbPath = $"{Request.Scheme}://{Request.Host}/Temp/Product/{fileName}"; //호출하는 폴더 

                        var dbPath = $"{fileName}"; //호출하는 폴더 

                        using (var fileStream = new FileStream(Path.Combine(uploadFolderProduct, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        return Ok(dbPath);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

      
    }
}

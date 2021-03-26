using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebServer.Data;
using WebServer.Data.Place;
using WebServer.Models.Places;

namespace WebServer.Controllers.Place
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceImageInfoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FilesController> logger;

        public PlaceImageInfoController(ApplicationDbContext context, IWebHostEnvironment environment, ILogger<FilesController> logger)
        {
            this.context = context;
            _environment = environment;
            this.logger = logger;
        }

        // POST: api/PlaceImageInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<ActionResult<PlaceImageResponse>> PostPlaceImageInfo()
        {
            try
            {

                if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
                {
                    _environment.WebRootPath = Directory.GetCurrentDirectory();
                }

                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];

                    var uploadFolder = Path.Combine(_environment.WebRootPath, "Files"); //실제 사용 폴더
                    var uploadFolderProduct = Path.Combine(uploadFolder, "Product");
                    if (!System.IO.Directory.Exists(uploadFolderProduct))
                    {
                        System.IO.Directory.CreateDirectory(uploadFolderProduct);
                    }

                    if (file.Length > 0)
                    {

                        //파일 이름 만들기 UserId + DateTime + extension
                        string extension = System.IO.Path.GetExtension(Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"')));
                        string strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        strTimeStamp = strTimeStamp.Replace(":", "");
                        strTimeStamp = strTimeStamp.Replace("-", "");
                        strTimeStamp = strTimeStamp.Replace(" ", "");
                        strTimeStamp = strTimeStamp.Insert(8, "-");
                        strTimeStamp = strTimeStamp.Insert(13, "-");
                        var fileName = "UserId-" + strTimeStamp + extension;

                        var dbPath = $"{Request.Scheme}://{Request.Host}/Temp/Product/{fileName}"; //호출하는 폴더 

                        using (var fileStream = new FileStream(Path.Combine(uploadFolderProduct, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        var placeImageInfo = new PlaceImageInfo();
                        placeImageInfo.FileName = fileName;
                        context.PlaceImageInfo.Add(placeImageInfo);

                        await context.SaveChangesAsync();

                        var response = new PlaceImageResponse()
                        {
                            Id = placeImageInfo.Id,
                            FileName = fileName
                        };


                        return Ok(response);
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

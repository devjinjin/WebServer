using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebServer.Data;
using WebServer.Data.Place;
using WebServer.Models.Features;
using WebServer.Models.Places;
using WebServer.Utils;

namespace WebServer.Controllers.Place
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceInfoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IPlaceInfoRepository repository;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<PlaceInfoController> logger;

        public PlaceInfoController(ApplicationDbContext context,  IWebHostEnvironment environment, IPlaceInfoRepository _repository, ILogger<PlaceInfoController> logger)
        {
            this.context = context;
            this.environment = environment;
            this.repository = _repository;
            this.logger = logger;
        }

        /// <summary>
        /// 전체 리스트 가져오기 검색어 정보 Page 정보 Orderby에 의해서 적용
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParameters parameters)
        {

            var notes = await repository.GetAllAsync(parameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(notes.MetaData));

            return Ok(notes);           
        }

        // GET: api/PlaceInfo
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PlaceInfo>>> GetPlaceInfo()
        //{
        //    return await _context.PlaceInfo.ToListAsync();
        //}

        // GET: api/PlaceInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceInfo>> GetPlaceInfo(int id)
        {
            var placeInfo = await context.PlaceInfo.FindAsync(id);

            if (placeInfo == null)
            {
                return NotFound();
            }

            return placeInfo;
        }

        // PUT: api/PlaceInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceInfo(int id, PlaceInfo placeInfo)
        {
            if (id != placeInfo.Id)
            {
                return BadRequest();
            }

            context.Entry(placeInfo).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlaceInfo placeInfo)
        {
            if (placeInfo == null)
            {
                return BadRequest();
            }


            FileUtil.MoveFile(environment, placeInfo.MainImage, "Place");

            await repository.AddAsync(placeInfo);

            return CreatedAtAction("GetPlaceInfo", new { id = placeInfo.Id }, placeInfo);
        }


        // DELETE: api/PlaceInfo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaceInfo>> DeletePlaceInfo(int id)
        {
            var placeInfo = await context.PlaceInfo.FindAsync(id);
            if (placeInfo == null)
            {
                return NotFound();
            }

            context.PlaceInfo.Remove(placeInfo);
            await context.SaveChangesAsync();

            return placeInfo;
        }

        private bool PlaceInfoExists(int id)
        {
            return context.PlaceInfo.Any(e => e.Id == id);
        }

        private bool CopyFile(string ImageFile)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(environment.WebRootPath))
                {
                    environment.WebRootPath = Directory.GetCurrentDirectory();
                }

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadFolder = Path.Combine(environment.WebRootPath, "Files"); //실제 사용 폴더
                    var uploadTempFolder = Path.Combine(uploadFolder, "Temp");//임시폴더
                    var uploadProductFolder = Path.Combine(uploadFolder, "Place");

                    if (!System.IO.Directory.Exists(uploadTempFolder))
                    {
                        System.IO.Directory.CreateDirectory(uploadTempFolder);
                    }

                    if (!System.IO.Directory.Exists(uploadProductFolder))
                    {
                        System.IO.Directory.CreateDirectory(uploadProductFolder);
                    }

                    var orginTempFilePath = Path.Combine(uploadTempFolder, ImageFile);
                    var destTempFilePath = Path.Combine(uploadProductFolder, ImageFile);
                    if (System.IO.File.Exists(orginTempFilePath))
                    {
                        System.IO.File.Move(orginTempFilePath, destTempFilePath);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}

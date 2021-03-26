using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServer.Data;
using WebServer.Data.Place;
using WebServer.Models.Features;
using WebServer.Models.Places;

namespace WebServer.Controllers.Place
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlaceInfoRepository repository;


        public PlaceInfoController(ApplicationDbContext context, IPlaceInfoRepository _repository)
        {
            _context = context;
            repository = _repository;
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
            var placeInfo = await _context.PlaceInfo.FindAsync(id);

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

            _context.Entry(placeInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> CreateNote([FromBody] PlaceInfo placeInfo)
        {
            if (placeInfo == null)
            {
                return BadRequest();
            }

            await repository.AddAsync(placeInfo);

            return CreatedAtAction("GetPlaceInfo", new { id = placeInfo.Id }, placeInfo);
        }


        // DELETE: api/PlaceInfo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaceInfo>> DeletePlaceInfo(int id)
        {
            var placeInfo = await _context.PlaceInfo.FindAsync(id);
            if (placeInfo == null)
            {
                return NotFound();
            }

            _context.PlaceInfo.Remove(placeInfo);
            await _context.SaveChangesAsync();

            return placeInfo;
        }

        private bool PlaceInfoExists(int id)
        {
            return _context.PlaceInfo.Any(e => e.Id == id);
        }
    }
}

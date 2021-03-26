using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models.Notes;
using WebServer.Models.Notices;
using WebServer.Data.Common.Notices;
using WebServer.Data.Common;
using Newtonsoft.Json;

namespace WebServer.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NoticesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] NoteParameters parameters)
        {
            if (parameters == null) {
                return BadRequest();
            }
        
            var items = await _context.Notice
              .Search(parameters.SearchTerm)
              .Sort(parameters.OrderBy)
              .ToListAsync();

            var pageItems = PagedList<NoticeModel>
                .ToPagedList(items, parameters.PageNumber, parameters.PageSize);

       
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pageItems.MetaData));

            return Ok(pageItems);
        }

        // GET: api/Notices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoticeModel>> GetNotice(int id)
        {

            var notice = await _context.Notice.FindAsync(id);

            if (notice == null)
            {
                return NotFound();
            }

            notice.ReadCnt = notice.ReadCnt + 1;

            _context.Entry(notice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return notice;
        }

        // PUT: api/Notices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotice(int id, NoticeModel notice)
        {
            if (id != notice.Id)
            {
                return BadRequest();
            }

            _context.Entry(notice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeExists(id))
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

        // POST: api/Notices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NoticeModel>> PostNotice([FromBody] NoticeModel notice)
        {
            if (notice == null)
            {
                return BadRequest();
            }
            notice.Created = DateTime.Now;
            _context.Notice.Add(notice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotice", new { id = notice.Id }, notice);
        }

     
        // DELETE: api/Notices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NoticeModel>> DeleteNotice(int id)
        {
            var notice = await _context.Notice.FindAsync(id);
            if (notice == null)
            {
                return NotFound();
            }

            _context.Notice.Remove(notice);
            await _context.SaveChangesAsync();

            return notice;
        }

        private bool NoticeExists(int id)
        {
            return _context.Notice.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models.Popup;

namespace WebServer.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PopupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Popup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PopupModel>>> GetPopups()
        {
            return await _context.Popups.ToListAsync();
        }

        // GET: api/Popup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PopupModel>> GetPopupModel(int id)
        {
            var popupModel = await _context.Popups.FindAsync(id);

            if (popupModel == null)
            {
                return NotFound();
            }

            return popupModel;
        }

        // PUT: api/Popup/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPopupModel(int id, PopupModel popupModel)
        {
            if (id != popupModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(popupModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopupModelExists(id))
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

        // POST: api/Popup
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PopupModel>> PostPopupModel(PopupModel popupModel)
        {
            _context.Popups.Add(popupModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPopupModel", new { id = popupModel.Id }, popupModel);
        }

        // DELETE: api/Popup/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PopupModel>> DeletePopupModel(int id)
        {
            var popupModel = await _context.Popups.FindAsync(id);
            if (popupModel == null)
            {
                return NotFound();
            }

            _context.Popups.Remove(popupModel);
            await _context.SaveChangesAsync();

            return popupModel;
        }

        private bool PopupModelExists(int id)
        {
            return _context.Popups.Any(e => e.Id == id);
        }
    }
}

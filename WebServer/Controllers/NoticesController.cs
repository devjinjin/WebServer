﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NoticesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NoticesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Notices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notice>>> GetNotice()
        {
            return await _context.Notice.ToListAsync();
        }

        // GET: api/Notices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notice>> GetNotice(int id)
        {
            var notice = await _context.Notice.FindAsync(id);

            if (notice == null)
            {
                return NotFound();
            }

            return notice;
        }

        // PUT: api/Notices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotice(int id, Notice notice)
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
        public async Task<ActionResult<Notice>> PostNotice(Notice notice)
        {
            _context.Notice.Add(notice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotice", new { id = notice.Id }, notice);
        }

        // DELETE: api/Notices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notice>> DeleteNotice(int id)
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

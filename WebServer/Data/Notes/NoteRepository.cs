using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models;
using WebServer.Models.Notes;

namespace WebServer.Data.Notes
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;

        ////public IQueryable<Note> Notes => throw new NotImplementedException();

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Note GetNote(int id)
        {
            var notice = _context.Notes.Find(id);

            return notice;
        }

        public Note AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

            return GetNote(note.Id);
        }





        public async Task<IEnumerable<Note>> GetNoteList()
        {
            return await _context.Notes.ToListAsync();

        }
    }
}

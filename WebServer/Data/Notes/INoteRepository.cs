using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.Notes;

namespace WebServer.Data.Notes
{
    public interface INoteRepository
    {
        //IQueryable<Note> Notes { get; }
        Note GetNote(int id);

        Task<IEnumerable<Note>> GetNoteList();

        Note AddNote(Note note);
    }
}

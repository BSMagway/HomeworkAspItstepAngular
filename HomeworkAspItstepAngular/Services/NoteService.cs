using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using HomeworkAspItstepAngular.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAspItstepAngular.Services
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _appDb;

        public NoteService(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public Note Add(Note note)
        {
            note.NoteId = Guid.NewGuid();

            var entity = _appDb.Notes.Add(note);
            _appDb.SaveChanges();

            return entity.Entity;
        }

        public Note[] GetAll(Guid notepadId)
        {
            return _appDb.Notes
                .AsNoTracking()
                .Where(x => x.NotepadId == notepadId)
                .ToArray();
        }

        public bool Remove(Guid noteId)
        {

            var note = _appDb.Notes
                .Where(x => x.NoteId == noteId)
                .FirstOrDefault();

            if (note == null)
            {
                return false;
            }

            _appDb.Notes.Remove(note);
            _appDb.SaveChanges();

            return true;
        }

        public bool Update(Note note)
        {
            var dbNote = _appDb.Notes
                .Where(x => x.NoteId == note.NoteId)
                .FirstOrDefault();

            if (dbNote == null)
            {
                return false;
            }

            dbNote.NoteName = note.NoteName;
            dbNote.NoteContent = note.NoteContent;
            _appDb.SaveChanges();

            return true;
        }


    }
}

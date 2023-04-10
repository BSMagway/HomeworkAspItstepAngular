using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using HomeworkAspItstepAngular.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAspItstepAngular.Services
{
    public class NotepadService : INotepadService
    {
        private readonly AppDbContext _appDb;

        public NotepadService(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public Notepad Add(NotepadCreateDto notepadDto, Guid userId)
        {
            var notepad = new Notepad
            {
                NotepadName = notepadDto.NotepadName,
                ApplicationUserId = userId,
                NotepadId = Guid.NewGuid()
            };

            var entity = _appDb.Notepads.Add(notepad);
            _appDb.SaveChanges();

            return entity.Entity;
        }

        public Notepad[] GetAll(Guid userId)
        {
            return _appDb.Notepads
                .AsNoTracking()
                .Where(x => x.ApplicationUserId == userId)
                .Include(x => x.Notes)
                .ToArray();               
        }

        public bool Remove(Guid notepadId, Guid userId)
        {
            var notepad = _appDb.Notepads
                .Where(x => x.ApplicationUserId == userId)
                .Where(x => x.NotepadId == notepadId)
                .FirstOrDefault();

            if (notepad == null)
            {
                return false;
            }

            _appDb.Notepads.Remove(notepad);
            _appDb.SaveChanges();

            return true;
        }

        public bool Update(NotepadUpdateDto dto, Guid userId)
        {
            var dbNotepad = _appDb.Notepads
                .Where(x => x.ApplicationUserId == userId)
                .Where(x => x.NotepadId == dto.NotepadId)
                .FirstOrDefault();

            if (dbNotepad == null)
            {
                return false;
            }

            dbNotepad.NotepadName = dto.NotepadName;
            _appDb.SaveChanges();

            return true;
        }
    }
}

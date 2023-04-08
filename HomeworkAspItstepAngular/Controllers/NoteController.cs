using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAspItstepAngular.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public NoteController(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        [HttpGet]
        public Note[] GetAll([FromQuery] Guid notepadId)
        {
            return _appDb.Notes
                .AsNoTracking()
                .Where(x => x.NotepadId == notepadId)
                .ToArray();
        }

        [HttpPost]
        public Note Add([FromBody] Note note)
        {
            note.NoteId = Guid.NewGuid();

            var entity = _appDb.Notes.Add(note);
            _appDb.SaveChanges();

            return entity.Entity;
        }

        [HttpPut]
        public IActionResult Update([FromBody] Note note)
        {
            var dbNote = _appDb.Notes
                .Where(x => x.NoteId == note.NoteId)
                .FirstOrDefault();

            if (dbNote == null)
            {
                return BadRequest();
            }

            dbNote.NoteName = note.NoteName;
            dbNote.NoteContent = note.NoteContent;
            _appDb.SaveChanges();

            return Ok(dbNote);
        }

        [HttpDelete]
        public IActionResult Remove([FromQuery] Guid noteId)
        {
            var note = _appDb.Notes
                .Where(x => x.NoteId == noteId)
                .FirstOrDefault();

            if (note == null)
            {
                return BadRequest();
            }

            _appDb.Notes.Remove(note);
            _appDb.SaveChanges();

            return Ok(note);
        }
    }
}

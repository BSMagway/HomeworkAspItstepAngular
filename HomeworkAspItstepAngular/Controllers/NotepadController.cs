using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkAspItstepAngular.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotepadController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public NotepadController(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        [HttpGet]
        public Notepad[] GetAll()
        {
            return _appDb.Notepads
                .AsNoTracking()
                .Where(x => x.ApplicationUserId == GetUserId())
                .Include(x => x.Notes)
                .ToArray();
        }

        [HttpPost]
        public Notepad Add([FromBody] NotepadCreateDto notepadDto)
        {
            var notepad = new Notepad
            {
                NotepadName = notepadDto.NotepadName,
                ApplicationUserId = GetUserId(),
                NotepadId = Guid.NewGuid()
            };

            var entity = _appDb.Notepads.Add(notepad);
            _appDb.SaveChanges();

            return entity.Entity;
        }

        [HttpPut]
        public IActionResult Update([FromBody] NotepadUpdateDto dto)
        {
            var dbNotepad = _appDb.Notepads
                .Where(x => x.ApplicationUserId == GetUserId())
                .Where(x => x.NotepadId == dto.NotepadId)
                .FirstOrDefault();

            if (dbNotepad == null)
            {
                return BadRequest();
            }

            dbNotepad.NotepadName = dto.NotepadName;
            _appDb.SaveChanges();

            return Ok(dbNotepad);
        }

        [HttpDelete]
        public IActionResult Remove([FromQuery] Guid notepadId)
        {
            var notepad = _appDb.Notepads
                .Where(x => x.ApplicationUserId == GetUserId())
                .Where(x => x.NotepadId == notepadId)
                .FirstOrDefault();

            if (notepad == null)
            {
                return BadRequest();
            }

            _appDb.Notepads.Remove(notepad);
            _appDb.SaveChanges();

            return Ok(notepad);
        }

        private Guid GetUserId()
        {
            var guidString = User.Claims
                .Where(x => x.Type == "ApplicationUserId")
                .Select(x => x.Value)
                .Single();

            return Guid.Parse(guidString);
        }
    }
}

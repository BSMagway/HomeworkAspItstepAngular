using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Services;
using HomeworkAspItstepAngular.Services.Interface;
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
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public Note[] GetAll([FromQuery] Guid notepadId) => _noteService.GetAll(notepadId);

        [HttpPost]
        public Note Add([FromBody] Note note) => _noteService.Add(note);

        [HttpPut]
        public IActionResult Update([FromBody] Note note)
        {
            if (_noteService.Update(note))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Remove([FromQuery] Guid noteId)
        {
            if (_noteService.Remove(noteId))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}

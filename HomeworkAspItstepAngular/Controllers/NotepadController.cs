using HomeworkAspItstepAngular.Data;
using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using HomeworkAspItstepAngular.Services.Interface;
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
        private readonly INotepadService _notepadService;

        public NotepadController(INotepadService notepadService)
        {
            _notepadService = notepadService;
        }

        [HttpGet]
        public Notepad[] GetAll() => _notepadService.GetAll(GetUserId());

        [HttpPost]
        public Notepad Add([FromBody] NotepadCreateDto notepadDto) => _notepadService.Add(notepadDto, GetUserId());

        [HttpPut]
        public IActionResult Update([FromBody] NotepadUpdateDto dto)
        {

            if (_notepadService.Update(dto, GetUserId()))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Remove([FromQuery] Guid notepadId)
        {
            if (_notepadService.Remove(notepadId, GetUserId()))
            {
                return Ok();
            }

            return BadRequest();
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

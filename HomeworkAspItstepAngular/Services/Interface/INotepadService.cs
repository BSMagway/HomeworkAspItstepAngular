using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAspItstepAngular.Services.Interface
{
    public interface INotepadService
    {

        public Notepad[] GetAll(Guid userId);

        public Notepad Add(NotepadCreateDto notepadDto, Guid userId);

        public bool Update(NotepadUpdateDto dto, Guid userId);

        public bool Remove(Guid notepadId, Guid userId);

    }
}

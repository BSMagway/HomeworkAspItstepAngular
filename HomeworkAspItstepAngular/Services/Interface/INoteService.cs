using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAspItstepAngular.Services.Interface
{
    public interface INoteService
    {

        public Note[] GetAll(Guid userId);

        public Note Add(NotepadCreateDto notepadDto, Guid userId);

        public bool Update(NotepadUpdateDto dto, Guid userId);

        public bool Remove(Guid notepadId, Guid userId);

    }
}

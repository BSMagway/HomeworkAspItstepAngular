using HomeworkAspItstepAngular.Entities;
using HomeworkAspItstepAngular.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkAspItstepAngular.Services.Interface
{
    public interface INoteService
    {

        public Note[] GetAll(Guid notepadId);

        public Note Add(Note note);

        public bool Update(Note note);

        public bool Remove(Guid noteId);

    }
}

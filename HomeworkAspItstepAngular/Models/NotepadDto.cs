using HomeworkAspItstepAngular.Entities;

namespace HomeworkAspItstepAngular.Models
{
    public class NotepadDto
    {
        public Guid NotepadId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string NotepadName { get; set; }
        public List<Note> Notes { get; set; }
    }
}

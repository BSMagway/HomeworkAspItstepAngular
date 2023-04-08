using System.ComponentModel.DataAnnotations;

namespace HomeworkAspItstepAngular.Models
{
    public class NotepadUpdateDto
    {
        [Required]
        public Guid NotepadId { get; set; }

        public string NotepadName { get; set; }
    }
}

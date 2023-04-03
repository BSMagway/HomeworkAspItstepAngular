namespace HomeworkAspItstepAngular.Entities
{
    public class Notepad
    {
        public Guid NotepadId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string NotepadName { get; set;}
        public List<Note> Notes { get; set;}

    }
}

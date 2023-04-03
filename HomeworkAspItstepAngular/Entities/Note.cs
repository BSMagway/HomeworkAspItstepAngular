namespace HomeworkAspItstepAngular.Entities
{
    public class Note
    {
        public Guid NoteId { get; set; }

        public Guid NotepadId { get; set; }
        public string NoteName { get; set; }
        public string NoteContent { get; set; }
    }
}

namespace Schedule.Models.Domain
{
    public class LessionStudent
    {
        public int LessionId { get; set; }
        public Lession Lession { get; set; }
        public string StudentId { get; set; }
        public AppUser Student { get; set; }
    }
}

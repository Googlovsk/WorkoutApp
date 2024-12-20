namespace Schedule.Models.Domain
{
    public class LessionStudent
    {
        public int LessionId { get; set; }
        public Lession Lession { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }
    }
}

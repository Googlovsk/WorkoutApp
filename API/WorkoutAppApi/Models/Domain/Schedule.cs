namespace Schedule.Models.Domain
{
    public class Schedule
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public AppUser Teacher { get; set; }
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? LessionId { get; set; }
        public Lession Lession { get; set; }
    }
}

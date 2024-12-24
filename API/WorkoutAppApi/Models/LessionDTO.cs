namespace Schedule.Models
{
    public class LessionDTO
    {
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public int CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public bool IsGroup { get; set; }
        public int? RoomId { get; set; }
    }
}

namespace Schedule.Models.Domain
{
    public class Lession
    {
        public enum LessionStatus
        {
            Scheduled,
            Cancelled,
            Postponed
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public int CategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public LessionStatus Status { get; set; } // Enum для статусов занятий
        public string Notes { get; set; }

        public AppUser Teacher { get; set; }
        public Category Category { get; set; }
        public ICollection<LessionStudent> Students { get; set; }

        public bool IsGroup { get; set; } // True — групповое, False — индивидуальное

    }
}

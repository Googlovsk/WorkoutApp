namespace WorkoutAppApi.Models.Domain
{
    public class TeacherSchedule
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public DateOnly CanWorkWith { get; set; }
        public DateOnly CanWorkBy { get; set; }

        //нивигационные свойства
        public AppUser Teacher { get; set; }
    }
}

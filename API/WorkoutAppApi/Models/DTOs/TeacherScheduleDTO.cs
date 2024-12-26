namespace WorkoutAppApi.Models.DTOs
{
    public class TeacherScheduleDTO
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public DateOnly CanWorkWith { get; set; }
        public DateOnly CanWorkBy { get; set; }
    }
}

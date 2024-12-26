namespace WorkoutAppApi.Models.Domain
{
    public class LessionStudent
    {
        public int LessionId { get; set; }
        public string StudentId { get; set; }

        // Навигационные свойства
        public Lession Lession { get; set; }
        public AppUser Student { get; set; }

    }
}

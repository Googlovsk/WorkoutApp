namespace WorkoutAppApi.Models.DTOs
{
    public class NotificationDTO
    {
        public string Message { get; set; }
        public string RecipientId { get; set; }
        public DateTime SentDate { get; set; }
    }
}

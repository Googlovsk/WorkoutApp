namespace WorkoutAppApi.Models.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string RecipientId { get; set; }
        public DateTime SentDate { get; set; }

        public AppUser Recipient { get; set; }
    }
}

namespace Schedule.Models.Domain
{
    public class Lession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int CategoryId { get; set; } //Например физические занятие, математика, программирование, физика и т.д.
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } //статус занятия, например запланировано, отменено, перенесено и тд.
        public string Notes { get; set; } //доп. информация от учителя для учащихся (при изменении этого поля записавшимя учащимся должен приходить увед.) 


        public User Teacher { get; set; }
        public Category Category { get; set; }
        public ICollection<LessionStudent> Students { get; set; }

    }
}

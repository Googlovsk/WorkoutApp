namespace Schedule.Models.Domain
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } // Например, "ул.Пушкина, д.Колотушкина, Кабинет 101"
        public int Capacity { get; set; } // Вместимость кабинета
        public ICollection<Schedule> Schedules { get; set; }
    }
}

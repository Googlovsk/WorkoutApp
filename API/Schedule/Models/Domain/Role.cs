namespace Schedule.Models.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AppUser> Users { get; set; }
    }
}

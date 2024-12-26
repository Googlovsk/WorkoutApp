using WorkoutAppApi.Models.Domain;

namespace WorkoutAppApi.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

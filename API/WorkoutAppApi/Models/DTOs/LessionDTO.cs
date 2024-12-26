using System.ComponentModel.DataAnnotations;

namespace WorkoutAppApi.Models.DTOs
{
    public class LessionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherId { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public string Notes { get; set; }
        public bool IsGroup { get; set; }
        public string Location { get; set; }
        public int MaxGroupSize { get; set; }
    }
}

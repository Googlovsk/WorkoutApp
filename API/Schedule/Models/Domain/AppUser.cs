using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models.Domain
{
    public class AppUser : IdentityUser
    {
        //public int Id { get; set; }
        //public string Login { get; set; }

        [PersonalData]
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(7)")]
        public string Gender { get; set; }

        [PersonalData]
        public DateOnly DOB { get; set; }

        //public ICollection<Lession> LessonsAsTeacher { get; set; }
        //public ICollection<LessionStudent> LessonsAsStudent { get; set; }

    }
}

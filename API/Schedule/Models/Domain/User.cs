using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models.Domain
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        //public string Login { get; set; }

        [PersonalData]
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        //public int RoleId { get; set; }
        //public string PasswordHash { get; set; }
        //public string PasswordSalt { get; set; }

        //public Role Role { get; set; }
        //public ICollection<Lession> LessonsAsTeacher { get; set; }
        //public ICollection<LessionStudent> LessonsAsStudent { get; set; }

    }
}

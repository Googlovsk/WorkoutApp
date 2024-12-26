using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutAppApi.Models.Domain
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        /// <summary>
        /// Пол пользователя. Остальные поля наследуются от IdentityUser
        /// </summary>
        [PersonalData]
        [Column(TypeName = "nvarchar(6)")]
        public string Gender { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Занятия, на которые могут быть зарегистрированны пользовати с ролью Student
        /// </summary>
        public ICollection<LessionStudent> LessionStudents { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}

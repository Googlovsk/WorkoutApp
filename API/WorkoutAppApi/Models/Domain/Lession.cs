using System.ComponentModel.DataAnnotations;

namespace WorkoutAppApi.Models.Domain
{
    public class Lession
    {
        /// <summary>
        /// Возможные состояния занятия
        /// </summary>
        public enum LessionStatus
        {
            Scheduled,
            Cancelled,
            Postponed
        }
        /// <summary>
        /// Уникальный идентификатор занятия
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название занятия
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор преподователя
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// Статус занятия
        /// </summary>
        public LessionStatus Status { get; set; }
        /// <summary>
        /// Дата начала занятия
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Дата конца занятия
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Примечания от учителя
        /// </summary>
        [MaxLength(500)]
        public string Notes { get; set; }
        /// <summary>
        /// True - групповое занятие, False - индивидуальное
        /// </summary>
        public bool IsGroup { get; set; }
        /// <summary>
        /// Место проведения
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Кол-во мест, если групповое
        /// </summary>
        public int MaxGroupSize { get; set; }

        // Навигационные свойства
        public AppUser Teacher { get; set; }
        public Category Category { get; set; }
        public ICollection<LessionStudent> Students { get; set; }
        public ICollection<TeacherSchedule> TeacherSchedules { get; set; }
    }
}

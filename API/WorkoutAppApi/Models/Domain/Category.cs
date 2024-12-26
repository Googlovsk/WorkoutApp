namespace WorkoutAppApi.Models.Domain
{
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ссылка на родительскую категорию
        /// </summary>
        public int? ParentCategoryId { get; set; }

        // Навигационные свойства
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Lession> Lessions { get; set; }
        public virtual ICollection<Category> Subcategories { get; set; }

    }
}

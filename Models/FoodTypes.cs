namespace CafeManager3.Models
{
    /// <summary>
    /// Модель категорий еды
    /// </summary>
    public class FoodTypes
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название категории
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Фото категории. В виде байтового массива
        /// </summary>
        public byte[]? Icon { get; set; }
    }
}

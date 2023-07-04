namespace CafeManager3.Models
{
    /// <summary>
    /// Модель товара
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = null!;
        /// <summary>
        /// Какой категории товар
        /// </summary>
        public int FoodTypeId { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Ингредиенты
        /// </summary>
        public string Ingredients { get; set; } = null!;
        /// <summary>
        /// Вес
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Калории
        /// </summary>
        public int Calories { get; set; }
        /// <summary>
        /// Изображение товара в байтовом представлении
        /// </summary>
        public byte[]? Icon { get; set; }
    }
}

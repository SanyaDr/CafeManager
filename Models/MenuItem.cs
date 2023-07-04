namespace CafeManager3.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public double Price { get; set; }
        public string Ingredients { get; set; } = null!;
        public int Weight { get; set; }
        public int Calories { get; set; }
        public byte[]? Icon { get; set; }
    }
}

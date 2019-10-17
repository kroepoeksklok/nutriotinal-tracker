namespace NutritionalTracker.Database {
    public class Ingredient {
        public int IngredientId { get; set; }

        public int RecipeId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public Product Product { get; set; }

        public Recipe Recipe { get; set; }
    }
}
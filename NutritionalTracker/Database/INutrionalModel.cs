using System.Data.Entity;
using System.Threading.Tasks;

namespace NutritionalTracker.Database {
    public interface INutrionalModel {
        IDbSet<FoodLog> FoodLogs { get; set; }
        IDbSet<Ingredient> Ingredients { get; set; }
        IDbSet<Meal> Meals { get; set; }
        IDbSet<Producer> Producers { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Recipe> Recipes { get; set; }
        IDbSet<Unit> Units { get; set; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
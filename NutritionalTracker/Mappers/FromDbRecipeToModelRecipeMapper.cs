namespace NutritionalTracker.Mappers
{
    public sealed class FromDbRecipeToModelRecipeMapper : IMapper<Database.Recipe, Models.Recipe>
    {
        public Models.Recipe Map(Database.Recipe recipe)
        {
            return new Models.Recipe
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name
            };
        }
    }
}
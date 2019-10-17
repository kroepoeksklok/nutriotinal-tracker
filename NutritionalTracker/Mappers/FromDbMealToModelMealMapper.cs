namespace NutritionalTracker.Mappers {
    public sealed class FromDbMealToModelMealMapper : IMapper<Database.Meal, Models.Meal> {
        public Models.Meal Map(Database.Meal meal) {
            return new Models.Meal {
                MealId = meal.MealId,
                Name = meal.Name
            };
        }
    }
}
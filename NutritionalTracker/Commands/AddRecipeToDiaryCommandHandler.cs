using System;
using System.Data.Entity;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Commands
{
    public sealed class AddRecipeToDiaryCommandHandler : ICommandHandler<AddRecipeToDiaryCommand>
    {
        private readonly INutrionalModel _context;

        public AddRecipeToDiaryCommandHandler(INutrionalModel context)
        {
            _context = context;
        }

        public void Handle(AddRecipeToDiaryCommand command)
        {
            Recipe recipe = _context.Recipes
                .Include(r => r.Ingredients.Select(i => i.Product))
                .First(r => r.RecipeId == command.RecipeId);

            decimal servingsRatio = (decimal)command.NumberOfServingsConsumed / recipe.Servings;

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                int amountConsumed = (int)Math.Round(ingredient.Amount * servingsRatio, 0, MidpointRounding.AwayFromZero);
                DiaryHelper.AddProductToDiary(_context, amountConsumed, ingredient.Product, command.MealId, command.ConsumedDate);
            }

            _context.SaveChanges();
        }
    }
}
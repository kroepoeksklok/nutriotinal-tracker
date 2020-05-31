using System;

namespace NutritionalTracker.Commands
{
    public sealed class AddRecipeToDiaryCommand : ICommand
    {
        public int RecipeId { get; set; }
        public double NumberOfServingsConsumed { get; set; }
        public byte MealId { get; set; }
        public DateTime ConsumedDate { get; set; }
    }
}

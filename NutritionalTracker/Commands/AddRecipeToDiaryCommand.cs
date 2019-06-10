using System;

namespace NutritionalTracker.Commands
{
    public sealed class AddRecipeToDiaryCommand : ICommand
    {
        public int RecipeId { get; set; }
        public int NumberOfServingsConsumed { get; set; }
        public byte MealId { get; set; }
        public DateTime ConsumedDate { get; set; }
    }
}
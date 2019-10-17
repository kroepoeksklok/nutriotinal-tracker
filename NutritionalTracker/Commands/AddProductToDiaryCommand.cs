using System;

namespace NutritionalTracker.Commands {
    public sealed class AddProductToDiaryCommand : ICommand {
        public int ProductId { get; set; }
        public int AmountConsumed { get; set; }
        public byte MealId { get; set; }
        public DateTime ConsumedDate { get; set; }
    }
}
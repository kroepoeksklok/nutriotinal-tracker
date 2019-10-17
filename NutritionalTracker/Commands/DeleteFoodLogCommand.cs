namespace NutritionalTracker.Commands {
    public sealed class DeleteFoodLogCommand : ICommand {
        public int FoodLogId { get; set; }
    }
}
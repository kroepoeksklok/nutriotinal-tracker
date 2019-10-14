using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Commands
{
    public sealed class DeleteFoodLogCommandHandler : ICommandHandler<DeleteFoodLogCommand>
    {
        private readonly INutrionalModel _context;

        public DeleteFoodLogCommandHandler(INutrionalModel context)
        {
            _context = context;
        }

        public void Handle(DeleteFoodLogCommand command)
        {
            var foodLog = _context.FoodLogs.First(log => log.FoodLogId == command.FoodLogId);
            _context.FoodLogs.Remove(foodLog);
            _context.SaveChanges();
        }
    }
}
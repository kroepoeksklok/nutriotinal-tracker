using NutritionalTracker.Database;
using System.Data.Entity;
using System.Linq;

namespace NutritionalTracker.Commands
{
    public sealed class CopyToDiaryCommandHandler : ICommandHandler<CopyToDiaryCommand>
    {
        private readonly INutrionalModel _context;

        public CopyToDiaryCommandHandler(INutrionalModel context) {
            _context = context;
        }

        public void Handle(CopyToDiaryCommand command) {
            var foodlogs = _context.FoodLogs
                .Include(f => f.Product)
                .Where(f => f.Date == command.DateToCopy.Date && f.MealId == command.MealId);

            foreach(var foodlog in foodlogs) {
                DiaryHelper.CopyFoodlog(_context, foodlog, command.MealId, command.DateToCopyTo);
            }

            _context.SaveChanges();
        }
    }
}
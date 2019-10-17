using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Commands {
    public sealed class AddProductToDiaryCommandHandler : ICommandHandler<AddProductToDiaryCommand> {
        private readonly INutrionalModel _context;

        public AddProductToDiaryCommandHandler(INutrionalModel context) {
            _context = context;
        }

        public void Handle(AddProductToDiaryCommand command) {
            var product = _context.Products.First(p => p.ProductId == command.ProductId);
            DiaryHelper.AddProductToDiary(_context, command.AmountConsumed, product, command.MealId, command.ConsumedDate);
            _context.SaveChanges();
        }
    }
}
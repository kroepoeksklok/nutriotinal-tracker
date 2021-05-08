using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetProductsForDailyLogHandler : IQueryHandler<GetProductsForDailyLogQuery, IReadOnlyList<Product>>
    {
        private readonly INutrionalModel _context;

        public GetProductsForDailyLogHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<Product> Handle(GetProductsForDailyLogQuery query) {
            return _context.Products
                .Where(product => !product.IsIngredientOnly)
                .Include(product => product.Producer)
                .Include(product => product.Unit)
                .OrderByDescending(product => product.IsFavourite)
                .ThenBy(product => product.Producer.Name)
                .ThenBy(product => product.Name)
                .ToList();
        }
    }
}
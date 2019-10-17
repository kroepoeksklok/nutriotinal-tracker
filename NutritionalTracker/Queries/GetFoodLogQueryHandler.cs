using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries {
    public class GetFoodLogQueryHandler : IQueryHandler<GetFoodLogQuery, IReadOnlyList<FoodLog>> {
        private readonly INutrionalModel _context;

        public GetFoodLogQueryHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<FoodLog> Handle(GetFoodLogQuery query) {
            return _context.FoodLogs
                .Include(log => log.Product)
                .Include(log => log.Product.Producer)
                .Include(log => log.Product.Unit)
                .Where(log => log.Date == query.Date)
                .OrderBy(log => log.Product.Name)
                .ToList();
        }
    }
}
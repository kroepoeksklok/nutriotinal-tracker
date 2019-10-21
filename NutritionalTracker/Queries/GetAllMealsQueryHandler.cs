using System.Collections.Generic;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries {
    public class GetAllMealsQueryHandler : IQueryHandler<GetAllMealsQuery, IReadOnlyList<Meal>> {
        private readonly INutrionalModel _context;

        public GetAllMealsQueryHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<Meal> Handle(GetAllMealsQuery query) {
            return _context.Meals.OrderBy(m => m.Name).ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries {
    public class GetAllRecipesQueryHandler : IQueryHandler<GetAllRecipesQuery, IReadOnlyList<Recipe>> {
        private readonly INutrionalModel _context;

        public GetAllRecipesQueryHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<Recipe> Handle(GetAllRecipesQuery query) {
            return _context.Recipes.ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetAllProducersQueryHandler : IQueryHandler<GetAllProducersQuery, IReadOnlyList<Producer>>
    {
        private readonly INutrionalModel _context;

        public GetAllProducersQueryHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<Producer> Handle(GetAllProducersQuery query) {
            return _context.Producers.OrderBy(m => m.Name).ToList();
        }
    }
}
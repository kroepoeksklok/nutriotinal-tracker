using System.Collections.Generic;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetAllProducersQueryHandler : IQueryHandler<GetAllProducersQuery, IReadOnlyList<ProducerListItem>>
    {
        private readonly INutrionalModel _context;

        public GetAllProducersQueryHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<ProducerListItem> Handle(GetAllProducersQuery query) {
            return _context
                .Producers
                .OrderBy(m => m.Name)
                .Select(p => new ProducerListItem {
                    ProducerId = p.ProducerId,
                    Name = p.Name,
                    CanBeDeleted = p.Products.Count == 0
                }).ToList();
        }
    }
}
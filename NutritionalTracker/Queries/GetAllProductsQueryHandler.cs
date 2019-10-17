using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries {
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IReadOnlyList<Product>> {
        private readonly INutrionalModel _context;

        public GetAllProductsQueryHandler(INutrionalModel context) {
            _context = context;
        }

        public IReadOnlyList<Product> Handle(GetAllProductsQuery query) {
            return _context.Products
                .Include(product => product.Producer)
                .Include(product => product.Unit)
                .OrderBy(product => product.Producer.Name)
                .ThenBy(product => product.Name)
                .ToList();
        }
    }
}
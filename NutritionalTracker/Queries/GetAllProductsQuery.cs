using System.Collections.Generic;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetAllProductsQuery : IQuery<IReadOnlyList<Product>>{ }
}
using System.Collections.Generic;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetProductsForDailyLogQuery : IQuery<IReadOnlyList<Product>> { }
}
using System.Collections.Generic;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetAllMealsQuery : IQuery<IReadOnlyList<Meal>>{ }
}
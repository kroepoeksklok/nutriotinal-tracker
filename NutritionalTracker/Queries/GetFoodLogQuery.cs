using System;
using System.Collections.Generic;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries
{
    public class GetFoodLogQuery : IQuery<IReadOnlyList<FoodLog>>
    {
        public DateTime Date { get; set; }
    }
}
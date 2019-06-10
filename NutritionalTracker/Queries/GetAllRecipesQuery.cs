using System.Collections.Generic;

namespace NutritionalTracker.Queries
{
    public class GetAllRecipesQuery : IQuery<IReadOnlyList<Database.Recipe>>{ }
}
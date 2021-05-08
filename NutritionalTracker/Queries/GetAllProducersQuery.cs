using System.Collections.Generic;
using NutritionalTracker.Database;

namespace NutritionalTracker.Queries { 
    public class GetAllProducersQuery : IQuery<IReadOnlyList<ProducerListItem>> { }
}
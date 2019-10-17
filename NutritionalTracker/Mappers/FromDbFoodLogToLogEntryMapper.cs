namespace NutritionalTracker.Mappers {
    public sealed class FromDbFoodLogToLogEntryMapper : IMapper<Database.FoodLog, Models.LogEntry> {
        public Models.LogEntry Map(Database.FoodLog foodLog) {
            return new Models.LogEntry {
                FoodLogId = foodLog.FoodLogId,
                ProductName = foodLog.Product.Name,
                ProducerName = foodLog.Product.Producer.Name,
                Amount = foodLog.Amount,
                GramsOfCarbohydrates = foodLog.ConsumedCarbohydrates,
                GramsOfFat = foodLog.ConsumedFats,
                GramsOfProtein = foodLog.ConsumedProteins,
                Kilocalories = foodLog.ConsumedEnergy,
                Unit = foodLog.Product.Unit.Name
            };
        }
    }
}
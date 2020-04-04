namespace NutritionalTracker.Models {
    public sealed class Statistics {
        public decimal TotalEnergy { get; set; }
        public decimal TotalFats { get; set; }
        public decimal TotalSaturatedFats { get; set; }
        public decimal TotalMonoUnsaturatedFats { get; set; }
        public decimal TotalPolyUnsaturatedFats { get; set; }
        public decimal TotalCarbohydrates { get; set; }
        public decimal TotalSugar { get; set; }
        public decimal TotalProteins { get; set; }
        public decimal TotalSalt { get; set; }
        public decimal TotalCalories { get; set; }

        public void AddLogEntry(Database.FoodLog foodLog) {
            TotalEnergy += foodLog.ConsumedEnergy;
            TotalFats += foodLog.ConsumedFats;
            TotalSaturatedFats += foodLog.ConsumedSaturatedFats;
            TotalMonoUnsaturatedFats += foodLog.ConsumedMonoUnsaturatedFats;
            TotalPolyUnsaturatedFats += foodLog.ConsumedPolyUnsaturatedFats;
            TotalCarbohydrates += foodLog.ConsumedCarbohydrates;
            TotalSugar += foodLog.ConsumedSugar;
            TotalProteins += foodLog.ConsumedProteins;
            TotalSalt += foodLog.ConsumedSalt;
            TotalCalories += foodLog.ConsumedEnergy;
        }
    }
}
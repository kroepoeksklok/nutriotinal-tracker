namespace NutritionalTracker.Models
{
    public sealed class LogEntry
    {
        public int FoodLogId { get; set; }
        public string ProductName { get; set; }
        public string ProducerName { get; set; }
        public int Amount { get; set; }
        public decimal Kilocalories { get; set; }
        public decimal GramsOfFat { get; set; }
        public decimal GramsOfProtein { get; set; }
        public decimal GramsOfCarbohydrates { get; set; }
        public string Unit { get; set; }

        public string LogSummary => $"{ProductName} ({ProducerName}) - {Amount} {Unit}";
    }
}
namespace NutritionalTracker.Models {
    public sealed class Product {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string NameAndProducer => $"{Producer} - {Name}";
        public string Unit { get; set; }
        public bool IsFavourite { get; set; }
    }
}
namespace NutritionalTracker.Models
{
    public sealed class Producer
    {
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public bool CanBeDeleted { get; set; }
    }
}
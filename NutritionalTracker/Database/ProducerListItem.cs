namespace NutritionalTracker.Database
{
    public sealed class ProducerListItem
    {
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public bool CanBeDeleted { get; set; }
    }
}

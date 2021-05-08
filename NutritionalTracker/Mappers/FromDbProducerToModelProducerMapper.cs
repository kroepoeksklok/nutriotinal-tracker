namespace NutritionalTracker.Mappers {
    public sealed class FromDbProducerToModelProducerMapper : IMapper<Database.ProducerListItem, Models.Producer>     {
        public Models.Producer Map(Database.ProducerListItem producer) {
            return new Models.Producer {
                ProducerId = producer.ProducerId,
                Name = producer.Name,
                CanBeDeleted = producer.CanBeDeleted
            };
        }
    }
}
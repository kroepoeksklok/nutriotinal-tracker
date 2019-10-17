namespace NutritionalTracker.Mappers {
    public sealed class FromDbProductToModelProductMapper : IMapper<Database.Product, Models.Product> {
        public Models.Product Map(Database.Product product) {
            return new Models.Product {
                ProductId = product.ProductId,
                Name = product.Name,
                Producer = product.Producer.Name,
                Unit = product.Unit.Name
            };
        }
    }
}
using NutritionalTracker.Database;

namespace NutritionalTracker.Commands {
    public sealed class AddProducerCommandHandler : ICommandHandler<AddProducerCommand> {
        private readonly INutrionalModel _context;

        public AddProducerCommandHandler(INutrionalModel context) {
            _context = context;
        }

        public void Handle(AddProducerCommand command) {
            var producer = new Producer {
                Name = command.Name
            };
            _context.Producers.Add(producer);
            _context.SaveChanges();
        }
    }
}
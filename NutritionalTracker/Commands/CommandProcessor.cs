using System;
using System.Diagnostics;
using SimpleInjector;

namespace NutritionalTracker.Commands
{
    public sealed class CommandProcessor : ICommandProcessor
    {
        private readonly Container _container;

        public CommandProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public void Process(ICommand command)
        {
            Type handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());

            dynamic handler = _container.GetInstance(handlerType);

            handler.Handle((dynamic)command);
        }
    }
}
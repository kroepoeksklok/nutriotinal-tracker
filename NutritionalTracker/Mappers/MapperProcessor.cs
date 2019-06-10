using System;
using System.Diagnostics;
using SimpleInjector;

namespace NutritionalTracker.Mappers
{
    public sealed class MapperProcessor : IMapperProcessor
    {
        private readonly Container _container;

        public MapperProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TTo Map<TFrom, TTo>(TFrom from)
        {
            Type handlerType = typeof(IMapper<,>)
                .MakeGenericType(from.GetType(), typeof(TTo));

            dynamic handler = _container.GetInstance(handlerType);

            return handler.Map((dynamic)from);
        }
    }
}
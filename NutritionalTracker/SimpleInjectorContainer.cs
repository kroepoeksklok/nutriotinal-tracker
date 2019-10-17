using NutritionalTracker.Commands;
using NutritionalTracker.Database;
using NutritionalTracker.Mappers;
using NutritionalTracker.Queries;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.Globalization;
using System.Windows.Data;

namespace NutritionalTracker
{
    public static class SimpleInjectorContainer
    {
        private static Container _container;

        public static void Initialize()
        {
            if (_container == null)
            {
                _container = new Container();
                
                RegisterModel();

                _container.Register(typeof(ICommandHandler<>), typeof(ICommandHandler<>).Assembly);
                _container.Register(typeof(ICommandProcessor), typeof(CommandProcessor));

                _container.Register(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);
                _container.Register(typeof(IQueryProcessor), typeof(QueryProcessor));

                _container.Register(typeof(IMapper<,>), typeof(IMapper<,>).Assembly);
                _container.Register(typeof(IMapperProcessor), typeof(MapperProcessor));

                _container.Verify();
            }
        }

        private static void RegisterModel()
        {
            _container.Register<INutrionalModel, NutrionalModel>();
            Registration registration = _container.GetRegistration(typeof(INutrionalModel)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
        }

        public static T Get<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
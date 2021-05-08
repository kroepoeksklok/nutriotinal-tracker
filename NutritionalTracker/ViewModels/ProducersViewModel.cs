using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using NutritionalTracker.Commands;
using NutritionalTracker.Mappers;
using NutritionalTracker.Queries;
using ICommand = System.Windows.Input.ICommand;

namespace NutritionalTracker.ViewModels
{
    public sealed class ProducersViewModel : INotifyPropertyChanged
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IMapperProcessor _mapperProcessor;
        private readonly ObservableCollection<Models.Producer> _producers;
        private string _newProducerName;

        public ProducersViewModel(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, IMapperProcessor mapperProcessor) {
            _mapperProcessor = mapperProcessor ?? throw new ArgumentNullException(nameof(mapperProcessor));
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));

            _producers = new ObservableCollection<Models.Producer>(GetProducers());

            ProducersView = CollectionViewSource.GetDefaultView(_producers);
            ProducersView.SortDescriptions.Add(new SortDescription(nameof(Models.Producer.Name), ListSortDirection.Ascending));

            AddProducer = new RelayCommand(AddProducerHandler, o => !string.IsNullOrWhiteSpace(NewProducerName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICollectionView ProducersView { get; }
        public ICommand AddProducer { get; set; }

        public string NewProducerName {
            get => _newProducerName;
            set {
                _newProducerName = value;
                OnPropertyChanged(nameof(NewProducerName));
            }
        }


        public IEnumerable<Models.Producer> GetProducers() {
            var producersQuery = new GetAllProducersQuery();
            var dbProducers = _queryProcessor.Process(producersQuery);
            return dbProducers.Select(_mapperProcessor.Map<Database.ProducerListItem, Models.Producer>);
        }

        private void AddProducerHandler(object parameter) {

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
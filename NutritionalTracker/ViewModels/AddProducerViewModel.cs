using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NutritionalTracker.Commands;
using ICommand = System.Windows.Input.ICommand;

namespace NutritionalTracker.ViewModels {
    public sealed class AddProducerViewModel : INotifyPropertyChanged {
        private readonly ICommandProcessor _commandProcessor;
        private string _newProducerName;

        public AddProducerViewModel(ICommandProcessor commandProcessor) {
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));

            AddProducer = new RelayCommand(AddProducerHandler, o => !string.IsNullOrWhiteSpace(NewProducerName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddProducer { get; set; }

        public string NewProducerName {
            get => _newProducerName;
            set {
                _newProducerName = value;
                OnPropertyChanged(nameof(NewProducerName));
            }
        }

        private void AddProducerHandler(object parameter) {
            _commandProcessor.Process(new AddProducerCommand {
                Name = NewProducerName
            });
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
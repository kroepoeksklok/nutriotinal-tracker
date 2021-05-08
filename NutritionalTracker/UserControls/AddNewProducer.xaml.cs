using NutritionalTracker.ViewModels;
using System.Windows.Controls;

namespace NutritionalTracker.UserControls
{
    /// <summary>
    /// Interaction logic for AddNewProducer.xaml
    /// </summary>
    public partial class AddNewProducer
    {
        public AddNewProducer()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.AddProducerViewModel;
        }
    }
}

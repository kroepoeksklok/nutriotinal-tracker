using System.Windows.Controls;

namespace NutritionalTracker.UserControls
{
    /// <summary>
    /// Interaction logic for Producers.xaml
    /// </summary>
    public partial class Producers : UserControl
    {
        public Producers()
        {
            InitializeComponent();
            DataContext = ViewModels.ViewModelLocator.ProducersViewModel;
        }
    }
}

using System.Windows;

namespace NutritionalTracker.UserControls {
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics {
        public static readonly DependencyProperty StatsProperty = DependencyProperty.Register(nameof(Stats), typeof(Models.Statistics), typeof(Statistics), new FrameworkPropertyMetadata(default(Models.Statistics)) {
            BindsTwoWayByDefault = true
        });

        public Models.Statistics Stats {
            get => (Models.Statistics) GetValue(StatsProperty);
            set => SetValue(StatsProperty, value);
        }

        public Statistics() {
            InitializeComponent();
        }
    }
}

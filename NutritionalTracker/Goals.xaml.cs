using System.Windows;
using System.Windows.Controls;

namespace NutritionalTracker {
    /// <summary>
    /// Interaction logic for Goals.xaml
    /// </summary>
    public partial class Goals : UserControl {
        public static readonly DependencyProperty GoalProgressProperty = DependencyProperty.Register(nameof(GoalProgress), typeof(Models.GoalProgress), typeof(Goals), new FrameworkPropertyMetadata(default(Models.GoalProgress)) {
            BindsTwoWayByDefault = true
        });

        public Models.GoalProgress GoalProgress {
            get => (Models.GoalProgress)GetValue(GoalProgressProperty);
            set => SetValue(GoalProgressProperty, value);
        }

        public Goals() {
            InitializeComponent();
        }
    }
}

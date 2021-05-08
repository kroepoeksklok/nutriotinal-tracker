using System.Collections.Generic;
using System.Windows;

namespace NutritionalTracker.UserControls {
    /// <summary>
    /// Interaction logic for Goals.xaml
    /// </summary>
    public partial class Goals {
        public static readonly DependencyProperty MyGoalsProperty = DependencyProperty.Register(nameof(MyGoals), typeof(IEnumerable<Models.Goal>), typeof(Goals), new FrameworkPropertyMetadata(default(IEnumerable<Models.Goal>)) {
            BindsTwoWayByDefault = true
        });

        public IEnumerable<Models.Goal> MyGoals {
            get => (IEnumerable<Models.Goal>) GetValue(MyGoalsProperty);
            set => SetValue(MyGoalsProperty, value);
        }

        public Goals() {
            InitializeComponent();
        }
    }
}

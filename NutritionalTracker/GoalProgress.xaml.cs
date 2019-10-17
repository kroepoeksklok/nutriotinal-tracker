using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NutritionalTracker {
    /// <summary>
    /// Interaction logic for GoalProgress.xaml
    /// </summary>
    public partial class GoalProgress : UserControl {
        public static readonly DependencyProperty GoalProperty = DependencyProperty.Register(nameof(Goal), typeof(int), typeof(GoalProgress), new FrameworkPropertyMetadata(default(int)) {
            BindsTwoWayByDefault = true
        });
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(nameof(Progress), typeof(decimal), typeof(GoalProgress), new FrameworkPropertyMetadata(default(decimal)) {
            BindsTwoWayByDefault = true
        });
        public static readonly DependencyProperty TotalProperty = DependencyProperty.Register(nameof(Total), typeof(decimal), typeof(GoalProgress), new FrameworkPropertyMetadata(default(decimal)) {
            BindsTwoWayByDefault = true
        });
        public static readonly DependencyProperty BarColorProperty = DependencyProperty.Register(nameof(BarColor), typeof(Brush), typeof(GoalProgress), new FrameworkPropertyMetadata(default(Brush)) {
            BindsTwoWayByDefault = true
        });
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(GoalProgress), new FrameworkPropertyMetadata(default(string)) {
            BindsTwoWayByDefault = true
        });
        public static readonly DependencyProperty GoalExceededProperty = DependencyProperty.Register(nameof(GoalExceeded), typeof(bool), typeof(GoalProgress), new FrameworkPropertyMetadata(default(bool)) {
            BindsTwoWayByDefault = true
        });

        public int Goal {
            get => (int)GetValue(GoalProperty);
            set => SetValue(GoalProperty, value);
        }

        public decimal Progress {
            get => (decimal)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public decimal Total {
            get => (decimal)GetValue(TotalProperty);
            set => SetValue(TotalProperty, value);
        }

        public Brush BarColor {
            get => (Brush)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        public string Label {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public bool GoalExceeded {
            get => (bool) GetValue(GoalExceededProperty);
            set => SetValue(GoalExceededProperty, value);
        }

        public GoalProgress() {
            InitializeComponent();
        }
    }
}

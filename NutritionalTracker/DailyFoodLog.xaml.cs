using System.Windows;
using System.Windows.Input;
using NutritionalTracker.Models;

namespace NutritionalTracker
{
    /// <summary>
    /// Interaction logic for DailyFoodLog.xaml
    /// </summary>
    public partial class DailyFoodLog
    {
        public static readonly DependencyProperty DailyLogProperty = DependencyProperty.Register(nameof(DailyLog), typeof(DailyLog), typeof(DailyFoodLog), new FrameworkPropertyMetadata(default(DailyLog))
        {
            BindsTwoWayByDefault = true
        });

        public DailyLog DailyLog
        {
            get => (DailyLog) GetValue(DailyLogProperty);
            set => SetValue(DailyLogProperty, value);
        }


        public static readonly DependencyProperty DeleteLogCommandProperty = DependencyProperty.Register(nameof(DeleteLogCommand), typeof(ICommand), typeof(DailyFoodLog), new PropertyMetadata(default(ICommand)));

        public ICommand DeleteLogCommand
        {
            get => (ICommand) GetValue(DeleteLogCommandProperty);
            set => SetValue(DeleteLogCommandProperty, value);
        }

        public DailyFoodLog()
        {
            InitializeComponent();
        }
    }
}

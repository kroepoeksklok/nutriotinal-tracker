using System.Windows.Media;

namespace NutritionalTracker.Models {
    public sealed class Goal {
        public Goal(string name, decimal totalIntake, int goal) {
            TotalIntake = totalIntake;
            IntakeGoal = goal;
            Name = name;
        }

        public string Name { get; set; }
        public decimal TotalIntake { get; set; }
        public Brush BarColor { get; set; }
        public int IntakeGoal { get; set; }

        public decimal Progress {
            get => CalculateProgress();
            set { }
        }

        public bool GoalExceeded {
            get => TotalIntake > IntakeGoal;
            set { }
        }

        private decimal CalculateProgress() {
            var progress = TotalIntake / IntakeGoal;
            return progress > 1 ? 1 : progress;
        }
    }
}
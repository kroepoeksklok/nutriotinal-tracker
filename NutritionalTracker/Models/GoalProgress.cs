namespace NutritionalTracker.Models {
    public sealed class GoalProgress {
        public GoalProgress(Statistics statistics, SetGoals goals) {
            TotalCarbohydrates = statistics.TotalCarbohydrates;
            TotalProteins = statistics.TotalProteins;
            TotalFats = statistics.TotalFats;
            Goals = goals;
        }

        public decimal TotalCarbohydrates { get; set; }
        public decimal TotalProteins { get; set; }
        public decimal TotalFats { get; set; }

        public SetGoals Goals { get; set; }

        public decimal CarbohydratesProgress {
            get => GetProgress(TotalCarbohydrates, Goals.Carbohydrates);
            set { }
        }
        public decimal ProteinProgress {
            get => GetProgress(TotalProteins, Goals.Proteins);
            set { }
        }

        public decimal FatProgress {
            get => GetProgress(TotalFats, Goals.Fats);
            set { }
        }

        private decimal GetProgress(decimal totalAmount, int goal) {
            var progress = totalAmount / goal;
            return progress > 1 ? 1 : progress;
        }
    }
}
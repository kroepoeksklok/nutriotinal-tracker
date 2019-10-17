namespace NutritionalTracker.Models {
    public struct SetGoals {
        public int Carbohydrates { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }

        public SetGoals(int carbohydrates, int proteins, int fats) {
            Carbohydrates = carbohydrates;
            Proteins = proteins;
            Fats = fats;
        }
    }
}
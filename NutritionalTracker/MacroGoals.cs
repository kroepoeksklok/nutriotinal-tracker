using System.Configuration;

namespace NutritionalTracker
{
    public static class MacroGoals
    {
        public static int Carbohydrates => GetGoal("Goals.Cardohydrates");
        public static int Proteins => GetGoal("Goals.Proteins");
        public static int Fats => GetGoal("Goals.Fats");
        public static int Calories => GetGoal("Goals.Calories");

        private static int GetGoal(string configurationValue) {
            return int.Parse(ConfigurationManager.AppSettings[configurationValue]);
        }
    }
}
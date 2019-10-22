using System;
using System.Collections.Generic;

namespace NutritionalTracker.ViewModels {
    public sealed class MealComparer : IEqualityComparer<Models.Meal> {
        public bool Equals(Models.Meal x, Models.Meal y) {
            if (x == null && y == null) return true;
            if (x == null && y != null) return false;
            if (x != null && y == null) return false;
            return x.MealId == y.MealId;
        }

        public int GetHashCode(Models.Meal obj) {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.MealId.GetHashCode();
        }
    }
}

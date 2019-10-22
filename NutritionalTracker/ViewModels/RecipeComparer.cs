using System;
using System.Collections.Generic;

namespace NutritionalTracker.ViewModels {
    public sealed class RecipeComparer : IEqualityComparer<Models.Recipe> {
        public bool Equals(Models.Recipe x, Models.Recipe y) {
            if (x == null && y == null) return true;
            if (x == null && y != null) return false;
            if (x != null && y == null) return false;
            return x.RecipeId == y.RecipeId;
        }

        public int GetHashCode(Models.Recipe obj) {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.RecipeId.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;

namespace NutritionalTracker.ViewModels {
    public sealed class ProductComparer : IEqualityComparer<Models.Product> {
        public bool Equals(Models.Product x, Models.Product y) {
            if (x == null && y == null) return true;
            if (x == null && y != null) return false;
            if (x != null && y == null) return false;
            return x.ProductId == y.ProductId;
        }

        public int GetHashCode(Models.Product obj) {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.ProductId.GetHashCode();
        }
    }
}

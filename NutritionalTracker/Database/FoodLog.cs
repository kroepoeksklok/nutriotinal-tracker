using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutritionalTracker.Database {
    public class FoodLog {
        public int FoodLogId { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public byte UnitId { get; set; }

        public byte MealId { get; set; }

        public decimal ConsumedEnergy { get; set; }

        public decimal ConsumedFats { get; set; }

        public decimal ConsumedSaturatedFats { get; set; }

        public decimal ConsumedMonoUnsaturatedFats { get; set; }

        public decimal ConsumedPolyUnsaturatedFats { get; set; }

        public decimal ConsumedCarbohydrates { get; set; }

        public decimal ConsumedSugar { get; set; }

        public decimal ConsumedProteins { get; set; }

        public decimal ConsumedSalt { get; set; }

        public Meal Meal { get; set; }

        public Product Product { get; set; }

        public Unit Unit { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutritionalTracker.Database {
    public class Meal {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meal() {
            FoodLogs = new HashSet<FoodLog>();
        }

        public byte MealId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<FoodLog> FoodLogs { get; set; }
    }
}

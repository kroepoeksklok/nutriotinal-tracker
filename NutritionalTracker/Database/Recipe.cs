using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutritionalTracker.Database {
    public class Recipe {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recipe() {
            Ingredients = new HashSet<Ingredient>();
        }

        public int RecipeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int Servings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}

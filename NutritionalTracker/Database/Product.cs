using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutritionalTracker.Database
{
    public class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            FoodLogs = new HashSet<FoodLog>();
            Ingredients = new HashSet<Ingredient>();
        }

        public int ProductId { get; set; }

        public int ProducerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Barcode { get; set; }

        public int ValuesPer { get; set; }

        public byte UnitId { get; set; }

        public decimal Energy { get; set; }

        public decimal Fats { get; set; }

        public decimal SaturatedFats { get; set; }

        public decimal MonoUnsaturatedFats { get; set; }

        public decimal PolyUnsaturatedFats { get; set; }

        public decimal Carbohydrates { get; set; }

        public decimal Sugar { get; set; }

        public decimal Proteins { get; set; }

        public decimal Salt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<FoodLog> FoodLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Ingredient> Ingredients { get; set; }

        public Producer Producer { get; set; }

        public Unit Unit { get; set; }
    }
}

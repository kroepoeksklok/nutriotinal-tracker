using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutritionalTracker.Database {
    public class Producer {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer() {
            Products = new HashSet<Product>();
        }

        public int ProducerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Product> Products { get; set; }
    }
}

using System.Data.Entity;

namespace NutritionalTracker.Database {
    public class NutrionalModel : DbContext, INutrionalModel {
        public NutrionalModel() : base("name=NutrionalModel") {
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<FoodLog> FoodLogs { get; set; }
        public IDbSet<Ingredient> Ingredients { get; set; }
        public IDbSet<Meal> Meals { get; set; }
        public IDbSet<Producer> Producers { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Recipe> Recipes { get; set; }
        public IDbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedEnergy)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedSaturatedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedMonoUnsaturatedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedPolyUnsaturatedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedCarbohydrates)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedSugar)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedProteins)
                .HasPrecision(9, 2);

            modelBuilder.Entity<FoodLog>()
                .Property(e => e.ConsumedSalt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Meal>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Meal>()
                .HasMany(e => e.FoodLogs)
                .WithRequired(e => e.Meal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Producer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Energy)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Fats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.SaturatedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.MonoUnsaturatedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.PolyUnsaturatedFats)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Carbohydrates)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Sugar)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Proteins)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Salt)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.FoodLogs)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Ingredients)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Recipe>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Recipe>()
                .HasMany(e => e.Ingredients)
                .WithRequired(e => e.Recipe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.FoodLogs)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(false);
        }
    }
}

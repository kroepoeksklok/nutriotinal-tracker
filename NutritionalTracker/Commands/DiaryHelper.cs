using System;
using NutritionalTracker.Database;

namespace NutritionalTracker.Commands {
    internal static class DiaryHelper {
        public static void AddProductToDiary(INutrionalModel context, int amountConsumed, Product product, byte mealId, DateTime consumedDate) {
            var amountConsumedRatio = (decimal)amountConsumed / product.ValuesPer;

            context.FoodLogs.Add(new FoodLog {
                ConsumedCarbohydrates = product.Carbohydrates * amountConsumedRatio,
                Amount = amountConsumed,
                ConsumedEnergy = product.Energy * amountConsumedRatio,
                ConsumedFats = product.Fats * amountConsumedRatio,
                ConsumedMonoUnsaturatedFats = product.MonoUnsaturatedFats * amountConsumedRatio,
                ConsumedPolyUnsaturatedFats = product.PolyUnsaturatedFats * amountConsumedRatio,
                ConsumedProteins = product.Proteins * amountConsumedRatio,
                ConsumedSalt = product.Salt * amountConsumedRatio,
                ConsumedSaturatedFats = product.SaturatedFats * amountConsumedRatio,
                ConsumedSugar = product.Sugar * amountConsumedRatio,
                ProductId = product.ProductId,
                Date = consumedDate,
                UnitId = product.UnitId,
                MealId = mealId
            });
        }

        public static void CopyFoodlog(INutrionalModel context, FoodLog foodLog, byte mealId, DateTime consumedDate) {
            context.FoodLogs.Add(new FoodLog {
                ConsumedCarbohydrates = foodLog.ConsumedCarbohydrates,
                Amount = foodLog.Amount,
                ConsumedEnergy = foodLog.ConsumedEnergy,
                ConsumedFats = foodLog.ConsumedFats,
                ConsumedMonoUnsaturatedFats = foodLog.ConsumedMonoUnsaturatedFats,
                ConsumedPolyUnsaturatedFats = foodLog.ConsumedPolyUnsaturatedFats,
                ConsumedProteins = foodLog.ConsumedProteins,
                ConsumedSalt = foodLog.ConsumedSalt,
                ConsumedSaturatedFats = foodLog.ConsumedSaturatedFats,
                ConsumedSugar = foodLog.ConsumedSugar,
                ProductId = foodLog.ProductId,
                Date = consumedDate,
                UnitId = foodLog.UnitId,
                MealId = mealId
            });
        }
    }
}
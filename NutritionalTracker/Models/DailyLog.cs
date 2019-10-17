using System;
using System.Collections.Generic;
using System.Linq;
using NutritionalTracker.Mappers;

namespace NutritionalTracker.Models {
    public sealed class DailyLog {
        private readonly IMapperProcessor _mapperProcessor;

        public DailyLog(IMapperProcessor mapperProcessor, IReadOnlyList<Meal> meals) {
            if (meals == null) {
                throw new ArgumentNullException(nameof(meals));
            }

            _mapperProcessor = mapperProcessor ?? throw new ArgumentNullException(nameof(mapperProcessor));
            PrepareDictionary(meals);
        }

        public Dictionary<Meal, ICollection<LogEntry>> MealsWithProducts { get; private set; }

        public void AddLogEntry(Database.FoodLog foodLog) {
            var meal = MealsWithProducts.Keys.First(m => m.MealId == foodLog.MealId);
            MealsWithProducts[meal].Add(_mapperProcessor.Map<Database.FoodLog, LogEntry>(foodLog));
        }

        private void PrepareDictionary(IEnumerable<Meal> meals) {
            MealsWithProducts = new Dictionary<Meal, ICollection<LogEntry>>();
            foreach (var meal in meals) {
                MealsWithProducts.Add(meal, new List<LogEntry>());
            }
        }
    }
}
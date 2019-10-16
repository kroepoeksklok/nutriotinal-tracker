using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NutritionalTracker.Commands;
using NutritionalTracker.Database;
using NutritionalTracker.Mappers;
using NutritionalTracker.Models;
using NutritionalTracker.Queries;
using ICommand = System.Windows.Input.ICommand;

namespace NutritionalTracker.ViewModels {
    public sealed class MainWindowViewModel : INotifyPropertyChanged {
        private ObservableCollection<Models.Recipe> _recipes;
        private ObservableCollection<Models.Product> _products;
        private ObservableCollection<Models.Meal> _meals;
        private Models.Recipe _selectedRecipe;
        private Models.Product _selectedProduct;
        private Models.Meal _selectedMeal;
        private DailyLog _dailyLog;
        private Models.Statistics _statistics;
        private double _servingsEaten;
        private int _amountOfProductConsumed;
        private DateTime _selectedDate;
        private string _unit;

        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IMapperProcessor _mapperProcessor;

        private IReadOnlyList<FoodLog> _logEntries;

        public MainWindowViewModel(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, IMapperProcessor mapperProcessor) {
            _mapperProcessor = mapperProcessor ?? throw new ArgumentNullException(nameof(mapperProcessor));
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));

            var data = GetData();

            Recipes = new ObservableCollection<Models.Recipe>(data.Item1);
            SelectedRecipe = Recipes.First();

            Products = new ObservableCollection<Models.Product>(data.Item2);
            SelectedProduct = Products.First();

            Meals = new ObservableCollection<Models.Meal>(data.Item3);
            SelectedMeal = Meals.First();

            SelectedDate = DateTime.Now.Date;
            Unit = "gr / ml";

            AddProductToDiary = new RelayCommand(AddProductToDiaryHandler, o => SelectedProduct != null && _amountOfProductConsumed > 0 && SelectedMeal != null);
            AddRecipeToDiary = new RelayCommand(AddRecipeToDiaryHandler, o => SelectedRecipe != null && _servingsEaten > 0 && SelectedMeal != null);
            DeleteFoodLog = new RelayCommand(DeleteFoodLogHandler);

            Task.Run(() => {
                while (true) {
                    Thread.Sleep(10000);
                    var newData = GetData();

                    var currentlySelectedProductId = SelectedProduct.ProductId;
                    var currentlySelectedMealId = SelectedMeal.MealId;
                    var currentlySelectedRecipeId = SelectedRecipe.RecipeId;

                    SelectedMeal = null;
                    SelectedProduct = null;
                    SelectedRecipe = null;

                    Recipes = new ObservableCollection<Models.Recipe>(newData.Item1);
                    Products = new ObservableCollection<Models.Product>(newData.Item2);
                    Meals = new ObservableCollection<Models.Meal>(newData.Item3);

                    SelectedRecipe = Recipes.FirstOrDefault(r => r.RecipeId == currentlySelectedRecipeId);
                    SelectedProduct = Products.FirstOrDefault(p => p.ProductId == currentlySelectedProductId);
                    SelectedMeal = Meals.FirstOrDefault(m => m.MealId == currentlySelectedMealId);
                }
            });
        }

        public ICommand AddRecipeToDiary { get; set; }
        public ICommand AddProductToDiary { get; set; }
        public ICommand DeleteFoodLog { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Unit {
            get => _unit;
            set {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        public ObservableCollection<Models.Recipe> Recipes {
            get => _recipes;
            set {
                _recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public Models.Recipe SelectedRecipe {
            get => _selectedRecipe;
            set {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        public double ServingsEaten {
            get => _servingsEaten;
            set {
                _servingsEaten = value;
                OnPropertyChanged(nameof(ServingsEaten));
            }
        }


        public ObservableCollection<Models.Product> Products {
            get => _products;
            set {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public Models.Product SelectedProduct {
            get => _selectedProduct;
            set {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                if (value != null) {
                    Unit = value.Unit;
                }
            }
        }

        public int AmountOfProductConsumed {
            get => _amountOfProductConsumed;
            set {
                _amountOfProductConsumed = value;
                OnPropertyChanged(nameof(AmountOfProductConsumed));
            }
        }


        public ObservableCollection<Models.Meal> Meals {
            get => _meals;
            set {
                _meals = value;
                OnPropertyChanged(nameof(Meals));
            }
        }

        public Models.Meal SelectedMeal {
            get => _selectedMeal;
            set {
                _selectedMeal = value;
                OnPropertyChanged(nameof(SelectedMeal));
            }
        }

        public DateTime SelectedDate {
            get => _selectedDate;
            set {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                LoadDailyData();
            }
        }

        public DailyLog DailyLog {
            get => _dailyLog;
            set {
                _dailyLog = value;
                OnPropertyChanged(nameof(DailyLog));
            }
        }

        public Models.Statistics Statistics {
            get => _statistics;
            set {
                _statistics = value;
                OnPropertyChanged(nameof(Statistics));
            }
        }

        private void AddProductToDiaryHandler(object parameter) {
            _commandProcessor.Process(new AddProductToDiaryCommand {
                AmountConsumed = AmountOfProductConsumed,
                ProductId = SelectedProduct.ProductId,
                MealId = SelectedMeal.MealId,
                ConsumedDate = SelectedDate
            });

            LoadDailyData();
        }

        private void AddRecipeToDiaryHandler(object parameter) {
            _commandProcessor.Process(new AddRecipeToDiaryCommand {
                NumberOfServingsConsumed = ServingsEaten,
                RecipeId = SelectedRecipe.RecipeId,
                MealId = SelectedMeal.MealId,
                ConsumedDate = SelectedDate
            });

            LoadDailyData();
        }

        private void DeleteFoodLogHandler(object parameter) {
            var logentry = (LogEntry)parameter;
            _commandProcessor.Process(new DeleteFoodLogCommand {
                FoodLogId = logentry.FoodLogId
            });

            LoadDailyData();
        }

        private Tuple<List<Models.Recipe>, List<Models.Product>, List<Models.Meal>> GetData() {
            var recipesQuery = new GetAllRecipesQuery();
            var dbRecipes = _queryProcessor.Process(recipesQuery);

            var productsQuery = new GetAllProductsQuery();
            var dbProducts = _queryProcessor.Process(productsQuery);

            var mealsQuery = new GetAllMealsQuery();
            var dbMeals = _queryProcessor.Process(mealsQuery);

            return Tuple.Create(
                new List<Models.Recipe>(dbRecipes.Select(_mapperProcessor.Map<Database.Recipe, Models.Recipe>)),
                new List<Models.Product>(dbProducts.Select(_mapperProcessor.Map<Database.Product, Models.Product>)),
                new List<Models.Meal>(dbMeals.Select(_mapperProcessor.Map<Database.Meal, Models.Meal>)));
        }

        private void LoadDailyData() {
            LoadLogEntries();
            LoadFoodLog();
            LoadStatistics();
        }


        private void LoadLogEntries() {
            var getFoodLogQuery = new GetFoodLogQuery {
                Date = SelectedDate
            };
            _logEntries = _queryProcessor.Process(getFoodLogQuery);
        }

        private void LoadFoodLog() {
            var dailyLog = new DailyLog(_mapperProcessor, Meals);
            foreach (var foodLog in _logEntries) {
                dailyLog.AddLogEntry(foodLog);
            }

            DailyLog = dailyLog;
        }

        private void LoadStatistics() {
            var statistics = new Models.Statistics();
            foreach (var foodLog in _logEntries) {
                statistics.AddLogEntry(foodLog);
            }

            Statistics = statistics;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

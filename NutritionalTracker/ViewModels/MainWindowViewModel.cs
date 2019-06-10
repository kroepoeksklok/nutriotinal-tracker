using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using NutritionalTracker.Commands;
using NutritionalTracker.Database;
using NutritionalTracker.Mappers;
using NutritionalTracker.Models;
using NutritionalTracker.Queries;
using ICommand = System.Windows.Input.ICommand;

namespace NutritionalTracker.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<Models.Recipe> _recipes;
        private List<Models.Product> _products;
        private List<Models.Meal> _meals;
        private Models.Recipe _selectedRecipe;
        private Models.Product _selectedProduct;
        private Models.Meal _selectedMeal;
        private DailyLog _dailyLog;
        private Models.Statistics _statistics;
        private int _servingsEaten;
        private int _amountOfProductConsumed;
        private DateTime _selectedDate;
        private string _unit;

        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IMapperProcessor _mapperProcessor;

        private IReadOnlyList<FoodLog> _logEntries;

        public MainWindowViewModel(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, IMapperProcessor mapperProcessor)
        {
            _mapperProcessor = mapperProcessor ?? throw new ArgumentNullException(nameof(mapperProcessor));
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));

            LoadData();

            SelectedDate = DateTime.Now.Date;
            Unit = "gr / ml";

            AddProductToDiary = new RelayCommand(AddProductToDiaryHandler, o => SelectedProduct != null && _amountOfProductConsumed > 0 && SelectedMeal != null);
            AddRecipeToDiary = new RelayCommand(AddRecipeToDiaryHandler, o => SelectedRecipe != null && _servingsEaten > 0 && SelectedMeal != null);
            DeleteFoodLog = new RelayCommand(DeleteFoodLogHandler);
        }

        public ICommand AddRecipeToDiary { get; set; }
        public ICommand AddProductToDiary { get; set; }
        public ICommand DeleteFoodLog { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        public List<Models.Recipe> Recipes
        {
            get => _recipes;
            set
            {
                _recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public Models.Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        public int ServingsEaten
        {
            get => _servingsEaten;
            set
            {
                _servingsEaten = value;
                OnPropertyChanged(nameof(ServingsEaten));
            }
        }


        public List<Models.Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public Models.Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                Unit = value.Unit;
            }
        }

        public int AmountOfProductConsumed
        {
            get => _amountOfProductConsumed;
            set
            {
                _amountOfProductConsumed = value;
                OnPropertyChanged(nameof(AmountOfProductConsumed));
            }
        }


        public List<Models.Meal> Meals
        {
            get => _meals;
            set
            {
                _meals = value;
                OnPropertyChanged(nameof(Meals));
            }
        }

        public Models.Meal SelectedMeal
        {
            get => _selectedMeal;
            set
            {
                _selectedMeal = value;
                OnPropertyChanged(nameof(SelectedMeal));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                LoadDailyData();
            }
        }

        public DailyLog DailyLog
        {
            get => _dailyLog;
            set
            {
                _dailyLog = value;
                OnPropertyChanged(nameof(DailyLog));
            }
        }

        public Models.Statistics Statistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                OnPropertyChanged(nameof(Statistics));
            }
        }

        private void AddProductToDiaryHandler(object parameter)
        {
            _commandProcessor.Process(new AddProductToDiaryCommand
            {
                AmountConsumed = AmountOfProductConsumed,
                ProductId = SelectedProduct.ProductId,
                MealId = SelectedMeal.MealId,
                ConsumedDate = SelectedDate
            });

            LoadDailyData();
        }

        private void AddRecipeToDiaryHandler(object parameter)
        {
            _commandProcessor.Process(new AddRecipeToDiaryCommand
            {
                NumberOfServingsConsumed = ServingsEaten,
                RecipeId = SelectedRecipe.RecipeId,
                MealId = SelectedMeal.MealId,
                ConsumedDate = SelectedDate
            });

            LoadDailyData();
        }

        private void DeleteFoodLogHandler(object parameter)
        {
            LogEntry logentry = (LogEntry) parameter;
            _commandProcessor.Process(new DeleteFoodLogCommand
            {
                FoodLogId = logentry.FoodLogId
            });

            LoadDailyData();

        }
        
        private void LoadData()
        {
            GetAllRecipesQuery recipesQuery = new GetAllRecipesQuery();
            IReadOnlyList<Database.Recipe> dbRecipes = _queryProcessor.Process(recipesQuery);
            Recipes = new List<Models.Recipe>(dbRecipes.Select(_mapperProcessor.Map<Database.Recipe, Models.Recipe>));

            GetAllProductsQuery productsQuery = new GetAllProductsQuery();
            IReadOnlyList<Database.Product> dbProducts = _queryProcessor.Process(productsQuery);
            Products = new List<Models.Product>(dbProducts.Select(_mapperProcessor.Map<Database.Product, Models.Product>));

            GetAllMealsQuery mealsQuery = new GetAllMealsQuery();
            IReadOnlyList<Database.Meal> dbMeals = _queryProcessor.Process(mealsQuery);
            Meals = new List<Models.Meal>(dbMeals.Select(_mapperProcessor.Map<Database.Meal, Models.Meal>));
        }

        private void LoadDailyData()
        {
            LoadLogEntries();
            LoadFoodLog();
            LoadStatistics();
        }
        

        private void LoadLogEntries()
        {
            GetFoodLogQuery getFoodLogQuery = new GetFoodLogQuery
            {
                Date = SelectedDate
            };
            _logEntries = _queryProcessor.Process(getFoodLogQuery);
        }

        private void LoadFoodLog()
        {
            DailyLog dailyLog = new DailyLog(_mapperProcessor, Meals);
            foreach (FoodLog foodLog in _logEntries)
            {
                dailyLog.AddLogEntry(foodLog);
            }

            DailyLog = dailyLog;
        }

        private void LoadStatistics()
        {
            Models.Statistics statistics = new Models.Statistics();
            foreach (FoodLog foodLog in _logEntries)
            {
                statistics.AddLogEntry(foodLog);
            }

            Statistics = statistics;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

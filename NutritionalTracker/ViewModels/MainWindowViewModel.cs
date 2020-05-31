using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using NutritionalTracker.Commands;
using NutritionalTracker.Database;
using NutritionalTracker.Mappers;
using NutritionalTracker.Models;
using NutritionalTracker.Queries;
using ICommand = System.Windows.Input.ICommand;

namespace NutritionalTracker.ViewModels {
    public sealed class MainWindowViewModel : INotifyPropertyChanged {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IMapperProcessor _mapperProcessor;
        private readonly ObservableCollection<Models.Recipe> _recipes;
        private readonly ObservableCollection<Models.Product> _products;
        private readonly ObservableCollection<Models.Meal> _meals;
        private Models.Recipe _selectedRecipe;
        private Models.Product _selectedProduct;
        private Models.Meal _selectedMeal;
        private Models.Meal _selectedMealForCopy;
        private DailyLog _dailyLog;
        private Models.Statistics _statistics;
        private ObservableCollection<Goal> _goals;
        private double _servingsEaten;
        private int _amountOfProductConsumed;
        private DateTime _selectedDate;
        private DateTime _selectedDateForCopy;
        private string _unit;
        private IReadOnlyList<FoodLog> _logEntries;

        public MainWindowViewModel(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, IMapperProcessor mapperProcessor) {
            _mapperProcessor = mapperProcessor ?? throw new ArgumentNullException(nameof(mapperProcessor));
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
            _commandProcessor = commandProcessor ?? throw new ArgumentNullException(nameof(commandProcessor));

            var data = GetData();

            _recipes = new ObservableCollection<Models.Recipe>(data.Item1);
            RecipesView = CollectionViewSource.GetDefaultView(_recipes);
            RecipesView.SortDescriptions.Add(new SortDescription(nameof(Models.Recipe.Name), ListSortDirection.Ascending));
            SelectedRecipe = _recipes.First();

            _products = new ObservableCollection<Models.Product>(data.Item2);
            ProductsView = CollectionViewSource.GetDefaultView(_products);
            ProductsView.SortDescriptions.Add(new SortDescription(nameof(Models.Product.IsFavourite), ListSortDirection.Descending));
            ProductsView.SortDescriptions.Add(new SortDescription(nameof(Models.Product.NameAndProducer), ListSortDirection.Ascending));
            SelectedProduct = _products.First();

            _meals = new ObservableCollection<Models.Meal>(data.Item3);
            MealsView = CollectionViewSource.GetDefaultView(_meals);
            MealsView.SortDescriptions.Add(new SortDescription(nameof(Models.Meal.Name), ListSortDirection.Ascending));
            SelectedMeal = _meals.First();

            SelectedDate = DateTime.Now.Date;
            Unit = "gr / ml";

            AddProductToDiary = new RelayCommand(AddProductToDiaryHandler, o => SelectedProduct != null && _amountOfProductConsumed > 0 && SelectedMeal != null);
            AddRecipeToDiary = new RelayCommand(AddRecipeToDiaryHandler, o => SelectedRecipe != null && _servingsEaten > 0 && SelectedMeal != null);
            CopyToDiary = new RelayCommand(CopyToDiaryHandler, o => SelectedMealForCopy != null);
            DeleteFoodLog = new RelayCommand(DeleteFoodLogHandler);

            Task.Run(() => {
                while (true) {
                    Thread.Sleep(10000);
                    var newData = GetData();

                    LoadNewData(newData.Item1, _recipes, new RecipeComparer());
                    LoadNewData(newData.Item2, _products, new ProductComparer());
                    LoadNewData(newData.Item3, _meals, new MealComparer());
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollectionView RecipesView { get; }
        public ICollectionView ProductsView { get; }
        public ICollectionView MealsView { get; }
        public ICommand AddRecipeToDiary { get; set; }
        public ICommand AddProductToDiary { get; set; }
        public ICommand DeleteFoodLog { get; set; }
        public ICommand CopyToDiary { get; set; }

        public string Unit {
            get => _unit;
            set {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
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

        public Models.Meal SelectedMeal {
            get => _selectedMeal;
            set {
                _selectedMeal = value;
                OnPropertyChanged(nameof(SelectedMeal));
            }
        }

        public Models.Meal SelectedMealForCopy {
            get => _selectedMealForCopy;
            set {
                _selectedMealForCopy = value;
                OnPropertyChanged(nameof(SelectedMealForCopy));
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

        public DateTime SelectedDateForCopy {
            get => _selectedDateForCopy;
            set {
                _selectedDateForCopy = value;
                OnPropertyChanged(nameof(SelectedDateForCopy));
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

        public ObservableCollection<Models.Goal> Goals {
            get => _goals;
            set {
                _goals = value;
                OnPropertyChanged(nameof(Goals));
            }
        }

        private void LoadNewData<T>(IEnumerable<T> newItems, ObservableCollection<T> currentItems, IEqualityComparer<T> comparer) {
            var itemsToAdd = newItems.Except(currentItems, comparer);
            foreach (var item in itemsToAdd) {
                System.Windows.Application.Current.Dispatcher.Invoke(() => currentItems.Add(item));
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

        private void CopyToDiaryHandler(object parameter) {
            _commandProcessor.Process(new CopyToDiaryCommand {
                MealId = SelectedMealForCopy.MealId,
                DateToCopy = SelectedDateForCopy,
                DateToCopyTo = SelectedDate
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
            LoadGoals();
        }

        private void LoadLogEntries() {
            var getFoodLogQuery = new GetFoodLogQuery {
                Date = SelectedDate
            };
            _logEntries = _queryProcessor.Process(getFoodLogQuery);
        }

        private void LoadFoodLog() {
            var dailyLog = new DailyLog(_mapperProcessor, _meals);
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

        private void LoadGoals() {
            var goals = new ObservableCollection<Goal>(new List<Goal> {
                new Goal("Carbohydrates", Statistics.TotalCarbohydrates, MacroGoals.Carbohydrates) { BarColor = new SolidColorBrush(Color.FromRgb(255,0,0)) },
                new Goal("Proteins", Statistics.TotalProteins, MacroGoals.Proteins) { BarColor = new SolidColorBrush(Color.FromRgb(0,255,0)) },
                new Goal("Fats", Statistics.TotalFats, MacroGoals.Fats) { BarColor = new SolidColorBrush(Color.FromRgb(0,0,255)) },
                new Goal("Calories", Statistics.TotalCalories, MacroGoals.Calories) { BarColor = new SolidColorBrush(Color.FromRgb(255,255,0)) },
            });
            Goals = goals;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
namespace NutritionalTracker.ViewModels {
    public static class ViewModelLocator {
        public static MainWindowViewModel MainWindowViewModel => SimpleInjectorContainer.Get<MainWindowViewModel>();
    }
}
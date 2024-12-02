using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CSWPFTemplate.Common.Windows;
using CSWPFTemplate.Models;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace CSWPFTemplate.ViewModels
{
    public sealed partial class CategoryEditViewModel : ClosableWindow
    {
        #region Constructor
        public CategoryEditViewModel(ILogger<CategoryEditViewModel> logger)
        {
            Logger = logger;
        }
        #endregion

        #region Services
        public ILogger<CategoryEditViewModel> Logger { get; }

        #endregion

        #region Data
        [ObservableProperty]
        private Category? category;

        public void InitData(Category? category = null)
        {
            Category = category ?? new Category();
        }
        #endregion

        #region Commands
        [RelayCommand]
        public async Task Save()
        {
            try
            {
                if (Category != null)
                {
                    //TODO save data with something like await CategoryService.SaveCategory(Category.ToCategoryDataModel());
                }
                //TODO message the main window that a new category was added (reload the list or manually add new one?)
                CloseWindow();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while saving the category");
                MessageBox.Show("Error while saving the category", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public void Cancel()
        {
            CloseWindow();
        }
        #endregion
    }
}

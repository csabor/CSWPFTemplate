using CommunityToolkit.Mvvm.ComponentModel;
using CSWPFTemplate.Common.Validation;

namespace CSWPFTemplate.Models
{
    [ObservableObject] //defined as attribute because this class needs to inherit from ValidatableObject
    public sealed partial class Category : ValidatableObject
    {
        [ObservableProperty]
        private string? categoryName;

        [ObservableProperty]
        private string? categoryId;

        [ObservableProperty]
        private bool isNewPlaceholder = false;

        public Category()
        {
        }

        public Category(string categoryName, string? categoryId, bool isNewPlaceholder)
        {
            this.categoryName = categoryName;
            this.categoryId = categoryId;
            this.isNewPlaceholder = isNewPlaceholder;
        }
    }
}

using FluentValidation;
using System.Collections;
using System.ComponentModel;

namespace CSWPFTemplate.Common.Validation
{
    public class ValidatableObject : INotifyDataErrorInfo
    {

        private IValidator? validator;

        public IValidator? Validator
        {
            get => validator;
            set
            {
                validator = value;
            }
        }

        /// <inheritdoc />
        public virtual void OnPropertyMessagesChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }


        #region INotifyDataErrorInfo

        /// <inheritdoc />
        bool INotifyDataErrorInfo.HasErrors => false; //TODO !IsValid;

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        /// <inheritdoc />
        IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
        {
            //TODO
            //if (Validator == null)
            //    return Array.Empty<ValidationMessage>();

            //return string.IsNullOrEmpty(propertyName)
            //    ? Validator.ValidationMessages
            //    : Validator.GetMessages(propertyName!);
            return Array.Empty<object>();
        }

        #endregion
    }
}

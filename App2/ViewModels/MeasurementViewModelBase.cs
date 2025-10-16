using System;
using System.ComponentModel;

namespace App2.ViewModels
{
    /// <summary>
    /// Serves as a base class for measurement view models, providing common properties and methods for input handling and validation.
    /// </summary>
    public abstract class MeasurementViewModelBase : INotifyPropertyChanged
    {
        protected string _inputText = "";
        private bool _hasValidationError = false;
        private string _validationMessage = "";

        public string Label { get; set; }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                ValidateAndParseInput(value);
                OnPropertyChanged(nameof(InputText));
                OnPropertyChanged(nameof(HasValidationError));
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }

        public bool HasValidationError
        {
            get => _hasValidationError;
            protected set
            {
                _hasValidationError = value;
                OnPropertyChanged(nameof(HasValidationError));
            }
        }

        public string ValidationMessage
        {
            get => _validationMessage;
            protected set
            {
                _validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }
        protected abstract void ValidateAndParseInput(string input);
        public abstract double GetValueAsDouble();
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
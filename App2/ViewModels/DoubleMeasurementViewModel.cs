using System;
using System.Globalization;

namespace App2.ViewModels
{
    public class DoubleMeasurementViewModel : MeasurementViewModelBase
    {
        private double? _value;

        public double? Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        protected override void ValidateAndParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Value = null;
                HasValidationError = true;
                ValidationMessage = "Wert ist erforderlich";
                return;
            }


            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                if (double.IsNaN(result))
                {
                    Value = null;
                    HasValidationError = true;
                    ValidationMessage = "Wert muss eine gültige Zahl sein";
                }
                else if (double.IsInfinity(result))
                {
                    Value = null;
                    HasValidationError = true;
                    ValidationMessage = "Wert darf nicht unendlich sein";
                }
                else
                {
                    Value = Math.Round(result, 3);
                    HasValidationError = false;
                    ValidationMessage = "";
                    
                    // optional: format the input text to always show three decimal places
                    /*
                    if (_value.HasValue)
                    {
                        string formattedValue = _value.Value.ToString("F3", CultureInfo.InvariantCulture);
                        if (InputText != formattedValue)
                        {
                            _inputText = formattedValue;
                            OnPropertyChanged(nameof(InputText));
                        }
                    }
                    */
                }
            }
            else
            {
                Value = null;
                HasValidationError = true;
                ValidationMessage = "Bitte geben Sie eine gültige Dezimalzahl ein";
            }
        }

        public override double GetValueAsDouble() => Value.GetValueOrDefault();
    }
}
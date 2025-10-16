
using System;
using System.Globalization;

namespace App2.ViewModels
{
    public class IntegerMeasurementViewModel : MeasurementViewModelBase
    {
        private int? _value;

        public int? Value
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

            if (int.TryParse(input.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int result))
            {
                Value = result;
                HasValidationError = false;
                ValidationMessage = "";
            }
            else
            {
                Value = null;
                HasValidationError = true;
                ValidationMessage = "Bitte geben Sie eine gültige Ganzzahl ein";
            }
        }

        public override double GetValueAsDouble() => Value.GetValueOrDefault();
    }
}
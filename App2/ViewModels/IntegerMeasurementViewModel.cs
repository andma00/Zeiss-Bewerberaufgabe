
using System;
using System.Globalization;

namespace App2.ViewModels
{
    /// <summary>
    /// Represents a view model for handling integer measurements, implementing input validation and parsing logic.
    /// </summary>
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

        /// <summary>
        /// Validates the given input string and attempts to parse it as a 32-bit integer.
        /// Updates the internal state to reflect the parsed value or any validation errors.
        /// </summary>
        protected override void ValidateAndParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Value = null;
                HasValidationError = true;
                ValidationMessage = "Wert ist erforderlich";
                return;
            }

            if (int.TryParse(input.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using App2.Services;

namespace App2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IExporter _exporter;

        public ObservableCollection<MeasurementViewModelBase> Measurements { get; set; }
        public ICommand ProcessAndSaveCommand { get; }

        public MainViewModel()
        {
            _exporter = new FileExporter();

            Measurements = new ObservableCollection<MeasurementViewModelBase>
            {
                new DoubleMeasurementViewModel { Label = "Wert 1 (Fließkommazahl)" },
                new DoubleMeasurementViewModel { Label = "Wert 2 (Fließkommazahl)" },
                new IntegerMeasurementViewModel { Label = "Wert 3 (Ganzzahl)" }
            };

            ProcessAndSaveCommand = new RelayCommand(ProcessAndSave, CanProcessAndSave);
        }

        private bool CanProcessAndSave(object parameter)
        {
            return Measurements.All(m => !m.HasValidationError && !string.IsNullOrWhiteSpace(m.InputText));
        }

        private void ProcessAndSave(object parameter)
        {
            try
            {
                var values = Measurements.Select(m => m.GetValueAsDouble()).ToList();
                values.Sort();

                _exporter.Export(values);
                MessageBox.Show("Werte erfolgreich verarbeitet und gespeichert.", "Erfolg", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
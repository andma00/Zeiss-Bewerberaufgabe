using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using App2.Models; 
using App2.Services;

namespace App2.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IExporter _exporter;

        private double? _value1;
        public double? Value1
        {
            get => _value1;
            set { _value1 = value; OnPropertyChanged(nameof(Value1)); }
        }

        private double? _value2;
        public double? Value2
        {
            get => _value2;
            set { _value2 = value; OnPropertyChanged(nameof(Value2)); }
        }

        private int? _value3;
        public int? Value3
        {
            get => _value3;
            set { _value3 = value; OnPropertyChanged(nameof(Value3)); }
        }

        public ICommand ProcessAndSaveCommand { get; }

        public MainViewModel()
        {
            _exporter = new FileExporter();

            ProcessAndSaveCommand = new RelayCommand(ProcessAndSave, CanProcessAndSave);
        }

        private bool CanProcessAndSave(object parameter)
        {
            return Value1.HasValue && Value2.HasValue && Value3.HasValue;
        }

        private void ProcessAndSave(object parameter)
        {
            try
            {
                var values = new List<double> { Value1.Value, Value2.Value, (double)Value3.Value };

                values.Sort();

                _exporter.Export(values);

                MessageBox.Show("Werte erfolgreich verarbeitet und gespeichert.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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

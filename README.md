# Patient measurements


## Purpose

---

Graphical user interface for entering, validating, sorting, and storing patient measurements.

## How to run

---
1. Open the solution `App2.sln` in Visual Studio.
2. Set the startup project to `App2`.
3. Run the application (F5).
4. Enter the measurements and click "Process and Save".
5. Check the user directory for the `values.txt` file containing the sorted measurements.

## Functionalities

---

* Entry of three patient measurements (two floating point numbers, one integer).
* Real-time validation of user entries.
* Sorting of measurements in ascending order.
* Storage of measurements in a text file (values.txt) in the user directory.
* Error handling for unexpected errors during the storage process.

## Architecture

---

![Diagram.jpg](Diagram.jpg)

## Classes

---

### IExporter (Service)

Defines an interface with a method `Export(IEnumerable<double> sortedValues)` for exporting measured values.

- Methods: `void Export(IEnumerable<double> sortedValues)`
- Implementations: `FileExporter`, `ConsoleExporter`

### MeasurementViewModelBase (ViewModels)

The abstract base class that provides an interface for validating inputs. It implements INotifyPropertyChanged.

- Properties: `string Label`, `string InputText`, `string HasValidationError`, `string ValidationMessage`.
- Methods: `void ValidateAndParseInput(string input)`, `double GetValueAsDouble()`.
- Implementations: `DoubleMeasurementViewModel`, `IntegerMeasurementViewModel`

### MainViewModel (ViewModels)

Main ViewModel that serves as the DataContext for `MainWindows.xaml`. Contains the logic for user interaction and the measured values to be displayed for the view.

- Properties: `ObservableCollection<MeasurementViewModelBase> Measurements`, `ICommand ProcessAndSaveCommand`, `IExporter exporter`.
- Methods: `ProcessAndSave()`, `CanProcessAndSave()`

### RelayCommand (ViewModels/MainViewModel)
Encapsulation of methods as ICommands, which is necessary for WPF view binding.



## Extensibility

---

### Adding new export targets:
```csharp
//Services/IExporter.cs
    public interface IExporter
    {
        void Export(IEnumerable<double> sortedValues);
    }
```
```csharp
//Services/
    class MyCustomExporter: IExporter
    {
        public void Export(IEnumerable<double> sortedValues)
        {
            //... Custom export logic
        }
    }
```
### Change export destination:
```csharp
//ViewModels/MainViewModel.cs
        public MainViewModel()
        {
            
            _exporter = new MyCustomExporter();
            
            //...

        }
```


### Add new measurement types:
```csharp
//ViewModels/MeasurementViewModelBase.cs
    public abstract class MeasurementViewModelBase : INotifyPropertyChanged
    {
        //...
        public abstract bool ValidateAndParseInput();
        public abstract double GetValueAsDouble();
    }
```
```csharp
//ViewModels/
    class MyCustomMeasurementViewModel : MeasurementViewModelBase
    {
        public override bool ValidateAndParseInput()
        {
            //... Custom validation logic
        }

        public override double GetValueAsDouble()
        {
            //... Custom conversion logic
        }
    }
```

###### Developed as part of an applicant task for Carl Zeiss Meditec AG.
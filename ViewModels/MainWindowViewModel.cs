using System.Collections.ObjectModel;
using System.ComponentModel;
using OptiHeatPro;

namespace OptiHeatPro.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private HeatingData _heatingData;
        private ObservableCollection<DataEntry> _winterData;
        private ObservableCollection<DataEntry> _summerData;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public ObservableCollection<DataEntry> WinterData
        {
            get { return _winterData; }
            set
            {
                if (_winterData != value)
                {
                    _winterData = value;
                    OnPropertyChanged(nameof(WinterData));
                }
            }
        }

        public ObservableCollection<DataEntry> SummerData
        {
            get { return _summerData; }
            set
            {
                if (_summerData != value)
                {
                    _summerData = value;
                    OnPropertyChanged(nameof(SummerData));
                }
        }
        }

        public Boiler Boiler1 { get; } = new Boiler("GB", 5.0, 500, 215, 1.1);
        public Boiler Boiler2 { get; } = new Boiler("OB", 4.0, 700, 265, 1.2);
        public Boiler Boiler3 { get; } = new Boiler("GM", 3.6, 2.7, 1100, 640, 1.9);
        public Boiler Boiler4 { get; } = new Boiler("EK", 8.0, -8.0, 50);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainWindowViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _heatingData = new HeatingData();
            _heatingData.Read();
#pragma warning disable CS8604 // Possible null reference argument.
            WinterData = new ObservableCollection<DataEntry>(_heatingData.WinterData);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
            SummerData = new ObservableCollection<DataEntry>(_heatingData.SummerData);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // This method is called to notify the UI that a property has changed
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

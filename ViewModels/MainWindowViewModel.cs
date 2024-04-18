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

        public event PropertyChangedEventHandler PropertyChanged;

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

        public MainWindowViewModel()
        {
            _heatingData = new HeatingData();
            _heatingData.Read();
            WinterData = new ObservableCollection<DataEntry>(_heatingData.WinterData);
            SummerData = new ObservableCollection<DataEntry>(_heatingData.SummerData);
        }

        // This method is called to notify the UI that a property has changed
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

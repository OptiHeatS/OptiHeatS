using System.Collections.ObjectModel;
using ReactiveUI;

namespace OptiHeatPro.ViewModels
{
    public class HeatingViewModel : ViewModelBase
    {
        // Heating data lists.
        private HeatingData _heatingData;
        private ObservableCollection<DataEntry> _winterData;
        private ObservableCollection<DataEntry> _summerData;

        public ObservableCollection<DataEntry> WinterData
        {
            get { return _winterData; }
            set { this.RaiseAndSetIfChanged(ref _winterData, value); }
        }

        public ObservableCollection<DataEntry> SummerData
        {
            get { return _summerData; }
            set { this.RaiseAndSetIfChanged(ref _summerData, value); }
        }

        public HeatingViewModel()
        {
            _heatingData = new HeatingData();
            _heatingData.Read();
            WinterData = new ObservableCollection<DataEntry>(_heatingData.WinterData);
            SummerData = new ObservableCollection<DataEntry>(_heatingData.SummerData);
        }
    }
}

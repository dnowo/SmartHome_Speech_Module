using SHSM.Devices;
using System.Collections.Specialized;
using System.Linq;

namespace SHSM {
    public partial class Home
    {
        private Place _placeToShow;
        public Place PlaceToShow
        {
            get => _placeToShow;
            set { _placeToShow = value; RefreshItemsSource(null, null); }
        }

        private TrulyObservableCollection<Device> fullListOfDevices;
        public Home()
        {
            InitializeComponent();
        }

        public void SetDevices(TrulyObservableCollection<Device> devices)
        {
            fullListOfDevices = devices;
            fullListOfDevices.CollectionChanged += RefreshItemsSource;
            PlaceToShow = Place.ALL;
        }

        private void RefreshItemsSource(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DeviceList.ItemsSource = fullListOfDevices.Where(device => PlaceToShow == Place.ALL || device.Place == PlaceToShow);
        }
    }
}
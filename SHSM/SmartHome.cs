using System.Linq;
using System.Windows.Media;
using EnumsNET;
using SHSM.Devices;

namespace SHSM
{
    public class SmartHome
    {
        private readonly DialogController dialogController;
        private readonly DatabaseDriver databaseDriver;
        private readonly MainWindow mainWindow;

        private TrulyObservableCollection<Device> devices = new();

        public SmartHome(MainWindow mainWindow)
        {
            dialogController = new DialogController(this);
            databaseDriver = new DatabaseDriver();
            databaseDriver.Database.EnsureCreated();
            this.mainWindow = mainWindow;

            // PopulateDatabase();
            LoadData();
        }

        private void LoadData()
        {
            devices = new TrulyObservableCollection<Device>(databaseDriver.Devices);
            mainWindow.HomePage.SetDevices(devices);
        }

        private void PopulateDatabase()
        {
            databaseDriver.Lights.Add(new Light{Name = "Światło", Place = Devices.Place.KITCHEN, NumericalState = 50});
            databaseDriver.Lights.Add(new Light{Name = "Światło", Place = Devices.Place.DAYROOM});
            databaseDriver.Doors.Add(new Door{Name = "Drzwi", Place = Devices.Place.DAYROOM});
            databaseDriver.SaveChanges();
        }

        public void SetDeviceState(string type, string place, bool state)
        {
            if (!devices.Any(device => device.Type.AsString(EnumFormat.Description) == type
                                       && device.Place.AsString(EnumFormat.Description) == place))
            {
                return;
            }

            Device device = devices.First(device => device.Type.AsString(EnumFormat.Description) == type 
                                                    && device.Place.AsString(EnumFormat.Description) == place);
            device.State = state;
            databaseDriver.SaveChanges();
        }

        public void SetDeviceNumericalState(string type, string place, int state)
        {
            if (!devices.Any(device => device.Type.AsString(EnumFormat.Description) == type
                                       && device.Place.AsString(EnumFormat.Description) == place))
            {
                return;
            }

            Device device = devices.First(device => device.Type.AsString(EnumFormat.Description) == type 
                                                    && device.Place.AsString(EnumFormat.Description) == place);
            device.GetType().GetProperty("NumericalState")?.SetValue(device, state);
            databaseDriver.SaveChanges();
        }

        public void IndicateSpeechRecognition(bool speechRecognition)
        {
            mainWindow.recognitionStatus.Fill = speechRecognition ? Brushes.ForestGreen : Brushes.Red;
        }

        public void Stop()
        {
            dialogController.Stop();
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SHSM.Devices;

namespace SHSM
{
    public class SmartHome
    {
        private readonly DialogController dialogController;
        private readonly DatabaseDriver databaseDriver;
        private readonly MainWindow mainWindow;

        private List<Device> devices = new();

        public SmartHome(MainWindow mainWindow)
        {
            dialogController = new DialogController(this);
            databaseDriver = new DatabaseDriver();
            databaseDriver.Database.EnsureCreated();
            this.mainWindow = mainWindow;

            LoadData();
            // PopulateDatabase();
        }

        private void LoadData()
        {
            devices = (List<Device>)devices.Concat(databaseDriver.Lights.Cast<Device>().ToList());
            devices = (List<Device>)devices.Concat(databaseDriver.Doors.Cast<Device>().ToList());
        }

        private void PopulateDatabase()
        {
            // TODO obrazki będzie trzeba tworzyć w kodzie na podstawie typu, że jak light to żarówka i podpinać, a potem WPF niech zasysa z programu
            databaseDriver.Lights.Add(new Light{Name = "Światło", Place = "kitchen", Image = mainWindow.HomePage.kitchenLightImage});
            databaseDriver.Lights.Add(new Light{Name = "Światło", Place = "dayroom", Image = mainWindow.HomePage.dayRoomLightImage});

            databaseDriver.Doors.Add(new Door{Name = "Drzwi", Place = "dayroom", Image = mainWindow.HomePage.dayRoomLightImage});

            databaseDriver.SaveChanges();
        }

        public void SetDeviceState(string type, string place, bool state)
        {
            // TODO Zlokalizuj odpowiedni device i ustaw mu state, zapisz do bazy
            Device device = devices.FirstOrDefault((System.Func<Device, bool>)(device => device.Type == type && device.Place == place));
            device.Image.Visibility = state ? Visibility.Visible : Visibility.Hidden;
        }

        public void SetDeviceNumericalState(string type, string place, int state)
        {
            // TODO Zlokalizuj odpowiedni device, w zależności od jego typu ustaw mu odpowiedni property, zapisz do bazy
            Device device = devices.FirstOrDefault((System.Func<Device, bool>)(device => device.Type == type && device.Place == place));
            device.GetType().GetProperty("NumericalState")?.SetValue(device, state);
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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using SHSM.Devices;

namespace SHSM
{
    public partial class MainWindow : Window
    {
        private SmartHome smartHome;
        public Home HomePage;
        public Menu MenuPage;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.Closed += Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuPage = new Menu();
            HomePage = new Home();
            smartHome = new SmartHome(this);
            mainFrame.Navigate(HomePage);
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            smartHome.Stop();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void HomeClick(object sender, MouseButtonEventArgs e)
        {
            HomePage.PlaceToShow = Place.ALL;
        }

        private void Home_MouseEnter(object sender, MouseEventArgs e)
        {
            ttHome.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void KitchenClick(object sender, MouseButtonEventArgs e)
        {
            HomePage.PlaceToShow = Place.KITCHEN;
        }

        private void Kitchen_MouseEnter(object sender, MouseEventArgs e)
        {
            ttKitchen.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void DayroomClick(object sender, MouseButtonEventArgs e)
        {
            HomePage.PlaceToShow = Place.DAYROOM;
        }

        private void Dayroom_MouseEnter(object sender, MouseEventArgs e)
        {
            ttDayroom.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void LivingroomClick(object sender, MouseButtonEventArgs e)
        {
            HomePage.PlaceToShow = Place.LIVINGROOM;
        }

        private void Livingroom_MouseEnter(object sender, MouseEventArgs e)
        {
            ttLivingroom.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void BathroomClick(object sender, MouseButtonEventArgs e)
        {
            HomePage.PlaceToShow = Place.BATHROOM;
        }

        private void Bathroom_MouseEnter(object sender, MouseEventArgs e)
        {
            ttBathroom.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void HallClick(object sender, MouseButtonEventArgs e)
        {
            HomePage.PlaceToShow = Place.HALL;
        }

        private void Hall_MouseEnter(object sender, MouseEventArgs e)
        {
            ttHall.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Speech_MouseEnter(object sender, MouseEventArgs e)
        {
            ttSpeech.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}

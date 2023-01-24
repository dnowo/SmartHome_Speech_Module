using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

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
            mainFrame.Navigate(HomePage);
        }

        private void InfoClick(object sender, MouseButtonEventArgs e)
        {
            //mainFrame.Navigate(infoPage);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ttHome.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using SHSM;

namespace SHSM3
{
    public partial class MainWindow : Window
    {
        public DatabaseDriver db;

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private SpeechEngine speechEngine;
        public Home homePage;
        public Menu menuPage;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.Closed += Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            speechEngine = new SpeechEngine(recognitionStatus);
            db = new DatabaseDriver();
            menuPage = new Menu();
            homePage = new Home(speechEngine, db);
            mainFrame.Navigate(homePage);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            speechEngine.stopEngine();
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

        private void windowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void homeClick(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(homePage);
        }

        private void infoClick(object sender, MouseButtonEventArgs e)
        {
            //mainFrame.Navigate(infoPage);
        }

        private void listViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ttHome.Visibility = toggleBtn.IsChecked == true ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.X86;
using Microsoft.Speech.Recognition.SrgsGrammar;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;
using SHSM;

namespace SHSM3
{
    public partial class MainWindow : Window
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private SpeechEngine speechEngine;
        public Home homePage = new Home();
        public Menu menuPage;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.Closed += Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            menuPage = new Menu();
            speechEngine = new SpeechEngine(recognitionStatus);
            mainFrame.Navigate(menuPage);
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

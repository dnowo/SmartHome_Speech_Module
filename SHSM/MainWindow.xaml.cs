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
using System;
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

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.Closed += Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            speechEngine = new SpeechEngine(recognitionStatus);
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

    }
}

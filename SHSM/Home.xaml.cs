using Microsoft.Speech.Recognition;
using SHSM;
using System;

namespace SHSM3 {
    public partial class Home
    {
        private SpeechEngine speechEngine;

        public Home(SpeechEngine speechEngine)
        {
            InitializeComponent();
            this.speechEngine = speechEngine;
            speechEngine.HomeActions += subscribeHomeActions;
        }

        void subscribeHomeActions(object sender, SpeechRecognizedEventArgs e)
        {
            if (sender == null)
            {
                Console.WriteLine("Nie odebrano eventu.");
                return;
            }
            Console.WriteLine("Pomyślnie wykryto mowę.");
            System.Diagnostics.Trace.WriteLine(SpeechEngine.GetValue(e.Result.Semantics, "TurnOnLightInRoom"));
            System.Diagnostics.Trace.WriteLine(SpeechEngine.GetConfidence(e.Result.Semantics, "TurnOnLightInRoom"));
            System.Diagnostics.Trace.WriteLine("----------");
            handleSpeech(e);
        }

        private void handleSpeech(SpeechRecognizedEventArgs e)
        {
            string keyname = "TurnOnLightInRoom";
            string result = SpeechEngine.GetValue(e.Result.Semantics, keyname);
            if (result.Length > 0)
            {
                string c = SpeechEngine.GetConfidence(e.Result.Semantics, keyname);
                System.Diagnostics.Trace.WriteLine(c);
                if ((float)Convert.ToDouble(c) > 0.9)
                {
                    kitchenLightImage.Visibility = System.Windows.Visibility.Visible;
                    System.Diagnostics.Trace.WriteLine(SpeechEngine.GetValue(e.Result.Semantics, keyname).Substring(keyname.Length));
                }
            }
        }

    }
}
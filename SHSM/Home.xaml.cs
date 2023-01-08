using Microsoft.Speech.Recognition;
using SHSM;
using System;

namespace SHSM3 {
    public partial class Home
    {
        private SpeechEngine speechEngine;
        private DatabaseDriver db;

        public Home(SpeechEngine speechEngine, DatabaseDriver db)
        {
            InitializeComponent();
            this.speechEngine = speechEngine;
            speechEngine.HomeActions += subscribeHomeActions;
            this.db = db;
            // Odczyt z DB ostatniego stanu włączenia światła
            // TODO!
        }

        void subscribeHomeActions(object sender, SpeechRecognizedEventArgs e)
        {
            if (sender == null)
            {
                Console.WriteLine("Nie odebrano eventu.");
                return;
            }
            handleTurnOnLight(e);
            handleTurnOffLight(e);
            handleBrightness(e);
        }

        private void handleTurnOnLight(SpeechRecognizedEventArgs e)
        {
            string keyname = "TurnOnLightInRoom";
            string result = SpeechEngine.GetValue(e.Result.Semantics, keyname);
            if (result.Length > 0)
            {
                logRecognition(e, keyname);
                if (SpeechEngine.existsAndConfident(e.Result.Semantics, keyname))
                {
                    if (result.Equals("Włącz światło"))
                    {
                        speechEngine.pTTS.Speak("Gdzie włączyć światło? Przykład: Włącz światło w kuchni");
                        return;
                    }
                    else if (result.Contains("kuchni"))
                    {
                        brightnessKitchen.Text = "todo" + "%"; // odczyt przy włączeniu światłą z db
                        kitchenLightImage.Visibility = System.Windows.Visibility.Visible;
                        // PUT do DB z zmianą wartości TODO
                    }
                    else if (result.Contains("pokoju"))
                    {
                        brightnessDayRoom.Text = "todo" + "%"; // odczyt przy włączeniu światłą z db
                        // PUT do DB z zmianą wartości TODO
                        dayRoomLightImage.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        private void handleTurnOffLight(SpeechRecognizedEventArgs e)
        {
            string keyname = "TurnOffLightInRoom";
            string result = SpeechEngine.GetValue(e.Result.Semantics, keyname);
            if (result.Length > 0)
            {
                logRecognition(e, keyname);
                if (SpeechEngine.existsAndConfident(e.Result.Semantics, keyname))
                {
                    if (result.Equals("Wyłącz światło"))
                    {
                        speechEngine.pTTS.Speak("Gdzie wyłączyć światło? Przykład: Wyłącz światło w kuchni");
                        return;
                    }
                    else if (result.Contains("kuchni"))
                    {
                        brightnessKitchen.Text = "0%";
                        kitchenLightImage.Visibility = System.Windows.Visibility.Hidden;
                        // PUT do DB z zmianą wartości TODO
                    }
                    else if (result.Contains("pokoju"))
                    {
                        brightnessDayRoom.Text = "0%";
                        dayRoomLightImage.Visibility = System.Windows.Visibility.Hidden;
                        // PUT do DB z zmianą wartości TODO
                    }
                }
            }
        }

        private void handleBrightness(SpeechRecognizedEventArgs e)
        {
            string keyname = "BrightnessInRoom";
            string result = SpeechEngine.GetValue(e.Result.Semantics, keyname);
            if (result.Length > 0)
            {
                logRecognition(e, keyname);
                if (SpeechEngine.existsAndConfident(e.Result.Semantics, keyname))
                {
                    if (result.Equals("Ustaw jasność"))
                    {
                        speechEngine.pTTS.Speak("Gdzie ustawić jasność oraz na ile procent?? Przykład: Jasność w kuchni 20 procent");
                        return;
                    }
                    else if (result.Contains("kuchni"))
                    {
                        string brightnessPercentage = result.Split(" ")[3];
                        brightnessKitchen.Text = brightnessPercentage + "%";
                        // PUT do DB z zmianą wartości TODO
                    }
                    else if (result.Contains("pokoju"))
                    {
                        string brightnessPercentage = result.Split(" ")[3];
                        brightnessDayRoom.Text = brightnessPercentage + "%";
                        // PUT do DB z zmianą wartości TODO
                    }
                }
            }
        }

        private static void logRecognition(SpeechRecognizedEventArgs e, string keyname)
        {
            System.Diagnostics.Trace.WriteLine(SpeechEngine.GetValue(e.Result.Semantics, keyname) + " || Confidence: " + SpeechEngine.GetConfidence(e.Result.Semantics, keyname));
        }

    }
}
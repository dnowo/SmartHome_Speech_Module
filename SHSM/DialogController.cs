using Microsoft.Speech.Recognition;

namespace SHSM
{
    public class DialogController
    {
        private TextToSpeechEngine TTS;
        private SpeechRecognitionEngine SRE;
        private readonly SmartHome smartHome;

        private string? prevAction;
        private string? prevObject;
        private string? prevPlace;

        public DialogController(SmartHome smartHome)
        {
            TTS = new TextToSpeechEngine();
            SRE = new SpeechRecognitionEngine();
            this.smartHome = smartHome;
            SRE.SpeechRecognized += HandleSpeechRecognition;
            SRE.AudioStateChanged += HandleAudioStateChanged;
        }

        private void HandleAudioStateChanged(object? sender, AudioStateChangedEventArgs e)
        {
            if (e.AudioState == AudioState.Speech)
            {
                smartHome.IndicateSpeechRecognition(true);
            }
            else if (e.AudioState == AudioState.Silence)
            {
                if (SRE.IsGrammarEnable("SH") == false)
                {
                    SRE.DisableAllGrammars();
                    SRE.ToggleGrammar("SH", true);
                }

                smartHome.IndicateSpeechRecognition(false);
            }
        }

        public void HandleSpeechRecognition(object? sender, SpeechRecognizedEventArgs e)
        {
            /*
             * {Włącz} {światło} {w kuchni}
             * {Wyłącz} {światło} {w kuchni}
             * {Zapal} {światło} {w pokoju}
             * {Zgaś} {światło} {w pokoju}
             * {Włącz} {żyrandol} {w pokoju}
             * {Włącz} {telewizor} {w pokoju}
             * {Zapal} {światło} {na korytarzu}
             * Ustaw {jasność} światła {w pokoju} na {40%}
            */
            string action = SpeechRecognitionEngine.GetValue(e.Result.Semantics, "action");
            string targetObject = SpeechRecognitionEngine.GetValue(e.Result.Semantics, "object");
            string targetPlace = SpeechRecognitionEngine.GetValue(e.Result.Semantics, "place");

            action = prevAction ?? action;
            targetObject = prevObject ?? targetObject;
            targetPlace = prevPlace ?? targetPlace;

            if (action.Length <= 0) return;

            if (targetPlace.Length <= 0)
            {
                prevAction = action;
                prevObject = targetObject;

                SRE.DisableAllGrammars();
                SRE.ToggleGrammar("place", true);
                TTS.SpeakAsync("Gdzie?");
                return;
            }

            switch (action)
            {
                case "brightness":
                {
                    targetObject = "light";

                    string strPower = SpeechRecognitionEngine.GetValue(e.Result.Semantics, "number");

                    if (strPower.Length <= 0 || int.TryParse(strPower, out int power) == false)
                    {
                        prevAction = action;
                        prevObject = targetObject;
                        prevPlace = targetPlace;

                        SRE.DisableAllGrammars();
                        SRE.ToggleGrammar("number", true);
                        TTS.SpeakAsync("Na ile procent?");
                        return;
                    }

                    smartHome.SetDeviceNumericalState(targetObject, targetPlace, power);
                    break;
                }
                case "on":
                case "off":
                    smartHome.SetDeviceState(targetObject, targetPlace, action == "on");
                    break;
            }
            
            prevAction = null;
            prevObject = null;
            prevPlace = null;
            SRE.DisableAllGrammars();
            SRE.ToggleGrammar("SH", true);
        }

        public void Stop()
        {
            SRE.StopEngine();
        }
    }
}

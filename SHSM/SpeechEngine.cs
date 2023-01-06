using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using Microsoft.Speech.Synthesis;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SHSM
{
    public partial class SpeechEngine
    {
        private SpeechSynthesizer pTTS;
        private SpeechRecognitionEngine pSRE;
        private Ellipse recognitionIndicator;

        public delegate void SmartHomeEventHandler(object sender, SpeechRecognizedEventArgs e);
        public event SmartHomeEventHandler HomeActions;

        public SpeechEngine(Ellipse recognitionIndicator)
        {
            this.recognitionIndicator = recognitionIndicator;
            pTTS = new SpeechSynthesizer();
            pTTS.SetOutputToDefaultAudioDevice();

            pSRE = new SpeechRecognitionEngine();
            pSRE.SetInputToDefaultAudioDevice();
            pSRE.SpeechRecognized += PSRE_SpeechRecognized;
            pSRE.AudioStateChanged += SRE_AudioStateChanged;

            Grammar grammar = create();
            pSRE.LoadGrammar(grammar);
            pSRE.RecognizeAsync(RecognizeMode.Multiple);
            System.Diagnostics.Trace.WriteLine("Grammars has been loaded.");
        }

        public void stopEngine()
        {
            pSRE.RecognizeAsyncStop();
        }

        private void PSRE_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result == null) return;
            if (e.Result.Semantics != null && e.Result.Semantics.Count != 0)
            {
                // Tutaj silnik rozpoznawania mowy, jeżeli rozpozna mowę to
                // wysyła EVENT jako publisher do wszystkich zainteresowanych subscriberów
                HomeActions(this, e);
            }

        }

        private void SRE_AudioStateChanged(object sender, AudioStateChangedEventArgs e)
        {
            if (e.AudioState == AudioState.Speech)
            {
                recognitionIndicator.Fill = Brushes.Green;
                System.Diagnostics.Trace.WriteLine(e.AudioState.ToString());
            }
            else if (e.AudioState == AudioState.Silence)
            {
                // when no voice detected
                recognitionIndicator.Fill = Brushes.Red;
            }
            else
            {
                // not handled state.
            }
        }

        private Grammar create()
        {
            SrgsDocument srgs = new SrgsDocument(@"../../../../grammar.grxml");
            return new Grammar(srgs, "SH");
        }

        public static string GetValue(SemanticValue Semantics, string keyName)
        {
            string result = "";
            if (Semantics.ContainsKey(keyName))
                result = Semantics[keyName].Value.ToString();
            return result;
        }

        public static string GetConfidence(SemanticValue Semantics, string keyName)
        {
            string result = "";
            if (Semantics.ContainsKey(keyName))
                result = Semantics[keyName].Confidence.ToString("0.0000");
            return result;
        }

    }
}

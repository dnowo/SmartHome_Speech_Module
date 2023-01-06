using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using Microsoft.Speech.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SHSM
{
    internal class SpeechEngine
    {
        private SpeechSynthesizer pTTS;
        private SpeechRecognitionEngine pSRE;
        private Ellipse recognitionIndicator;

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

            System.Diagnostics.Trace.WriteLine(GetValue(e.Result.Semantics, "TurnOnLightInRoom"));
            System.Diagnostics.Trace.WriteLine(GetConfidence(e.Result.Semantics, "TurnOnLightInRoom"));
            System.Diagnostics.Trace.WriteLine("----------");

            if (e.Result.Semantics != null && e.Result.Semantics.Count != 0)
            {
                // Examples: 
                //handleSpeech(RuchInput, RuchConfidence, e, "Aktywnosc");
            }

        }

        private void handleSpeech(TextBox obj, TextBlock confidence, SpeechRecognizedEventArgs e, string keyname)
        {
            // example speech handling.
            string result = GetValue(e.Result.Semantics, keyname);
            if (result.Length > 0)
            {
                string c = GetConfidence(e.Result.Semantics, keyname);
                confidence.Text = c;
                if ((float) Convert.ToDouble(c) > 0.9)
                {
                    obj.Text = GetValue(e.Result.Semantics, keyname).Substring(keyname.Length);
                }
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

        private string GetValue(SemanticValue Semantics, string keyName)
        {
            string result = "";
            if (Semantics.ContainsKey(keyName))
                result = Semantics[keyName].Value.ToString();
            return result;
        }

        private string GetConfidence(SemanticValue Semantics, string keyName)
        {
            string result = "";
            if (Semantics.ContainsKey(keyName))
                result = Semantics[keyName].Confidence.ToString("0.0000");
            return result;
        }

    }
}

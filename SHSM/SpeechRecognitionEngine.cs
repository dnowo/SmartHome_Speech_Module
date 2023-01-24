using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using System.Collections.Generic;
using System.Diagnostics;

namespace SHSM
{
    public class SpeechRecognitionEngine
    {
        private const double CONFIDENCE_LEVEL = 0.7;
        
        private Microsoft.Speech.Recognition.SpeechRecognitionEngine SRE;

        public delegate void SpeechRecognizedEventHandler(object? sender, SpeechRecognizedEventArgs e);
        public event SpeechRecognizedEventHandler? SpeechRecognized;
        public delegate void AudioStateChangedEventHandler(object? sender, AudioStateChangedEventArgs e);
        public event AudioStateChangedEventHandler? AudioStateChanged;

        private readonly Dictionary<string, Grammar> grammars = new();

        public SpeechRecognitionEngine()
        {
            SRE = new Microsoft.Speech.Recognition.SpeechRecognitionEngine();
            SRE.SetInputToDefaultAudioDevice();
            SRE.SpeechRecognized += SRE_SpeechRecognized;
            SRE.AudioStateChanged += SRE_AudioStateChanged;

            LoadGrammars();

            SRE.RecognizeAsync(RecognizeMode.Multiple);
            Trace.WriteLine("Grammars have been loaded.");
        }

        public void StopEngine()
        {
            SRE.RecognizeAsyncStop();
        }

        private void SRE_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            Debug.WriteLine($"{e.Result.Text} | Confidence: {e.Result.Confidence}");

            if (e.Result.Confidence < CONFIDENCE_LEVEL)
            {
                return;
            }
            
            SpeechRecognized?.Invoke(sender, e);
        }

        private void SRE_AudioStateChanged(object? sender, AudioStateChangedEventArgs e)
        {
            AudioStateChanged?.Invoke(sender, e);
        }

        private void LoadGrammars()
        {
            SrgsDocument srgs = new SrgsDocument(@"../../../../grammar.grxml");
            grammars.Add("SH", new Grammar(srgs, "SH"));
            grammars.Add("place", new Grammar(srgs, "place"));
            grammars.Add("number", new Grammar(srgs, "number"));
            ToggleGrammar("place", false);
            ToggleGrammar("number", false);
            foreach (var (name, grammar) in grammars)
            {
                SRE.LoadGrammarAsync(grammar);
            }
        }

        public static string GetValue(SemanticValue semantics, string keyName)
        {
            string result = "";
            if (semantics.ContainsKey(keyName))
                result = semantics[keyName].Value.ToString() ?? string.Empty;
            return result;
        }

        public bool IsGrammarEnable(string grammarName)
        {
            return grammars[grammarName].Enabled;
        }

        public void ToggleGrammar(string grammarName, bool? on = null)
        {
            Grammar grammar = grammars[grammarName];
            grammar.Enabled = on ?? !grammar.Enabled;
        }

        public void DisableAllGrammars()
        {
            foreach (var (name, grammar) in grammars)
            {
                grammar.Enabled = false;
            }
        }
    }
}

using Microsoft.Speech.Synthesis;

namespace SHSM
{
    public class TextToSpeechEngine
    {
        public SpeechSynthesizer TTS;

        public TextToSpeechEngine()
        {
            TTS = new SpeechSynthesizer();
            TTS.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string message)
        {
            TTS.Speak(message);
        }

        public void SpeakAsync(string message)
        {
            TTS.SpeakAsync(message);
        }
    }
}

using System;
using static System.Console;
using System.Globalization;
using System.Speech;
using System.Speech.Recognition;
using TalkToMe.Events;

namespace TalkToMe
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var recognitionEngine = new SpeechRecognitionEngine(new CultureInfo("en-US")))
            {
                recognitionEngine.LoadGrammar(new DictationGrammar());
                recognitionEngine.SpeechRecognized += SpeechEvents.recognizer_IfRecognizedCorrectWords;
                recognitionEngine.SpeechRecognized += SpeechEvents.recognizer_recognitionDebug;

                recognitionEngine.SetInputToDefaultAudioDevice();

                recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

                while (true)
                {
                    WriteLine("Talk to me.");
                    ReadLine();
                }
            }

        }
    }
}

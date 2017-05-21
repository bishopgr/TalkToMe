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
            using (var recognitionEngine = new SpeechRecognitionEngine())
            {
                var spe = new SpeechEvents();

                recognitionEngine.LoadGrammar(new DictationGrammar());
                recognitionEngine.SpeechRecognized += SpeechEvents.recognizer_IfRecognizedCorrectWords;
                recognitionEngine.SpeechRecognized += spe.recognizer_recognitionDebug;
                recognitionEngine.SpeechRecognized += spe.recognizer_ExitListener;
                recognitionEngine.SpeechRecognized += spe.recognizer_DebugListener;

                recognitionEngine.SetInputToDefaultAudioDevice();

                recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);


                while (SpeechEvents.Continue)
                {

                }
            }

        }

     
    }
}

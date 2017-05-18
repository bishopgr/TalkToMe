using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using static System.Console;
using System.Speech.Recognition;

namespace TalkToMe.Events
{
    public class SpeechEvents : EventArgs
    {

        public bool Continue { get; set; } = true;
        public bool Recording { get; set; }
        private readonly SpeechEvents _speechEvents;

        public SpeechEvents(SpeechEvents speechEvents)
        {
            _speechEvents = speechEvents;
        }

        public SpeechEvents()
        {
            
        }

        public void recognizer_recognitionDebug(object sender, SpeechRecognizedEventArgs spe)
        {
            const string logPath = @"C:\SpeechRecognition\AudioSamples\Logs\log.txt";
            const string path = @"C:\SpeechRecognition\AudioSamples\dbugaudio.wav";


            var highestConfidenceResult = spe.Result.Alternates.Where(a => a.Confidence > 0.90).Select(a => a.Text).FirstOrDefault();

                WriteLine(DateTime.Now.ToLongDateString());
                WriteLine("**** DEBUG ****");
                WriteLine("Confidence level: " + spe.Result.Confidence);

                WriteLine("Highest confidence: " + highestConfidenceResult);
                WriteLine("Actual: " + spe.Result.Text);

            if (spe.Result.Text == "record")
            {
                
                Recording = true;
            }

            if (spe.Result.Text == "exit" || spe.Result.Text == "Exit")
            {
                Continue = false;
            }

            if (!Recording) return;
            WriteLine("Dumping audio...");
            var nameAudio = spe.Result.Audio;

            using (Stream outputStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                nameAudio?.WriteToWaveStream(outputStream);
                outputStream.Close();
            }
        }

        public static void recognizer_IfRecognizedCorrectWords(object sender, SpeechRecognizedEventArgs spe)
        {
            switch (spe.Result.Text)
            {
                case "run":
                    WriteLine("Run Forrest run!");
                    break;
                case "open garage":
                    WriteLine("Garage door OPENED");
                    break;
                case "close garage":
                    Write("Garage door CLOSED");
                    break;
                default:
                    break;
            }
        }

        public bool EvaluateClosed()
        {
            return Continue;
        }
        
    }
}
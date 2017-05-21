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

        public static bool Continue { get; set; } = true;
        public bool Recording { get; set; }
        private bool Debugging { get; set; }

        public void recognizer_recognitionDebug(object sender, SpeechRecognizedEventArgs spe)
        {
            const string logPath = @"C:\SpeechRecognition\AudioSamples\Logs\log.txt";
            const string path = @"C:\SpeechRecognition\AudioSamples\dbugaudio.wav";

            while (Debugging)
            {
                Console.WriteLine("Begin debug...");
                var highestConfidenceResult = (spe.Result.Alternates.FirstOrDefault(a => a.Confidence > 0.8) == null) ? "No alternatives > 80%" : spe.Result.Alternates.Where(a => a.Confidence > 0.80).Select(a => a.Text).FirstOrDefault()  ;


                WriteLine("Confidence level: " + spe.Result.Confidence);

                WriteLine("Highest confidence: " + highestConfidenceResult);
                if (spe.Result.Confidence > 0.8)
                {
                    WriteLine("Best guess: " + spe.Result.Text);
                }


                if (spe.Result.Text == "record")
                {

                    Recording = true;
                }

                if (spe.Result.Text == "Stop" || spe.Result.Text == "stop")
                {
                    Console.WriteLine("Ending debug...");
                    Debugging = false;
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
        }

        public  void recognizer_ExitListener(object sender, SpeechRecognizedEventArgs spe)
        {
            if (spe.Result.Text == "exit" || spe.Result.Text == "Exit")
            {
                Continue = false;
            }
        }

        public  void recognizer_DebugListener(object sender, SpeechRecognizedEventArgs spe)
        {

            if (spe.Result.Text == "Debug" || spe.Result.Text == "debug")
            {
                Debugging = true;
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
        
    }
}
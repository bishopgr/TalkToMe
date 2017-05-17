using System;
using System.IO;
using static System.Console;
using System.Speech.Recognition;

namespace TalkToMe.Events
{
    public static class SpeechEvents
    {

        public static void recognizer_recognitionDebug(object sender, SpeechRecognizedEventArgs spe)
        {
            const string logPath = @"C:\SpeechRecognition\AudioSamples\Logs\log.txt";
            const string path = @"C:\SpeechRecognition\AudioSamples\dbugaudio.wav";

            using (var streamWriter = new StreamWriter(logPath,true))
            {
                WriteLine(DateTime.Now.ToLongDateString());
                WriteLine("**** DEBUG ****");
                WriteLine("Confidence level: " + spe.Result.Confidence);
                WriteLine("Possible alternatives: " + spe.Result.Alternates);

                foreach (var alternative in spe.Result.Alternates)
                {
                    WriteLine("Alternative: " + alternative);
                }
                WriteLine("Actual: " + spe.Result.Text);

                foreach (var word in spe.Result.Words)
                {
                    WriteLine("Words so far: " + word.Text);
                    WriteLine("Confidence level: " + word.Confidence);
                    WriteLine("Display Attributes: " + word.DisplayAttributes);
                    WriteLine("Pronunciation: " + word.Pronunciation);
                }

                WriteLine("Dumping audio...");
                streamWriter.Close();
            }
            
            var nameAudio = spe.Result.Audio;

            using (Stream outputStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                nameAudio?.WriteToWaveStream(outputStream);
                outputStream.Close();
            }
        }

        public static void recognizer_recognizedSpeech(object sender, SpeechRecognizedEventArgs spe)
        {
            WriteLine("Recognized " + spe.Result.Text);
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
using System;
using System.Linq;
using System.Collections.Generic;
using System.Speech.Recognition;
using WindowsInput;
using WindowsInput.Native;
using System.Speech.Synthesis;

namespace SpeechTest
{
    class Program
    {
        static InputSimulator sim = new InputSimulator();
        static SpeechSynthesizer sr = new SpeechSynthesizer();

        static string next = "nadya 9";
        static string stop = "nadya 2";
        static string back = "nadya 7";
        static string nextViol = "violetta 9";
        static string stopViol = "violetta 2";
        static string backViol = "violetta 7";
        static string nextAngel = "angelica 9";
        static string stopAngel = "angelica 2";
        static string backAngel = "angelica 7";

        static Dictionary<string, VirtualKeyCode> map = new Dictionary<string, VirtualKeyCode>()
        {
            {next, VirtualKeyCode.MEDIA_NEXT_TRACK},
            {stop, VirtualKeyCode.MEDIA_PLAY_PAUSE},
            {back, VirtualKeyCode.MEDIA_PREV_TRACK},
            {nextViol, VirtualKeyCode.MEDIA_NEXT_TRACK},
            {stopViol, VirtualKeyCode.MEDIA_PLAY_PAUSE},
            {backViol, VirtualKeyCode.MEDIA_PREV_TRACK},
            {nextAngel, VirtualKeyCode.MEDIA_NEXT_TRACK},
            {stopAngel, VirtualKeyCode.MEDIA_PLAY_PAUSE},
            {backAngel, VirtualKeyCode.MEDIA_PREV_TRACK},
        };

        static void Main(string[] args)
        {
            // var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            var culture = new System.Globalization.CultureInfo("en-US");
            using (var rec = new SpeechRecognitionEngine(culture))
            {
                var builder = new GrammarBuilder(new Choices(map.Keys.Select(s => s).ToArray()))
                {
                    Culture = culture
                };

                rec.LoadGrammar(new Grammar(builder));
                rec.SpeechRecognized += Rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        private static void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //sr.SpeakAsync(e.Result.Text);
            Console.WriteLine(e.Result.Text);
            VirtualKeyCode key;
            if (map.TryGetValue(e.Result.Text, out key))
            {
                sim.Keyboard.KeyPress(key);
            }
        }
    }
}

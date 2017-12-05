using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace POCTTS
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = 0;     // -10...10

            synthesizer.SelectVoice("Microsoft Maria Desktop");

            // Synchronous
            synthesizer.Speak("Olá mundo");
            

            // Asynchronous
            //synthesizer.SpeakAsync("Hello World");
        }
    }
}

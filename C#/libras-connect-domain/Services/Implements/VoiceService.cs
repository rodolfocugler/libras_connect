using libras_connect_domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IControlService<see cref="IVoiceService"/>
    /// </summary>
    public class VoiceService : IVoiceService
    {
        private readonly SpeechSynthesizer _synthesizer;

        public VoiceService()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.Volume = 100;
            _synthesizer.Rate = 0;
            _synthesizer.SelectVoice("Microsoft Maria Desktop");
        }

        /// <summary>
        ///     <para><see cref="IVoiceService.Speak(string)"/></para>
        /// </summary>
        public void Speak(string text)
        {
            _synthesizer.Speak(text);
        }
    }
}

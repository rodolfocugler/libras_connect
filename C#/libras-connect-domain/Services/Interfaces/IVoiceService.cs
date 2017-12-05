using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Interfaces
{
    /// <summary>
    /// Interface of VoiceService
    /// </summary>
    public interface IVoiceService
    {
        /// <summary>
        /// Speak some text
        /// </summary>
        /// <param name="text">Text to PC speak</param>
        void Speak(string text);
    }
}

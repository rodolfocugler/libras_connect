using libras_connect_domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Interfaces
{
    /// <summary>
    /// Interface of CntkService
    /// </summary>
    public interface ICntkService
    {
        /// <summary>
        /// Compute output foreach input
        /// </summary>
        /// <param name="signal">Signal model to use as a input</param>
        /// <returns>the label founded</returns>
        string Compute(Signal signal);
    }
}

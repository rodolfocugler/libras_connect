using libras_connect_client.Views;
using libras_connect_domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_client.Services.Interfaces
{
    /// <summary>
    /// Interface of ControlService
    /// </summary>
    public interface IControlService
    {
        /// <summary>
        /// Set HomePage
        /// </summary>
        /// <param name="homePage">HomePage</param>
        void SetHomePage(IHomePage homePage);

        /// <summary>
        /// Set ControlTypeEnum
        /// </summary>
        /// <param name="controlTypeEnum">ControlTypeEnum</param>
        void SetControlType(ControlTypeEnum controlTypeEnum);

        /// <summary>
        /// Set ControlTypeEnum and word of signal
        /// </summary>
        /// <param name="controlTypeEnum">ControlTypeEnum</param>
        /// /// <param name="word">Word</param>
        void SetControlType(ControlTypeEnum controlTypeEnum, string word);

        /// <summary>
        /// Get ControlTypeEnum
        /// </summary>
        /// <returns>ControlTypeEnum</returns>
        ControlTypeEnum GetControlType();
    }
}

using libras_connect_domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Services.Interfaces
{
    /// <summary>
    /// Interface of SettingService
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// Create a new Setting model
        /// </summary>
        /// <param name="setting">Setting model</param>
        void Create(Setting setting);

        /// <summary>
        /// Update Setting model
        /// </summary>
        /// <param name="setting">Setting model</param>
        void Update(Setting setting);

        /// <summary>
        /// Get Setting model collection
        /// </summary>
        /// <returns>Setting model collection</returns>
        ICollection<Setting> Get();

        /// <summary>
        /// Delete All Settings
        /// </summary>
        void Delete();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using libras_connect_domain.Services.Interfaces;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Exceptions;
using libras_connect_domain.Models;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of ISettingService<see cref="ISettingService"/>
    /// </summary>
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        /// <summary>
        ///     <para><see cref="ISettingService.Create(Setting)"/></para>
        /// </summary>
        public void Create(Setting setting)
        {
            this.ValidateSetting(setting);

            try
            {
                _settingRepository.Create(setting);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        /// <summary>
        ///     <para><see cref="ISettingService.Delete"/></para>
        /// </summary>
        public void Delete()
        {
            try
            {
                _settingRepository.Delete();
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        /// <summary>
        ///     <para><see cref="ISettingService.Get"/></para>
        /// </summary>
        public ICollection<Setting> Get()
        {
            ICollection<Setting> list = null;

            try
            {
                list = _settingRepository.Get();
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }

            return list;
        }

        /// <summary>
        ///     <para><see cref="ISettingService.Update(Setting)"/></para>
        /// </summary>
        public void Update(Setting setting)
        {
            this.ValidateSetting(setting);

            try
            {
                _settingRepository.Update(setting);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validate Setting before save or update
        /// </summary>
        /// <param name="setting">Setting model</param>
        private void ValidateSetting(Setting setting)
        {
            if (setting == null)
            {
                throw new ValidationException("O objeto não pode ser nulo");
            }

            //ip
            if (setting.IP == null || !Regex.Match(setting.IP, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}").Success)
            {
                throw new ValidationException("O IP deve ser preenchido no formato xxx.xxx.xxx.xxx");
            }

            //port
            if (setting.Port < 15000)
            {
                throw new ValidationException("A porta deve ser maior que 15000");
            }
        }
    }
}

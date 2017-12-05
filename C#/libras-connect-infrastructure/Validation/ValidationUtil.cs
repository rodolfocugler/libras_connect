using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace libras_connect_infrastructure.Validation
{
    public static class ValidationUtil
    {
        /// <summary>
        /// Valid an ip and a port address
        /// </summary>
        /// <param name="address">ip and port as string</param>
        public static void ValidIPAddress(string address)
        {
            if (!Regex.Match(address, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,5}").Success)
            {
                throw new ArgumentException("O formato do endereço deve ser xxx.xxx.xxx.xxx:xxxxx");
            }
        }
    }
}

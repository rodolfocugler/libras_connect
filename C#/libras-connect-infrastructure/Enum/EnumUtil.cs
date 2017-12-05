using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_infrastructure.Enum
{
    public static class EnumUtil
    {
        /// <summary>
        /// Get Description from Enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumerationValue">Enum Item</param>
        /// <returns>Description</returns>
        public static string GetDescription<T>(T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();

            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Must be of Enum type"));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());

            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
    }
}

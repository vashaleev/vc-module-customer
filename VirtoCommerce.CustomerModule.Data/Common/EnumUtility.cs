using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.CustomerModule.Data.Common
{
    /// <summary>
    /// TODO: The temporary copy of EnumUtility extension methods present in VirtoCommerce.Platform.Core 2.13.39
    /// Need to remove this implementation after update  VirtoCommerce.Platform.Core to 2.13.39 or higher version
    /// </summary>
    public static class EnumUtility
    {
        public static T SafeParseFlags<T>(string value, T defaultValue)
           where T : struct
        {
            return SafeParseFlags<T>(value, defaultValue, ",");
        }

        public static T SafeParseFlags<T>(string value, T defaultValue, string separator)
            where T : struct
        {
            if (!typeof(T).IsDefined(typeof(FlagsAttribute), false))
            {
                throw new ArgumentException($"{typeof(T).FullName} type should have [Flags] attribute.");
            }

            var result = 0;
            var wasAssigned = false;

            if (!string.IsNullOrEmpty(value))
            {
                var parts = value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                {
                    if (Enum.TryParse(part.Trim(), true, out T partResult))
                    {
                        result |= Convert.ToInt32(partResult);
                        wasAssigned = true;
                    }
                }
            }

            return wasAssigned ? (T)Enum.ToObject(typeof(T), result) : defaultValue;
        }
    }
}

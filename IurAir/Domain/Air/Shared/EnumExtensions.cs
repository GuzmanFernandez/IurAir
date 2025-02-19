using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Shared
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            var memberInfo = typeof(TEnum).GetField(enumValue.ToString());
            var enumMemberAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(EnumMemberAttribute));

            if (enumMemberAttribute != null)
            {
                return enumMemberAttribute.Value;
            }

            return enumValue.ToString();
        }

        public static TEnum ParseEnumValue<TEnum>(this string value) where TEnum : Enum
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                MemberInfo memberInfo = typeof(TEnum).GetField(enumValue.ToString());
                EnumMemberAttribute enumMemberAttribute = memberInfo.GetCustomAttribute<EnumMemberAttribute>();

                if (enumMemberAttribute != null && enumMemberAttribute.Value == value)
                {
                    return enumValue;
                }
            }

            return default;
        }
    }
}

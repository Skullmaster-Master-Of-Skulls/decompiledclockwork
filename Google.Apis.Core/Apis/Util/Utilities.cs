using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Google.Apis.Testing;

namespace Google.Apis.Util
{
	// Token: 0x0200000F RID: 15
	public static class Utilities
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002447 File Offset: 0x00000647
		[VisibleForTestOnly]
		public static string GetLibraryVersion()
		{
			return Regex.Match(typeof(Utilities).GetTypeInfo().Assembly.FullName, "Version=([\\d\\.]+)").Groups[1].ToString();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000247C File Offset: 0x0000067C
		public static T ThrowIfNull<T>(this T obj, string paramName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(paramName);
			}
			return obj;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000248E File Offset: 0x0000068E
		public static string ThrowIfNullOrEmpty(this string str, string paramName)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentException("Parameter was empty", paramName);
			}
			return str;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024A5 File Offset: 0x000006A5
		internal static bool IsNullOrEmpty<T>(this IEnumerable<T> coll)
		{
			return coll == null || coll.Count<T>() == 0;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024B8 File Offset: 0x000006B8
		public static T GetCustomAttribute<T>(this MemberInfo info) where T : Attribute
		{
			object[] array = info.GetCustomAttributes(typeof(T), false).ToArray<object>();
			if (array.Length != 0)
			{
				return (T)((object)array[0]);
			}
			return default(T);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024F4 File Offset: 0x000006F4
		internal static string GetStringValue(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			field.ThrowIfNull("value");
			StringValueAttribute customAttribute = field.GetCustomAttribute<StringValueAttribute>();
			if (customAttribute != null)
			{
				return customAttribute.Text;
			}
			throw new ArgumentException(string.Format("Enum value '{0}' does not contain a StringValue attribute", field), "value");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002545 File Offset: 0x00000745
		public static string GetEnumStringValue(Enum value)
		{
			return value.GetStringValue();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002550 File Offset: 0x00000750
		[VisibleForTestOnly]
		public static string ConvertToString(object o)
		{
			if (o == null)
			{
				return null;
			}
			if (o.GetType().GetTypeInfo().IsEnum)
			{
				StringValueAttribute customAttribute = o.GetType().GetField(o.ToString()).GetCustomAttribute<StringValueAttribute>();
				if (customAttribute == null)
				{
					return o.ToString();
				}
				return customAttribute.Text;
			}
			else
			{
				if (o is DateTime)
				{
					return Utilities.ConvertToRFC3339((DateTime)o);
				}
				if (o is bool)
				{
					return o.ToString().ToLowerInvariant();
				}
				return o.ToString();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025CA File Offset: 0x000007CA
		internal static string ConvertToRFC3339(DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
			{
				date = date.ToUniversalTime();
			}
			return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025F0 File Offset: 0x000007F0
		public static DateTime? GetDateTimeFromString(string raw)
		{
			DateTime value;
			if (!DateTime.TryParse(raw, out value))
			{
				return null;
			}
			return new DateTime?(value);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002617 File Offset: 0x00000817
		public static string GetStringFromDateTime(DateTime? date)
		{
			if (date == null)
			{
				return null;
			}
			return Utilities.ConvertToRFC3339(date.Value);
		}
	}
}

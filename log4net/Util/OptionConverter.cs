using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using log4net.Core;
using log4net.Util.TypeConverters;

namespace log4net.Util
{
	// Token: 0x0200010C RID: 268
	public sealed class OptionConverter
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x00017DC5 File Offset: 0x00015FC5
		private OptionConverter()
		{
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00017DD0 File Offset: 0x00015FD0
		public static bool ToBoolean(string argValue, bool defaultValue)
		{
			if (argValue != null && argValue.Length > 0)
			{
				try
				{
					return bool.Parse(argValue);
				}
				catch (Exception exception)
				{
					LogLog.Error(OptionConverter.declaringType, "[" + argValue + "] is not in proper bool form.", exception);
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00017E24 File Offset: 0x00016024
		public static long ToFileSize(string argValue, long defaultValue)
		{
			if (argValue == null)
			{
				return defaultValue;
			}
			string text = argValue.Trim().ToUpper(CultureInfo.InvariantCulture);
			long num = 1L;
			int length;
			if ((length = text.IndexOf("KB")) != -1)
			{
				num = 1024L;
				text = text.Substring(0, length);
			}
			else if ((length = text.IndexOf("MB")) != -1)
			{
				num = 1048576L;
				text = text.Substring(0, length);
			}
			else if ((length = text.IndexOf("GB")) != -1)
			{
				num = 1073741824L;
				text = text.Substring(0, length);
			}
			if (text != null)
			{
				text = text.Trim();
				long num2;
				if (SystemInfo.TryParse(text, out num2))
				{
					return num2 * num;
				}
				LogLog.Error(OptionConverter.declaringType, "OptionConverter: [" + text + "] is not in the correct file size syntax.");
			}
			return defaultValue;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00017EE4 File Offset: 0x000160E4
		public static object ConvertStringTo(Type target, string txt)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (typeof(string) == target || typeof(object) == target)
			{
				return txt;
			}
			IConvertFrom convertFrom = ConverterRegistry.GetConvertFrom(target);
			if (convertFrom != null && convertFrom.CanConvertFrom(typeof(string)))
			{
				return convertFrom.ConvertFrom(txt);
			}
			if (target.IsEnum)
			{
				return OptionConverter.ParseEnum(target, txt, true);
			}
			MethodInfo method = target.GetMethod("Parse", new Type[]
			{
				typeof(string)
			});
			if (method != null)
			{
				return method.Invoke(null, BindingFlags.InvokeMethod, null, new object[]
				{
					txt
				}, CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00017FAC File Offset: 0x000161AC
		public static bool CanConvertTypeTo(Type sourceType, Type targetType)
		{
			if (sourceType == null || targetType == null)
			{
				return false;
			}
			if (targetType.IsAssignableFrom(sourceType))
			{
				return true;
			}
			IConvertTo convertTo = ConverterRegistry.GetConvertTo(sourceType, targetType);
			if (convertTo != null && convertTo.CanConvertTo(targetType))
			{
				return true;
			}
			IConvertFrom convertFrom = ConverterRegistry.GetConvertFrom(targetType);
			return convertFrom != null && convertFrom.CanConvertFrom(sourceType);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00018004 File Offset: 0x00016204
		public static object ConvertTypeTo(object sourceInstance, Type targetType)
		{
			Type type = sourceInstance.GetType();
			if (targetType.IsAssignableFrom(type))
			{
				return sourceInstance;
			}
			IConvertTo convertTo = ConverterRegistry.GetConvertTo(type, targetType);
			if (convertTo != null && convertTo.CanConvertTo(targetType))
			{
				return convertTo.ConvertTo(sourceInstance, targetType);
			}
			IConvertFrom convertFrom = ConverterRegistry.GetConvertFrom(targetType);
			if (convertFrom != null && convertFrom.CanConvertFrom(type))
			{
				return convertFrom.ConvertFrom(sourceInstance);
			}
			throw new ArgumentException(string.Concat(new string[]
			{
				"Cannot convert source object [",
				sourceInstance.ToString(),
				"] to target type [",
				targetType.Name,
				"]"
			}), "sourceInstance");
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001809C File Offset: 0x0001629C
		public static object InstantiateByClassName(string className, Type superClass, object defaultValue)
		{
			if (className != null)
			{
				try
				{
					Type typeFromString = SystemInfo.GetTypeFromString(className, true, true);
					if (!superClass.IsAssignableFrom(typeFromString))
					{
						LogLog.Error(OptionConverter.declaringType, string.Concat(new string[]
						{
							"OptionConverter: A [",
							className,
							"] object is not assignable to a [",
							superClass.FullName,
							"] variable."
						}));
						return defaultValue;
					}
					return Activator.CreateInstance(typeFromString);
				}
				catch (Exception exception)
				{
					LogLog.Error(OptionConverter.declaringType, "Could not instantiate class [" + className + "].", exception);
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00018138 File Offset: 0x00016338
		public static string SubstituteVariables(string value, IDictionary props)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2;
			for (;;)
			{
				num2 = value.IndexOf("${", num);
				if (num2 == -1)
				{
					break;
				}
				stringBuilder.Append(value.Substring(num, num2 - num));
				int num3 = value.IndexOf('}', num2);
				if (num3 == -1)
				{
					goto Block_3;
				}
				num2 += 2;
				string key = value.Substring(num2, num3 - num2);
				string text = props[key] as string;
				if (text != null)
				{
					stringBuilder.Append(text);
				}
				num = num3 + 1;
			}
			if (num == 0)
			{
				return value;
			}
			stringBuilder.Append(value.Substring(num, value.Length - num));
			return stringBuilder.ToString();
			Block_3:
			throw new LogException(string.Concat(new object[]
			{
				"[",
				value,
				"] has no closing brace. Opening brace at position [",
				num2,
				"]"
			}));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00018212 File Offset: 0x00016412
		private static object ParseEnum(Type enumType, string value, bool ignoreCase)
		{
			return Enum.Parse(enumType, value, ignoreCase);
		}

		// Token: 0x040002DD RID: 733
		private const string DELIM_START = "${";

		// Token: 0x040002DE RID: 734
		private const char DELIM_STOP = '}';

		// Token: 0x040002DF RID: 735
		private const int DELIM_START_LEN = 2;

		// Token: 0x040002E0 RID: 736
		private const int DELIM_STOP_LEN = 1;

		// Token: 0x040002E1 RID: 737
		private static readonly Type declaringType = typeof(OptionConverter);
	}
}

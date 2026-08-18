using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace System.Web.Helpers
{
	// Token: 0x0200000A RID: 10
	internal static class ConversionUtil
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000030F8 File Offset: 0x000012F8
		internal static string ToString<T>(T obj)
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle.IsEnum)
			{
				return obj.ToString();
			}
			TypeConverter converter = TypeDescriptor.GetConverter(typeFromHandle);
			if (converter != null && converter.CanConvertTo(typeof(string)))
			{
				return converter.ConvertToInvariantString(obj);
			}
			return null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003150 File Offset: 0x00001350
		internal static bool TryFromString(Type type, string value, out object result)
		{
			result = null;
			if (type == typeof(string))
			{
				result = value;
				return true;
			}
			if (type.IsEnum)
			{
				return ConversionUtil.TryFromStringToEnumHelper(type, value, out result);
			}
			if (type == typeof(Color))
			{
				Color color;
				bool result2 = ConversionUtil.TryFromStringToColor(value, out color);
				result = color;
				return result2;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertFrom(typeof(string)))
			{
				try
				{
					result = converter.ConvertFromInvariantString(value);
					return true;
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031EC File Offset: 0x000013EC
		internal static bool TryFromStringToEnum<T>(string value, out T result) where T : struct
		{
			return Enum.TryParse<T>(value, true, out result);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031F8 File Offset: 0x000013F8
		private static bool TryFromStringToEnumHelper(Type enumType, string value, out object result)
		{
			result = null;
			if (ConversionUtil._stringToEnumMethod == null)
			{
				ConversionUtil._stringToEnumMethod = typeof(ConversionUtil).GetMethod("TryFromStringToEnum", BindingFlags.Static | BindingFlags.NonPublic);
			}
			object[] array = new object[2];
			array[0] = value;
			object[] array2 = array;
			bool result2 = (bool)ConversionUtil._stringToEnumMethod.MakeGenericMethod(new Type[]
			{
				enumType
			}).Invoke(null, array2);
			result = array2[1];
			return result2;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003268 File Offset: 0x00001468
		internal static bool TryFromStringToFontFamily(string fontFamily, out FontFamily result)
		{
			result = null;
			bool result2 = false;
			foreach (FontFamily fontFamily2 in FontFamily.Families)
			{
				if (fontFamily.Equals(fontFamily2.Name, StringComparison.OrdinalIgnoreCase))
				{
					result = fontFamily2;
					result2 = true;
					break;
				}
			}
			return result2;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032AC File Offset: 0x000014AC
		internal static bool TryFromStringToColor(string value, out Color result)
		{
			result = default(Color);
			if (value.StartsWith("#", StringComparison.OrdinalIgnoreCase))
			{
				if (value.Length != 7 && value.Length != 4)
				{
					return false;
				}
				if (value.Length == 4)
				{
					char[] array = new char[7];
					array[0] = '#';
					array[1] = (array[2] = value[1]);
					array[3] = (array[4] = value[2]);
					array[5] = (array[6] = value[3]);
					value = new string(array);
				}
			}
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
			try
			{
				result = (Color)converter.ConvertFromInvariantString(value);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003370 File Offset: 0x00001570
		internal static string NormalizeImageFormat(string value)
		{
			value = value.ToLowerInvariant();
			string key;
			switch (key = value)
			{
			case "jpeg":
			case "jpg":
			case "pjpeg":
				return "jpeg";
			case "png":
			case "x-png":
				return "png";
			case "icon":
			case "ico":
			case "x-icon":
				return "icon";
			}
			return value;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003450 File Offset: 0x00001650
		internal static bool TryFromStringToImageFormat(string value, out ImageFormat result)
		{
			result = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			if (value.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
			{
				value = value.Substring("image/".Length);
			}
			value = ConversionUtil.NormalizeImageFormat(value);
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(ImageFormat));
			try
			{
				result = (ImageFormat)converter.ConvertFromInvariantString(value);
			}
			catch (NotSupportedException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400002A RID: 42
		private static MethodInfo _stringToEnumMethod;
	}
}

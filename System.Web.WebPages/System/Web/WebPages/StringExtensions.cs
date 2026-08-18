using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.WebPages
{
	// Token: 0x02000091 RID: 145
	public static class StringExtensions
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0000E13E File Offset: 0x0000C33E
		public static bool IsEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E146 File Offset: 0x0000C346
		public static int AsInt(this string value)
		{
			return value.AsInt(0);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E150 File Offset: 0x0000C350
		public static int AsInt(this string value, int defaultValue)
		{
			int result;
			if (!int.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000E16A File Offset: 0x0000C36A
		public static decimal AsDecimal(this string value)
		{
			return value.As<decimal>();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E172 File Offset: 0x0000C372
		public static decimal AsDecimal(this string value, decimal defaultValue)
		{
			return value.As(defaultValue);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000E17B File Offset: 0x0000C37B
		public static float AsFloat(this string value)
		{
			return value.AsFloat(0f);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000E188 File Offset: 0x0000C388
		public static float AsFloat(this string value, float defaultValue)
		{
			float result;
			if (!float.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public static DateTime AsDateTime(this string value)
		{
			return value.AsDateTime(default(DateTime));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public static DateTime AsDateTime(this string value, DateTime defaultValue)
		{
			DateTime result;
			if (!DateTime.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		public static TValue As<TValue>(this string value)
		{
			return value.As(default(TValue));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static bool AsBool(this string value)
		{
			return value.AsBool(false);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E204 File Offset: 0x0000C404
		public static bool AsBool(this string value, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E220 File Offset: 0x0000C420
		public static TValue As<TValue>(this string value, TValue defaultValue)
		{
			try
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
				if (converter.CanConvertFrom(typeof(string)))
				{
					return (TValue)((object)converter.ConvertFrom(value));
				}
				converter = TypeDescriptor.GetConverter(typeof(string));
				if (converter.CanConvertTo(typeof(TValue)))
				{
					return (TValue)((object)converter.ConvertTo(value, typeof(TValue)));
				}
			}
			catch
			{
			}
			return defaultValue;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public static bool IsBool(this string value)
		{
			bool flag;
			return bool.TryParse(value, out flag);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		public static bool IsInt(this string value)
		{
			int num;
			return int.TryParse(value, out num);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
		public static bool IsDecimal(this string value)
		{
			return value.Is<decimal>();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public static bool IsFloat(this string value)
		{
			float num;
			return float.TryParse(value, out num);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E304 File Offset: 0x0000C504
		public static bool IsDateTime(this string value)
		{
			DateTime dateTime;
			return DateTime.TryParse(value, out dateTime);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E31C File Offset: 0x0000C51C
		public static bool Is<TValue>(this string value)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
			if (converter != null)
			{
				try
				{
					if (value == null || converter.CanConvertFrom(null, value.GetType()))
					{
						converter.ConvertFrom(null, CultureInfo.CurrentCulture, value);
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}
	}
}

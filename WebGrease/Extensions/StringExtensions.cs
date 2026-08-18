using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WebGrease.Extensions
{
	// Token: 0x020001A7 RID: 423
	internal static class StringExtensions
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x0007E5A6 File Offset: 0x0007C7A6
		public static string AsNullIfWhiteSpace(this string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0007E5B3 File Offset: 0x0007C7B3
		public static string InvariantFormat(this string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0007E5D0 File Offset: 0x0007C7D0
		public static TEnum? TryParseToEnum<TEnum>(this string value, TEnum? defaultValue = null) where TEnum : struct
		{
			TEnum tenum;
			if (!Enum.TryParse<TEnum>(value, true, out tenum) || !Enum.IsDefined(typeof(TEnum), tenum))
			{
				return defaultValue;
			}
			return new TEnum?(tenum);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0007E607 File Offset: 0x0007C807
		internal static bool IsNullOrWhitespace(this string text)
		{
			return string.IsNullOrWhiteSpace(text);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0007E610 File Offset: 0x0007C810
		public static bool TryParseBool(this string textToParse)
		{
			bool flag;
			return !bool.TryParse(textToParse, out flag) || flag;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0007E62C File Offset: 0x0007C82C
		internal static int TryParseInt32(this string textToParse)
		{
			int result;
			if (!int.TryParse(textToParse, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0007E648 File Offset: 0x0007C848
		internal static float? TryParseFloat(this string textToParse)
		{
			float value;
			if (!float.TryParse(textToParse, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
			{
				return null;
			}
			return new float?(value);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0007E681 File Offset: 0x0007C881
		internal static IEnumerable<string> SafeSplitSemiColonSeperatedValue(this string semicolonSeperatedValue)
		{
			if (!string.IsNullOrWhiteSpace(semicolonSeperatedValue))
			{
				return from t in semicolonSeperatedValue.Split(Strings.SemicolonSeparator, StringSplitOptions.RemoveEmptyEntries)
				select t.Trim();
			}
			return new string[0];
		}
	}
}

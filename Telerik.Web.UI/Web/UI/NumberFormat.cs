using System;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x020019FA RID: 6650
	public class NumberFormat
	{
		// Token: 0x06010191 RID: 65937 RVA: 0x0039E5E8 File Offset: 0x0039C7E8
		internal static double? Round(double? number, NumberFormatSettings config)
		{
			if (!config.AllowRounding)
			{
				return number;
			}
			return new double?(Math.Round(number.Value, config.DecimalDigits, MidpointRounding.AwayFromZero));
		}

		// Token: 0x06010192 RID: 65938 RVA: 0x0039E60C File Offset: 0x0039C80C
		internal static string SplitGroups(string str, int groupSize, string groupSeparator)
		{
			for (int i = str.Length - groupSize; i > 0; i -= groupSize)
			{
				str = str.Insert(i, groupSeparator);
			}
			return str;
		}

		// Token: 0x06010193 RID: 65939 RVA: 0x0039E637 File Offset: 0x0039C837
		internal static string Pad(string str, int count, string padChar)
		{
			while (str.ToString(CultureInfo.InvariantCulture).Length < count)
			{
				str = str.ToString(CultureInfo.InvariantCulture) + padChar.ToString(CultureInfo.InvariantCulture);
			}
			return str;
		}

		// Token: 0x06010194 RID: 65940 RVA: 0x0039E66C File Offset: 0x0039C86C
		public static string Format(double? num, NumberFormatSettings config)
		{
			if (num == null)
			{
				return "";
			}
			return InputUtil.FormatDouble(num.Value, config);
		}
	}
}

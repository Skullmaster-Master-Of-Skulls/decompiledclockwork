using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B88 RID: 2952
	internal class HtmlChartHelper
	{
		// Token: 0x06006F7C RID: 28540 RVA: 0x001A09CC File Offset: 0x0019EBCC
		internal static string ColorToHex(Color colorToConvert)
		{
			if (colorToConvert != Color.Transparent)
			{
				byte r = colorToConvert.R;
				byte g = colorToConvert.G;
				byte b = colorToConvert.B;
				return "#" + r.ToString("x2", null) + g.ToString("x2", null) + b.ToString("x2", null);
			}
			return "transparent";
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x001A0A35 File Offset: 0x0019EC35
		internal static string ToSerializableColor(Color color)
		{
			if (color == Color.Empty)
			{
				return string.Empty;
			}
			return HtmlChartHelper.ColorToHex(color);
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x001A0A50 File Offset: 0x0019EC50
		internal static StringBuilder RemoveEndingComma(StringBuilder builder)
		{
			for (int i = builder.Length - 1; i > 0; i--)
			{
				if (builder[i] == ',')
				{
					builder.Remove(i, 1);
					break;
				}
				if (builder[i] != ' ')
				{
					break;
				}
			}
			return builder;
		}

		// Token: 0x06006F7F RID: 28543 RVA: 0x001A0A92 File Offset: 0x0019EC92
		internal static void AddComma(StringBuilder sb)
		{
			if (sb.Length > 0 && sb[sb.Length - 1] != ',')
			{
				sb.Append(",");
			}
		}

		// Token: 0x06006F80 RID: 28544 RVA: 0x001A0ABC File Offset: 0x0019ECBC
		internal static string ToStringInvariant(decimal? value)
		{
			if (value != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					value
				});
			}
			return "null";
		}

		// Token: 0x06006F81 RID: 28545 RVA: 0x001A0AF8 File Offset: 0x0019ECF8
		internal static string ToStringInvariant(double? value)
		{
			if (value != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					value
				});
			}
			return "null";
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x001A0B34 File Offset: 0x0019ED34
		internal static string ToStringInvariant(DateTime? value)
		{
			if (value != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					value
				});
			}
			return "null";
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x001A0B70 File Offset: 0x0019ED70
		internal static string StringToLowerCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			if (value.Length > 1)
			{
				int indexOfFirtNonEmptyCharacter = HtmlChartHelper.GetIndexOfFirtNonEmptyCharacter(value);
				return string.Format("{0}{1}", value.Substring(0, indexOfFirtNonEmptyCharacter + 1).ToLower(), value.Substring(indexOfFirtNonEmptyCharacter + 1));
			}
			return value.ToLower();
		}

		// Token: 0x06006F84 RID: 28548 RVA: 0x001A0BC4 File Offset: 0x0019EDC4
		internal static int GetIndexOfFirtNonEmptyCharacter(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			string text = value.TrimStart(new char[0]);
			if (text.Length <= 0)
			{
				return -1;
			}
			if (text.Length == value.Length)
			{
				return 0;
			}
			return value.Length - text.Length;
		}

		// Token: 0x06006F85 RID: 28549 RVA: 0x001A0C10 File Offset: 0x0019EE10
		internal static string GetSerializedDate(DateTime? date)
		{
			if (date != null)
			{
				DateTime value = date.Value;
				return string.Format("new Date({0}, {1}, {2}, {3}, {4}, {5}, {6})", new object[]
				{
					value.Year,
					value.Month - 1,
					value.Day,
					value.Hour,
					value.Minute,
					value.Second,
					value.Millisecond
				});
			}
			return "null";
		}

		// Token: 0x06006F86 RID: 28550 RVA: 0x001A0CB8 File Offset: 0x0019EEB8
		internal static string GetSerializedValueField(string value, bool parseStringsAsDates = false)
		{
			decimal num;
			bool flag;
			DateTime value2;
			string text = decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num) ? num.ToString(CultureInfo.InvariantCulture) : (bool.TryParse(value, out flag) ? flag.ToString(CultureInfo.InvariantCulture).ToLower() : ((parseStringsAsDates && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out value2)) ? HtmlChartHelper.GetSerializedDate(new DateTime?(value2)) : string.Format("\"{0}\"", value)));
			if (!(text != "\"\""))
			{
				return "null";
			}
			return text;
		}

		// Token: 0x06006F87 RID: 28551 RVA: 0x001A0D48 File Offset: 0x0019EF48
		internal static string GetTemplateWithoutNewLinesAndTabs(string template)
		{
			string templateWithoutNewLines = HtmlChartHelper.GetTemplateWithoutNewLines(template);
			return HtmlChartHelper.GetTemplateWithoutTabs(templateWithoutNewLines);
		}

		// Token: 0x06006F88 RID: 28552 RVA: 0x001A0D64 File Offset: 0x0019EF64
		internal static string GetTemplateWithoutTabs(string template)
		{
			return Regex.Replace(template, "(\\t)+", "");
		}

		// Token: 0x06006F89 RID: 28553 RVA: 0x001A0D84 File Offset: 0x0019EF84
		internal static string GetTemplateWithoutNewLines(string template)
		{
			return Regex.Replace(template, "(\\r\\n)+", "");
		}

		// Token: 0x06006F8A RID: 28554 RVA: 0x001A0DA3 File Offset: 0x0019EFA3
		internal static string SerializeColor(Color color)
		{
			return string.Format("'{0}'", HtmlChartHelper.ColorToHex(color));
		}

		// Token: 0x06006F8B RID: 28555 RVA: 0x001A0DB5 File Offset: 0x0019EFB5
		internal static string SerializeBoolean(bool boolValue)
		{
			return string.Format("{0}", boolValue.ToString().ToLower());
		}

		// Token: 0x04001E09 RID: 7689
		private const string NEW_LINE_PATTERN = "(\\r\\n)+";

		// Token: 0x04001E0A RID: 7690
		private const string TAB_PATTERN = "(\\t)+";
	}
}

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebGrease.Css.Extensions
{
	// Token: 0x02000188 RID: 392
	internal static class NumberExtensions
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x00078158 File Offset: 0x00076358
		internal static string UnaryOperator(this float? number)
		{
			float? num = number;
			if (num.GetValueOrDefault() >= 0f || num == null)
			{
				return null;
			}
			return "-";
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0007818C File Offset: 0x0007638C
		internal static string CssUnitValue(this float? number, string unit = "px")
		{
			if (number == null)
			{
				return null;
			}
			float num = Math.Abs(number.Value);
			string format;
			if (unit != null)
			{
				if (unit == "em")
				{
					format = "{0}em";
					goto IL_5C;
				}
				if (unit == "rem")
				{
					format = "{0}rem";
					goto IL_5C;
				}
				if (!(unit == "px"))
				{
				}
			}
			format = "{0}px";
			IL_5C:
			if (num != 0f)
			{
				return string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					num
				});
			}
			return "0";
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00078220 File Offset: 0x00076420
		internal static float ParseFloat(this string text)
		{
			float result;
			if (!string.IsNullOrWhiteSpace(text) && float.TryParse(text, out result))
			{
				return result;
			}
			return 0f;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00078246 File Offset: 0x00076446
		internal static int SignInt(this string unaryOperator)
		{
			if (unaryOperator == "-")
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00078258 File Offset: 0x00076458
		internal static bool TryParseZeroBasedNumberValue(this string numberBasedValue)
		{
			if (string.IsNullOrWhiteSpace(numberBasedValue))
			{
				return true;
			}
			Match match = NumberExtensions.NumberWithUnitsRegex.Match(numberBasedValue);
			if (match.Success)
			{
				string s = match.Result("$1");
				float value;
				if (float.TryParse(s, out value) && Math.Abs(value) == 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000AD4 RID: 2772
		private static readonly Regex NumberWithUnitsRegex = new Regex("([+-]?[0-9]*\\.?[0-9]+)[a-z]*", RegexOptions.IgnoreCase);
	}
}

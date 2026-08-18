using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012B2 RID: 4786
	internal class InputUtil
	{
		// Token: 0x0600C84E RID: 51278 RVA: 0x002CA348 File Offset: 0x002C8548
		internal static string EnsureEndWithSemiColon(string value)
		{
			if (value != null)
			{
				int length = value.Length;
				if (length > 0 && value[length - 1] != ';')
				{
					return value + ";";
				}
			}
			return value;
		}

		// Token: 0x0600C84F RID: 51279 RVA: 0x002CA380 File Offset: 0x002C8580
		internal static string MergeScript(string firstScript, string secondScript)
		{
			if (!string.IsNullOrEmpty(firstScript))
			{
				if (firstScript.IndexOf(secondScript) != -1)
				{
					return firstScript;
				}
				return firstScript + secondScript;
			}
			else
			{
				if (secondScript.TrimStart(new char[0]).StartsWith("javascript:"))
				{
					return secondScript;
				}
				return "javascript:" + secondScript;
			}
		}

		// Token: 0x0600C850 RID: 51280 RVA: 0x002CA3D0 File Offset: 0x002C85D0
		internal static string GetStyle(string name, InputStyle value, CssStyleCollection style)
		{
			string arg = "";
			string arg2 = "";
			InputUtil.GetStyle(name, value, ref arg, ref arg2, style);
			return string.Format("{0}: [\"{1}\", \"{2}\"]", name, arg, arg2);
		}

		// Token: 0x0600C851 RID: 51281 RVA: 0x002CA404 File Offset: 0x002C8604
		internal static string GetStyle(InputStyle value, CssStyleCollection style)
		{
			string result = "";
			string text = "";
			string name = "";
			InputUtil.GetStyle(name, value, ref result, ref text, style);
			return result;
		}

		// Token: 0x0600C852 RID: 51282 RVA: 0x002CA430 File Offset: 0x002C8630
		internal static Type[] GetNumericSupportedTypes()
		{
			return new Type[]
			{
				typeof(double),
				typeof(float),
				typeof(long),
				typeof(int),
				typeof(short),
				typeof(byte),
				typeof(decimal),
				typeof(string),
				typeof(bool),
				typeof(TimeSpan),
				typeof(char),
				typeof(sbyte),
				typeof(ushort),
				typeof(uint),
				typeof(ulong)
			};
		}

		// Token: 0x0600C853 RID: 51283 RVA: 0x002CA510 File Offset: 0x002C8710
		internal static string ToStringNumberNegativePattern(CultureInfo culture, string numericPlaceHolder)
		{
			string result = string.Empty;
			switch (culture.NumberFormat.NumberNegativePattern)
			{
			case 0:
				result = string.Format("({0})", numericPlaceHolder);
				break;
			case 1:
				result = string.Format("{0}{1}", culture.NumberFormat.NegativeSign, numericPlaceHolder);
				break;
			case 2:
				result = string.Format("{0} {1}", culture.NumberFormat.NegativeSign, numericPlaceHolder);
				break;
			case 3:
				result = string.Format("{1}{0}", culture.NumberFormat.NegativeSign, numericPlaceHolder);
				break;
			case 4:
				result = string.Format("{1} {0}", culture.NumberFormat.NegativeSign, numericPlaceHolder);
				break;
			}
			return result;
		}

		// Token: 0x0600C854 RID: 51284 RVA: 0x002CA5C0 File Offset: 0x002C87C0
		internal static string ToStringCurrencyNegativePattern(CultureInfo culture, string numericPlaceHolder)
		{
			string result = string.Empty;
			switch (culture.NumberFormat.CurrencyNegativePattern)
			{
			case 0:
				result = string.Format("({0}{1})", culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 1:
				result = string.Format("{0}{1}{2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 2:
				result = string.Format("{1}{0}{2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 3:
				result = string.Format("{1}{2}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 4:
				result = string.Format("({2}{1})", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 5:
				result = string.Format("{0}{2}{1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 6:
				result = string.Format("{2}{0}{1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 7:
				result = string.Format("{2}{1}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 8:
				result = string.Format("{0}{2} {1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 9:
				result = string.Format("{0}{1} {2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 10:
				result = string.Format("{2} {1}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 11:
				result = string.Format("{1} {2}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 12:
				result = string.Format("{1} {0}{2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 13:
				result = string.Format("{2}{0} {1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 14:
				result = string.Format("({1} {2})", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 15:
				result = string.Format("({2} {1})", culture.NumberFormat.NegativeSign, culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			}
			return result;
		}

		// Token: 0x0600C855 RID: 51285 RVA: 0x002CA884 File Offset: 0x002C8A84
		internal static string ToStringPercentNegativePattern(CultureInfo culture, string numericPlaceHolder)
		{
			string result = string.Empty;
			switch (culture.NumberFormat.PercentNegativePattern)
			{
			case 0:
				result = string.Format("{0}{2} {1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 1:
				result = string.Format("{0}{2}{1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 2:
				result = string.Format("{0}{1}{2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 3:
				result = string.Format("{1}{0}{2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 4:
				result = string.Format("{1}{2}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 5:
				result = string.Format("{2}{0}{1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 6:
				result = string.Format("{2}{1}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 7:
				result = string.Format("{0}{1} {2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 8:
				result = string.Format("{2} {1}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 9:
				result = string.Format("{1} {2}{0}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 10:
				result = string.Format("{1} {0}{2}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 11:
				result = string.Format("{2}{0} {1}", culture.NumberFormat.NegativeSign, culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			}
			return result;
		}

		// Token: 0x0600C856 RID: 51286 RVA: 0x002CAAA8 File Offset: 0x002C8CA8
		internal static string MapDateFormatShortCuts(string format, DateTimeFormatInfo dateTimeFormatInfo)
		{
			if (format.Length > 1)
			{
				return format;
			}
			switch (format)
			{
			case "d":
				return dateTimeFormatInfo.ShortDatePattern;
			case "z":
				return dateTimeFormatInfo.YearMonthPattern;
			case "D":
				return dateTimeFormatInfo.LongDatePattern;
			case "f":
				return dateTimeFormatInfo.LongDatePattern + " " + dateTimeFormatInfo.ShortTimePattern;
			case "F":
				return dateTimeFormatInfo.FullDateTimePattern;
			case "g":
				return dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern;
			case "G":
				return dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.LongTimePattern;
			case "m":
				return dateTimeFormatInfo.MonthDayPattern;
			case "M":
				return dateTimeFormatInfo.MonthDayPattern;
			case "r":
				return dateTimeFormatInfo.RFC1123Pattern;
			case "R":
				return dateTimeFormatInfo.RFC1123Pattern;
			case "s":
				return dateTimeFormatInfo.SortableDateTimePattern;
			case "t":
				return dateTimeFormatInfo.ShortTimePattern;
			case "T":
				return dateTimeFormatInfo.LongTimePattern;
			case "y":
				return dateTimeFormatInfo.MonthDayPattern;
			case "Y":
				return dateTimeFormatInfo.MonthDayPattern;
			}
			return format;
		}

		// Token: 0x0600C857 RID: 51287 RVA: 0x002CACAC File Offset: 0x002C8EAC
		internal static string ToStringCurrencyPositivePattern(CultureInfo culture, string numericPlaceHolder)
		{
			string result = string.Empty;
			switch (culture.NumberFormat.CurrencyPositivePattern)
			{
			case 0:
				result = string.Format("{0}{1}", culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 1:
				result = string.Format("{1}{0}", culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 2:
				result = string.Format("{0} {1}", culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			case 3:
				result = string.Format("{1} {0}", culture.NumberFormat.CurrencySymbol, numericPlaceHolder);
				break;
			}
			return result;
		}

		// Token: 0x0600C858 RID: 51288 RVA: 0x002CAD48 File Offset: 0x002C8F48
		internal static string ToStringPercentPositivePattern(CultureInfo culture, string numericPlaceHolder)
		{
			string result = string.Empty;
			switch (culture.NumberFormat.PercentPositivePattern)
			{
			case 0:
				result = string.Format("{1} {0}", culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 1:
				result = string.Format("{1}{0}", culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 2:
				result = string.Format("{0}{1}", culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			case 3:
				result = string.Format("{0} {1}", culture.NumberFormat.PercentSymbol, numericPlaceHolder);
				break;
			}
			return result;
		}

		// Token: 0x0600C859 RID: 51289 RVA: 0x002CADE4 File Offset: 0x002C8FE4
		internal static string ToStringNumberPositivePattern(string numericPlaceHolder)
		{
			return numericPlaceHolder;
		}

		// Token: 0x0600C85A RID: 51290 RVA: 0x002CADE8 File Offset: 0x002C8FE8
		private static void GetStyle(string name, InputStyle value, ref string cssText, ref string cssClass, CssStyleCollection style)
		{
			string text = string.Empty;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			value.AddAttributesToRender(htmlTextWriter);
			htmlTextWriter.RenderBeginTag("");
			htmlTextWriter.RenderEndTag();
			string text2 = stringWriter.ToString().Replace("&#32;", " ");
			if (!string.IsNullOrEmpty(text2))
			{
				int num = text2.IndexOf("style=\"");
				if (num >= 0)
				{
					num += 7;
					int num2 = text2.IndexOf("\"", num);
					cssText = text2.Substring(num, num2 - num);
				}
				IEnumerator enumerator = style.Keys.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!InputUtil.IsPositionAttribute(enumerator.Current) && !InputUtil.IsSizeAttribute(enumerator.Current))
					{
						text += string.Format("{0}:{1};", enumerator.Current, style[enumerator.Current.ToString()]);
					}
				}
				cssText += text;
				num = text2.IndexOf("class=\"");
				if (num >= 0)
				{
					num += 7;
					int num3 = text2.IndexOf("\"", num);
					cssClass = text2.Substring(num, num3 - num);
				}
			}
		}

		// Token: 0x0600C85B RID: 51291 RVA: 0x002CAF18 File Offset: 0x002C9118
		private static bool IsPositionAttribute(object key)
		{
			foreach (string text in InputUtil.PositionAttributes)
			{
				if (key.ToString().ToUpper().Trim() == text.ToString().ToUpper().Trim())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C85C RID: 51292 RVA: 0x002CAF6C File Offset: 0x002C916C
		private static bool IsSizeAttribute(object key)
		{
			foreach (string text in InputUtil.SizeAttributes)
			{
				if (key.ToString().ToUpper().Trim() == text.ToString().ToUpper().Trim())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C85D RID: 51293 RVA: 0x002CAFC0 File Offset: 0x002C91C0
		internal static string GetAbsolutePositionValue(CssStyleCollection cssStyleCollection)
		{
			string text = string.Empty;
			foreach (string text2 in InputUtil.PositionAttributes)
			{
				if (cssStyleCollection[text2] != null)
				{
					text += string.Format("{0}:{1};", text2, cssStyleCollection[text2]);
				}
			}
			return text;
		}

		// Token: 0x0600C85E RID: 51294 RVA: 0x002CB010 File Offset: 0x002C9210
		internal static string GetSize(CssStyleCollection cssStyleCollection, Unit setWidth, Unit setHeight)
		{
			string text = string.Empty;
			if (!setWidth.IsEmpty)
			{
				text += string.Format("{0}:{1};", "width", setWidth.ToString());
			}
			foreach (string text2 in InputUtil.SizeAttributes)
			{
				if (cssStyleCollection[text2] != null && text2 != "height")
				{
					text += string.Format("{0}:{1};", text2, cssStyleCollection[text2]);
				}
			}
			return text;
		}

		// Token: 0x0600C85F RID: 51295 RVA: 0x002CB098 File Offset: 0x002C9298
		internal static string IncrementSettingsToClient(InputIncrementSettings inputIncrementSettings)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("InterceptArrowKeys:{0},", inputIncrementSettings.InterceptArrowKeys.ToString().ToLower());
			stringBuilder.AppendFormat("InterceptMouseWheel:{0},", inputIncrementSettings.InterceptMouseWheel.ToString().ToLower());
			stringBuilder.AppendFormat("Step:{0}", inputIncrementSettings.Step.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C860 RID: 51296 RVA: 0x002CB128 File Offset: 0x002C9328
		internal static string PasswordStrengthSettingsToClient(InputPasswordStrengthSettings passwordStrengthSettings)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("ShowIndicator:{0},", passwordStrengthSettings.ShowIndicator.ToString().ToLower());
			stringBuilder.AppendFormat("CalculationWeightings:'{0}',", passwordStrengthSettings.CalculationWeightings.ToString().ToLower());
			stringBuilder.AppendFormat("PreferredPasswordLength:{0},", passwordStrengthSettings.PreferredPasswordLength.ToString().ToLower());
			stringBuilder.AppendFormat("MinimumNumericCharacters:{0},", passwordStrengthSettings.MinimumNumericCharacters.ToString().ToLower());
			stringBuilder.AppendFormat("RequiresUpperAndLowerCaseCharacters:{0},", passwordStrengthSettings.RequiresUpperAndLowerCaseCharacters.ToString().ToLower());
			stringBuilder.AppendFormat("MinLowerCaseChars:{0},", passwordStrengthSettings.MinimumLowerCaseCharacters.ToString().ToLower());
			stringBuilder.AppendFormat("MinUpperCaseChars:{0},", passwordStrengthSettings.MinimumUpperCaseCharacters.ToString().ToLower());
			stringBuilder.AppendFormat("MinimumSymbolCharacters:{0},", passwordStrengthSettings.MinimumSymbolCharacters.ToString().ToLower());
			stringBuilder.AppendFormat("TextStrengthDescriptions:'{0}',", passwordStrengthSettings.TextStrengthDescriptions);
			stringBuilder.AppendFormat("TextStrengthDescriptionStyles:'{0}',", passwordStrengthSettings.TextStrengthDescriptionStyles);
			stringBuilder.AppendFormat("IndicatorElementBaseStyle:'{0}',", passwordStrengthSettings.IndicatorElementBaseStyle);
			stringBuilder.AppendFormat("IndicatorElementID:'{0}'", passwordStrengthSettings.IndicatorElementID);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C861 RID: 51297 RVA: 0x002CB29C File Offset: 0x002C949C
		internal static double ParseDouble(TextBox textBox, CultureInfo culture)
		{
			string text = null;
			double result = InputUtil.ParseDouble(textBox.Text, culture, out text);
			if (text != null)
			{
				textBox.Text = text;
			}
			return result;
		}

		// Token: 0x0600C862 RID: 51298 RVA: 0x002CB2C8 File Offset: 0x002C94C8
		internal static double ParseDouble(TextBox textBox, CultureInfo culture, NumericType type)
		{
			string text = null;
			double result;
			if (type == NumericType.Percent)
			{
				result = InputUtil.ParseDouble(textBox.Text.Replace("%", string.Empty), culture, out text);
			}
			else
			{
				result = InputUtil.ParseDouble(textBox.Text, culture, out text);
			}
			if (text != null)
			{
				textBox.Text = text;
			}
			return result;
		}

		// Token: 0x0600C863 RID: 51299 RVA: 0x002CB318 File Offset: 0x002C9518
		internal static double ParseDouble(string text, CultureInfo culture, out string textBoxText)
		{
			textBoxText = null;
			double result;
			bool flag = double.TryParse(text, NumberStyles.Any, culture, out result);
			if (!flag)
			{
				flag = double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out result);
			}
			if (!flag)
			{
				textBoxText = "";
			}
			return result;
		}

		// Token: 0x0600C864 RID: 51300 RVA: 0x002CB358 File Offset: 0x002C9558
		public static string FormatDouble(double value, NumberFormatSettings NumberFormat)
		{
			bool flag = value < 0.0;
			CultureInfo cultureInfo = new CultureInfo("en-tt");
			cultureInfo.NumberFormat.NumberDecimalDigits = NumberFormat.DecimalDigits;
			cultureInfo.NumberFormat.NumberDecimalSeparator = NumberFormat.DecimalSeparator;
			cultureInfo.NumberFormat.NumberGroupSeparator = NumberFormat.GroupSeparator;
			cultureInfo.NumberFormat.NumberGroupSizes = new int[]
			{
				NumberFormat.GroupSizes
			};
			value = Math.Abs(value);
			string text = value.ToString("N", cultureInfo);
			if (!NumberFormat.AllowRounding)
			{
				double num = value - Math.Round(value, cultureInfo.NumberFormat.NumberDecimalDigits);
				if (num < 0.0)
				{
					text = (value - Math.Pow(0.1, (double)cultureInfo.NumberFormat.NumberDecimalDigits)).ToString("N", cultureInfo);
				}
				string[] array = text.Split(new string[]
				{
					NumberFormat.DecimalSeparator
				}, StringSplitOptions.None);
				if (array.Length == 2)
				{
					array[1] = array[1].TrimEnd(new char[]
					{
						'0'
					});
					if (array[1].Length > 0)
					{
						text = string.Format("{0}{1}{2}", array[0], NumberFormat.DecimalSeparator, array[1]);
					}
					else
					{
						text = array[0];
					}
				}
			}
			double num2;
			bool flag2 = double.TryParse(text, out num2);
			if (flag2 && num2 == 0.0)
			{
				return InputUtil.ReplaceN(NumberFormat.ZeroPattern, text, NumberFormat.NegativeSign, NumberFormat.NumericPlaceHolder);
			}
			if (flag)
			{
				return InputUtil.ReplaceN(NumberFormat.NegativePattern, text, NumberFormat.NegativeSign, NumberFormat.NumericPlaceHolder);
			}
			return InputUtil.ReplaceN(NumberFormat.PositivePattern, text, NumberFormat.NegativeSign, NumberFormat.NumericPlaceHolder);
		}

		// Token: 0x0600C865 RID: 51301 RVA: 0x002CB514 File Offset: 0x002C9714
		private static string ReplaceN(string pattern, string value, string negativeSign, string numberPlaceHolder)
		{
			char c = Convert.ToChar(negativeSign);
			string value2 = negativeSign + numberPlaceHolder;
			string value3 = numberPlaceHolder + negativeSign;
			if (pattern.IndexOf(numberPlaceHolder) == -1)
			{
				throw new Exception("Invalid pattern");
			}
			int num;
			int num2;
			if (pattern.IndexOf(value2, StringComparison.Ordinal) != -1)
			{
				num = pattern.IndexOf(value2, StringComparison.Ordinal);
				num2 = 1;
			}
			else if (pattern.IndexOf(value3, StringComparison.Ordinal) != -1)
			{
				num = pattern.IndexOf(value3, StringComparison.Ordinal);
				num2 = 0;
			}
			else
			{
				num = pattern.IndexOf(numberPlaceHolder + " ");
				num2 = 0;
				if (num == -1 || (num > 0 && pattern[num - 1] != c))
				{
					num = pattern.IndexOf(" " + numberPlaceHolder);
					num2 = 1;
					if (num == -1 || num != pattern.Length - 2)
					{
						num = pattern.IndexOf(string.Format(" {0} ", numberPlaceHolder));
						num2 = 1;
					}
				}
				if (num == -1)
				{
					num = pattern.IndexOf(numberPlaceHolder);
					num2 = 0;
				}
			}
			if (num == -1)
			{
				throw new Exception("Invalid pattern");
			}
			pattern = pattern.Remove(num + num2, 1);
			return pattern.Insert(num + num2, value);
		}

		// Token: 0x040034BF RID: 13503
		private static string[] PositionAttributes = new string[]
		{
			"z-index",
			"right",
			"left",
			"position",
			"top",
			"bottom",
			"margin",
			"margin-left",
			"margin-right",
			"margin-top",
			"margin-bottom"
		};

		// Token: 0x040034C0 RID: 13504
		private static string[] SizeAttributes = new string[]
		{
			"width",
			"height"
		};
	}
}

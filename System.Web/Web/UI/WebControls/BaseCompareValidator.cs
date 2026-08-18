using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C9 RID: 1225
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseCompareValidator : BaseValidator
	{
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000F7288 File Offset: 0x000F6288
		// (set) Token: 0x06003A90 RID: 14992 RVA: 0x000F72B1 File Offset: 0x000F62B1
		[WebCategory("Behavior")]
		[WebSysDescription("RangeValidator_Type")]
		[Themeable(false)]
		[DefaultValue(ValidationDataType.String)]
		public ValidationDataType Type
		{
			get
			{
				object obj = this.ViewState["Type"];
				if (obj != null)
				{
					return (ValidationDataType)obj;
				}
				return ValidationDataType.String;
			}
			set
			{
				if (value < ValidationDataType.String || value > ValidationDataType.Currency)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Type"] = value;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x000F72DC File Offset: 0x000F62DC
		// (set) Token: 0x06003A92 RID: 14994 RVA: 0x000F7305 File Offset: 0x000F6305
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("BaseCompareValidator_CultureInvariantValues")]
		[Themeable(false)]
		public bool CultureInvariantValues
		{
			get
			{
				object obj = this.ViewState["CultureInvariantValues"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["CultureInvariantValues"] = value;
			}
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000F7320 File Offset: 0x000F6320
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				ValidationDataType type = this.Type;
				if (type != ValidationDataType.String)
				{
					string clientID = this.ClientID;
					HtmlTextWriter writer2 = base.EnableLegacyRendering ? writer : null;
					base.AddExpandoAttribute(writer2, clientID, "type", PropertyConverter.EnumToString(typeof(ValidationDataType), type), false);
					NumberFormatInfo currentInfo = NumberFormatInfo.CurrentInfo;
					if (type == ValidationDataType.Double)
					{
						string numberDecimalSeparator = currentInfo.NumberDecimalSeparator;
						base.AddExpandoAttribute(writer2, clientID, "decimalchar", numberDecimalSeparator);
						return;
					}
					if (type == ValidationDataType.Currency)
					{
						string currencyDecimalSeparator = currentInfo.CurrencyDecimalSeparator;
						base.AddExpandoAttribute(writer2, clientID, "decimalchar", currencyDecimalSeparator);
						string text = currentInfo.CurrencyGroupSeparator;
						if (text[0] == '\u00a0')
						{
							text = " ";
						}
						base.AddExpandoAttribute(writer2, clientID, "groupchar", text);
						base.AddExpandoAttribute(writer2, clientID, "digits", currentInfo.CurrencyDecimalDigits.ToString(NumberFormatInfo.InvariantInfo), false);
						int currencyGroupSize = BaseCompareValidator.GetCurrencyGroupSize(currentInfo);
						if (currencyGroupSize > 0)
						{
							base.AddExpandoAttribute(writer2, clientID, "groupsize", currencyGroupSize.ToString(NumberFormatInfo.InvariantInfo), false);
							return;
						}
					}
					else if (type == ValidationDataType.Date)
					{
						base.AddExpandoAttribute(writer2, clientID, "dateorder", BaseCompareValidator.GetDateElementOrder(), false);
						base.AddExpandoAttribute(writer2, clientID, "cutoffyear", BaseCompareValidator.CutoffYear.ToString(NumberFormatInfo.InvariantInfo), false);
						int year = DateTime.Today.Year;
						base.AddExpandoAttribute(writer2, clientID, "century", (year - year % 100).ToString(NumberFormatInfo.InvariantInfo), false);
					}
				}
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000F74A9 File Offset: 0x000F64A9
		public static bool CanConvert(string text, ValidationDataType type)
		{
			return BaseCompareValidator.CanConvert(text, type, false);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000F74B4 File Offset: 0x000F64B4
		public static bool CanConvert(string text, ValidationDataType type, bool cultureInvariant)
		{
			object obj = null;
			return BaseCompareValidator.Convert(text, type, cultureInvariant, out obj);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000F74D0 File Offset: 0x000F64D0
		protected static string GetDateElementOrder()
		{
			DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
			string shortDatePattern = currentInfo.ShortDatePattern;
			if (shortDatePattern.IndexOf('y') < shortDatePattern.IndexOf('M'))
			{
				return "ymd";
			}
			if (shortDatePattern.IndexOf('M') < shortDatePattern.IndexOf('d'))
			{
				return "mdy";
			}
			return "dmy";
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000F7520 File Offset: 0x000F6520
		private static int GetCurrencyGroupSize(NumberFormatInfo info)
		{
			int[] currencyGroupSizes = info.CurrencyGroupSizes;
			if (currencyGroupSizes != null && currencyGroupSizes.Length == 1)
			{
				return currencyGroupSizes[0];
			}
			return -1;
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000F7542 File Offset: 0x000F6542
		protected static int CutoffYear
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.Calendar.TwoDigitYearMax;
			}
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000F7553 File Offset: 0x000F6553
		protected static int GetFullYear(int shortYear)
		{
			return DateTimeFormatInfo.CurrentInfo.Calendar.ToFourDigitYear(shortYear);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F7565 File Offset: 0x000F6565
		protected static bool Convert(string text, ValidationDataType type, out object value)
		{
			return BaseCompareValidator.Convert(text, type, false, out value);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000F7570 File Offset: 0x000F6570
		protected static bool Convert(string text, ValidationDataType type, bool cultureInvariant, out object value)
		{
			value = null;
			try
			{
				switch (type)
				{
				case ValidationDataType.String:
					value = text;
					break;
				case ValidationDataType.Integer:
					value = int.Parse(text, CultureInfo.InvariantCulture);
					break;
				case ValidationDataType.Double:
				{
					string text2;
					if (cultureInvariant)
					{
						text2 = BaseCompareValidator.ConvertDouble(text, CultureInfo.InvariantCulture.NumberFormat);
					}
					else
					{
						text2 = BaseCompareValidator.ConvertDouble(text, NumberFormatInfo.CurrentInfo);
					}
					if (text2 != null)
					{
						value = double.Parse(text2, CultureInfo.InvariantCulture);
					}
					break;
				}
				case ValidationDataType.Date:
					if (cultureInvariant)
					{
						value = BaseCompareValidator.ConvertDate(text, "ymd");
					}
					else if (DateTimeFormatInfo.CurrentInfo.Calendar.GetType() != typeof(GregorianCalendar))
					{
						value = DateTime.Parse(text, CultureInfo.CurrentCulture);
					}
					else
					{
						string dateElementOrder = BaseCompareValidator.GetDateElementOrder();
						value = BaseCompareValidator.ConvertDate(text, dateElementOrder);
					}
					break;
				case ValidationDataType.Currency:
				{
					string text3;
					if (cultureInvariant)
					{
						text3 = BaseCompareValidator.ConvertCurrency(text, CultureInfo.InvariantCulture.NumberFormat);
					}
					else
					{
						text3 = BaseCompareValidator.ConvertCurrency(text, NumberFormatInfo.CurrentInfo);
					}
					if (text3 != null)
					{
						value = decimal.Parse(text3, CultureInfo.InvariantCulture);
					}
					break;
				}
				}
			}
			catch
			{
				value = null;
			}
			return value != null;
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000F76B4 File Offset: 0x000F66B4
		private static string ConvertCurrency(string text, NumberFormatInfo info)
		{
			string currencyDecimalSeparator = info.CurrencyDecimalSeparator;
			string text2 = info.CurrencyGroupSeparator;
			int currencyGroupSize = BaseCompareValidator.GetCurrencyGroupSize(info);
			string text3;
			string text4;
			if (currencyGroupSize > 0)
			{
				string str = currencyGroupSize.ToString(NumberFormatInfo.InvariantInfo);
				text3 = "{1," + str + "}";
				text4 = "{" + str + "}";
			}
			else
			{
				text4 = (text3 = "+");
			}
			if (text2[0] == '\u00a0')
			{
				text2 = " ";
			}
			int currencyDecimalDigits = info.CurrencyDecimalDigits;
			bool flag = currencyDecimalDigits > 0;
			string pattern = string.Concat(new string[]
			{
				"^\\s*([-\\+])?((\\d",
				text3,
				"(\\",
				text2,
				"\\d",
				text4,
				")+)|\\d*)",
				flag ? string.Concat(new string[]
				{
					"\\",
					currencyDecimalSeparator,
					"?(\\d{0,",
					currencyDecimalDigits.ToString(NumberFormatInfo.InvariantInfo),
					"})"
				}) : string.Empty,
				"\\s*$"
			});
			Match match = Regex.Match(text, pattern);
			if (!match.Success)
			{
				return null;
			}
			if (match.Groups[2].Length == 0 && flag && match.Groups[5].Length == 0)
			{
				return null;
			}
			return match.Groups[1].Value + match.Groups[2].Value.Replace(text2, string.Empty) + ((flag && match.Groups[5].Length > 0) ? ("." + match.Groups[5].Value) : string.Empty);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000F788C File Offset: 0x000F688C
		private static string ConvertDouble(string text, NumberFormatInfo info)
		{
			if (text.Length == 0)
			{
				return "0";
			}
			string numberDecimalSeparator = info.NumberDecimalSeparator;
			string pattern = "^\\s*([-\\+])?(\\d*)\\" + numberDecimalSeparator + "?(\\d*)\\s*$";
			Match match = Regex.Match(text, pattern);
			if (!match.Success)
			{
				return null;
			}
			if (match.Groups[2].Length == 0 && match.Groups[3].Length == 0)
			{
				return null;
			}
			return match.Groups[1].Value + ((match.Groups[2].Length > 0) ? match.Groups[2].Value : "0") + ((match.Groups[3].Length > 0) ? ("." + match.Groups[3].Value) : string.Empty);
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000F7974 File Offset: 0x000F6974
		private static object ConvertDate(string text, string dateElementOrder)
		{
			string pattern = "^\\s*((\\d{4})|(\\d{2}))([-/]|\\. ?)(\\d{1,2})\\4(\\d{1,2})\\.?\\s*$";
			Match match = Regex.Match(text, pattern);
			int day;
			int month;
			int year;
			if (match.Success && (match.Groups[2].Success || dateElementOrder == "ymd"))
			{
				day = int.Parse(match.Groups[6].Value, CultureInfo.InvariantCulture);
				month = int.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
				if (match.Groups[2].Success)
				{
					year = int.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
				}
				else
				{
					year = BaseCompareValidator.GetFullYear(int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture));
				}
			}
			else
			{
				if (dateElementOrder == "ymd")
				{
					return null;
				}
				string pattern2 = "^\\s*(\\d{1,2})([-/]|\\. ?)(\\d{1,2})(?:\\s|\\2)((\\d{4})|(\\d{2}))(?:\\sг\\.)?\\s*$";
				match = Regex.Match(text, pattern2);
				if (!match.Success)
				{
					return null;
				}
				if (dateElementOrder == "mdy")
				{
					day = int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
					month = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
				}
				else
				{
					day = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
					month = int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
				}
				if (match.Groups[5].Success)
				{
					year = int.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
				}
				else
				{
					year = BaseCompareValidator.GetFullYear(int.Parse(match.Groups[6].Value, CultureInfo.InvariantCulture));
				}
			}
			return new DateTime(year, month, day);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000F7B5B File Offset: 0x000F6B5B
		protected static bool Compare(string leftText, string rightText, ValidationCompareOperator op, ValidationDataType type)
		{
			return BaseCompareValidator.Compare(leftText, false, rightText, false, op, type);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000F7B68 File Offset: 0x000F6B68
		protected static bool Compare(string leftText, bool cultureInvariantLeftText, string rightText, bool cultureInvariantRightText, ValidationCompareOperator op, ValidationDataType type)
		{
			object obj;
			if (!BaseCompareValidator.Convert(leftText, type, cultureInvariantLeftText, out obj))
			{
				return false;
			}
			if (op == ValidationCompareOperator.DataTypeCheck)
			{
				return true;
			}
			object obj2;
			if (!BaseCompareValidator.Convert(rightText, type, cultureInvariantRightText, out obj2))
			{
				return true;
			}
			int num;
			switch (type)
			{
			case ValidationDataType.String:
				num = string.Compare((string)obj, (string)obj2, false, CultureInfo.CurrentCulture);
				break;
			case ValidationDataType.Integer:
				num = ((int)obj).CompareTo(obj2);
				break;
			case ValidationDataType.Double:
				num = ((double)obj).CompareTo(obj2);
				break;
			case ValidationDataType.Date:
				num = ((DateTime)obj).CompareTo(obj2);
				break;
			case ValidationDataType.Currency:
				num = ((decimal)obj).CompareTo(obj2);
				break;
			default:
				return true;
			}
			switch (op)
			{
			case ValidationCompareOperator.Equal:
				return num == 0;
			case ValidationCompareOperator.NotEqual:
				return num != 0;
			case ValidationCompareOperator.GreaterThan:
				return num > 0;
			case ValidationCompareOperator.GreaterThanEqual:
				return num >= 0;
			case ValidationCompareOperator.LessThan:
				return num < 0;
			case ValidationCompareOperator.LessThanEqual:
				return num <= 0;
			default:
				return true;
			}
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000F7C6E File Offset: 0x000F6C6E
		protected override bool DetermineRenderUplevel()
		{
			return (this.Type != ValidationDataType.Date || DateTimeFormatInfo.CurrentInfo.Calendar.GetType() == typeof(GregorianCalendar)) && base.DetermineRenderUplevel();
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000F7C9C File Offset: 0x000F6C9C
		internal string ConvertToShortDateString(string text)
		{
			DateTime dateTime;
			if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime))
			{
				text = dateTime.ToShortDateString();
			}
			return text;
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000F7CC3 File Offset: 0x000F6CC3
		internal bool IsInStandardDateFormat(string date)
		{
			return Regex.Match(date, "^\\s*(\\d+)([-/]|\\. ?)(\\d+)\\2(\\d+)\\s*$").Success;
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000F7CD8 File Offset: 0x000F6CD8
		internal string ConvertCultureInvariantToCurrentCultureFormat(string valueInString, ValidationDataType type)
		{
			object obj;
			BaseCompareValidator.Convert(valueInString, type, true, out obj);
			if (obj is DateTime)
			{
				return ((DateTime)obj).ToShortDateString();
			}
			return System.Convert.ToString(obj, CultureInfo.CurrentCulture);
		}
	}
}

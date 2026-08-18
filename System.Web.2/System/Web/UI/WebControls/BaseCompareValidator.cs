using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200037B RID: 891
	public abstract class BaseCompareValidator : BaseValidator
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x0008487C File Offset: 0x00082A7C
		// (set) Token: 0x06002906 RID: 10502 RVA: 0x000848A5 File Offset: 0x00082AA5
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(ValidationDataType.String)]
		[WebSysDescription("RangeValidator_Type")]
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

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000848D0 File Offset: 0x00082AD0
		// (set) Token: 0x06002908 RID: 10504 RVA: 0x000848F9 File Offset: 0x00082AF9
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(false)]
		[WebSysDescription("BaseCompareValidator_CultureInvariantValues")]
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

		// Token: 0x06002909 RID: 10505 RVA: 0x00084914 File Offset: 0x00082B14
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				ValidationDataType type = this.Type;
				if (type != ValidationDataType.String)
				{
					string clientID = this.ClientID;
					HtmlTextWriter writer2 = (base.EnableLegacyRendering || base.IsUnobtrusive) ? writer : null;
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

		// Token: 0x0600290A RID: 10506 RVA: 0x00084AA5 File Offset: 0x00082CA5
		public static bool CanConvert(string text, ValidationDataType type)
		{
			return BaseCompareValidator.CanConvert(text, type, false);
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00084AB0 File Offset: 0x00082CB0
		public static bool CanConvert(string text, ValidationDataType type, bool cultureInvariant)
		{
			object obj = null;
			return BaseCompareValidator.Convert(text, type, cultureInvariant, out obj);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00084ACC File Offset: 0x00082CCC
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

		// Token: 0x0600290D RID: 10509 RVA: 0x00084B1C File Offset: 0x00082D1C
		private static int GetCurrencyGroupSize(NumberFormatInfo info)
		{
			int[] currencyGroupSizes = info.CurrencyGroupSizes;
			if (currencyGroupSizes != null && currencyGroupSizes.Length == 1)
			{
				return currencyGroupSizes[0];
			}
			return -1;
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x00084B3E File Offset: 0x00082D3E
		protected static int CutoffYear
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.Calendar.TwoDigitYearMax;
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00084B4F File Offset: 0x00082D4F
		protected static int GetFullYear(int shortYear)
		{
			return DateTimeFormatInfo.CurrentInfo.Calendar.ToFourDigitYear(shortYear);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x00084B61 File Offset: 0x00082D61
		protected static bool Convert(string text, ValidationDataType type, out object value)
		{
			return BaseCompareValidator.Convert(text, type, false, out value);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00084B6C File Offset: 0x00082D6C
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
					else if (!(DateTimeFormatInfo.CurrentInfo.Calendar.GetType() == typeof(GregorianCalendar)))
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

		// Token: 0x06002912 RID: 10514 RVA: 0x00084CB0 File Offset: 0x00082EB0
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

		// Token: 0x06002913 RID: 10515 RVA: 0x00084E74 File Offset: 0x00083074
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

		// Token: 0x06002914 RID: 10516 RVA: 0x00084F5C File Offset: 0x0008315C
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
				string pattern2 = "^\\s*(\\d{1,2})([-/]|\\. ?)(\\d{1,2})(?:\\s|\\2)((\\d{4})|(\\d{2}))(?:\\sг\\.|\\.)?\\s*$";
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

		// Token: 0x06002915 RID: 10517 RVA: 0x00085143 File Offset: 0x00083343
		protected static bool Compare(string leftText, string rightText, ValidationCompareOperator op, ValidationDataType type)
		{
			return BaseCompareValidator.Compare(leftText, false, rightText, false, op, type);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00085150 File Offset: 0x00083350
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

		// Token: 0x06002917 RID: 10519 RVA: 0x0008524C File Offset: 0x0008344C
		protected override bool DetermineRenderUplevel()
		{
			return (this.Type != ValidationDataType.Date || !(DateTimeFormatInfo.CurrentInfo.Calendar.GetType() != typeof(GregorianCalendar))) && base.DetermineRenderUplevel();
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00085280 File Offset: 0x00083480
		internal string ConvertToShortDateString(string text)
		{
			DateTime dateTime;
			if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime))
			{
				text = dateTime.ToShortDateString();
			}
			return text;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000852A7 File Offset: 0x000834A7
		internal bool IsInStandardDateFormat(string date)
		{
			return Regex.Match(date, "^\\s*(\\d+)([-/]|\\. ?)(\\d+)\\2(\\d+)\\s*$").Success;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000852BC File Offset: 0x000834BC
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

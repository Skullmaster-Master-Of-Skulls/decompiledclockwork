using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Input.Serialization
{
	// Token: 0x020012AA RID: 4778
	internal class DateTimeFormatInfoConverter : JavaScriptConverter
	{
		// Token: 0x0600C806 RID: 51206 RVA: 0x002C902B File Offset: 0x002C722B
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotSupportedException("The method or operation is not implemented.");
		}

		// Token: 0x0600C807 RID: 51207 RVA: 0x002C9038 File Offset: 0x002C7238
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			this._dateInput = (obj as IRadDateInput);
			if (this._dateInput == null)
			{
				throw new ArgumentException("Can serialize only IRadDateInput objects.");
			}
			CultureInfo cultureInfo = new CultureInfo(this._dateInput.Culture.Name);
			cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DayNames", cultureInfo.DateTimeFormat.DayNames);
			dictionary.Add("MonthNames", cultureInfo.DateTimeFormat.MonthNames);
			dictionary.Add("AbbreviatedDayNames", cultureInfo.DateTimeFormat.AbbreviatedDayNames);
			dictionary.Add("AbbreviatedMonthNames", cultureInfo.DateTimeFormat.AbbreviatedMonthNames);
			dictionary.Add("AMDesignator", cultureInfo.DateTimeFormat.AMDesignator);
			dictionary.Add("PMDesignator", cultureInfo.DateTimeFormat.PMDesignator);
			dictionary.Add("DateSeparator", cultureInfo.DateTimeFormat.DateSeparator);
			if (cultureInfo.DateTimeFormat.TimeSeparator == cultureInfo.DateTimeFormat.DateSeparator)
			{
				dictionary.Add("TimeSeparator", ":");
			}
			else
			{
				dictionary.Add("TimeSeparator", cultureInfo.DateTimeFormat.TimeSeparator);
			}
			dictionary.Add("FirstDayOfWeek", cultureInfo.DateTimeFormat.FirstDayOfWeek);
			dictionary.Add("DateSlots", this.DateSlots);
			dictionary.Add("ShortYearCenturyEnd", this._dateInput.ShortYearCenturyEnd);
			dictionary.Add("TimeInputOnly", this.TimeInputOnly);
			dictionary.Add("MonthYearOnly", this.MonthYearOnly);
			return dictionary;
		}

		// Token: 0x1700409D RID: 16541
		// (get) Token: 0x0600C808 RID: 51208 RVA: 0x002C91E4 File Offset: 0x002C73E4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadDateInput),
					typeof(DateInputSetting)
				};
			}
		}

		// Token: 0x1700409E RID: 16542
		// (get) Token: 0x0600C809 RID: 51209 RVA: 0x002C921C File Offset: 0x002C741C
		internal Hashtable DateSlots
		{
			get
			{
				string text = DateTimeFormatInfoConverter.WhitespacePreservingEscape(this._dateInput.DateFormat);
				string text2 = text.Replace("dddd", string.Empty).Replace("ddd", string.Empty);
				Hashtable hashtable = new Hashtable();
				int num = 0;
				bool[] array = new bool[]
				{
					true,
					true,
					true
				};
				foreach (char c in text2)
				{
					if (c == 'M' && array[0])
					{
						hashtable["Month"] = num;
						num++;
						array[0] = false;
					}
					else if (c == 'd' && array[1])
					{
						hashtable["Day"] = num;
						num++;
						array[1] = false;
					}
					else if (c == 'y' && array[2])
					{
						hashtable["Year"] = num;
						num++;
						array[2] = false;
					}
				}
				if (num == 3)
				{
					return hashtable;
				}
				hashtable = null;
				string[] array2 = Regex.Split(text, "[^dMy]+");
				List<string> list = new List<string>();
				foreach (string text4 in array2)
				{
					if (text4.Length > 0)
					{
						list.Add(text4);
					}
				}
				array2 = list.ToArray();
				if (array2.Length == 3)
				{
					hashtable = DateTimeFormatInfoConverter.GetDateSlots(array2);
				}
				if (hashtable == null)
				{
					string input = DateTimeFormatInfoConverter.WhitespacePreservingEscape(this._dateInput.Culture.DateTimeFormat.ShortDatePattern);
					string pattern = DateTimeFormatInfoConverter.WhitespacePreservingEscape(this._dateInput.Culture.DateTimeFormat.DateSeparator);
					array2 = Regex.Split(input, pattern);
					hashtable = DateTimeFormatInfoConverter.GetDateSlots(array2);
				}
				return hashtable;
			}
		}

		// Token: 0x0600C80A RID: 51210 RVA: 0x002C93CC File Offset: 0x002C75CC
		private static Hashtable GetDateSlots(string[] dateParts)
		{
			Hashtable hashtable = new Hashtable();
			int num = 0;
			foreach (string text in dateParts)
			{
				if (text.IndexOf("M", StringComparison.InvariantCulture) != -1)
				{
					hashtable["Month"] = num;
					num++;
				}
				else if (text.IndexOf("d", StringComparison.InvariantCulture) != -1)
				{
					hashtable["Day"] = num;
					num++;
				}
				else if (text.IndexOf("y", StringComparison.InvariantCulture) != -1)
				{
					hashtable["Year"] = num;
					num++;
				}
			}
			if (hashtable.Keys.Count != 3)
			{
				return null;
			}
			return hashtable;
		}

		// Token: 0x0600C80B RID: 51211 RVA: 0x002C947E File Offset: 0x002C767E
		private static string WhitespacePreservingEscape(string input)
		{
			input = input.Replace("\\", "\\\\");
			input = input.Replace("/", "\\/");
			input = input.Replace(".", "\\.");
			return input;
		}

		// Token: 0x1700409F RID: 16543
		// (get) Token: 0x0600C80C RID: 51212 RVA: 0x002C94B8 File Offset: 0x002C76B8
		internal bool TimeInputOnly
		{
			get
			{
				string input = Regex.Escape(InputUtil.MapDateFormatShortCuts(this._dateInput.DateFormat, this._dateInput.Culture.DateTimeFormat));
				string pattern = Regex.Escape(this._dateInput.Culture.DateTimeFormat.DateSeparator);
				string[] array = Regex.Split(input, pattern);
				bool flag = false;
				foreach (string text in array)
				{
					if (text.IndexOf("M") != -1)
					{
						flag = true;
						break;
					}
					if (text.IndexOf("d") != -1)
					{
						flag = true;
						break;
					}
					if (text.IndexOf("y") != -1)
					{
						flag = true;
						break;
					}
				}
				return !flag;
			}
		}

		// Token: 0x170040A0 RID: 16544
		// (get) Token: 0x0600C80D RID: 51213 RVA: 0x002C956C File Offset: 0x002C776C
		internal bool MonthYearOnly
		{
			get
			{
				string input = Regex.Escape(InputUtil.MapDateFormatShortCuts(this._dateInput.DateFormat, this._dateInput.Culture.DateTimeFormat));
				string pattern = Regex.Escape(this._dateInput.Culture.DateTimeFormat.DateSeparator);
				string[] array = Regex.Split(input, pattern);
				bool flag = false;
				foreach (string text in array)
				{
					if (text.IndexOf("d") != -1)
					{
						flag = true;
						break;
					}
				}
				return !flag;
			}
		}

		// Token: 0x040034B0 RID: 13488
		private IRadDateInput _dateInput;
	}
}

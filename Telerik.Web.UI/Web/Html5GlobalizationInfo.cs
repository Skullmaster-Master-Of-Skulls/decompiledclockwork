using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Telerik.Web
{
	// Token: 0x02000158 RID: 344
	public static class Html5GlobalizationInfo
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x000317E8 File Offset: 0x0002F9E8
		private static IDictionary<string, object> BuildFlatDictionary(CultureInfo cultureInfo)
		{
			NumberFormatInfo numberFormat = cultureInfo.NumberFormat;
			DateTimeFormatInfo dateTimeFormat = cultureInfo.DateTimeFormat;
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["Name"] = cultureInfo.Name;
			dictionary["NumberPattern"] = new string[]
			{
				Html5GlobalizationInfo.numberNegativePatterns[cultureInfo.NumberFormat.NumberNegativePattern]
			};
			dictionary["NumberDecimalDigits"] = cultureInfo.NumberFormat.NumberDecimalDigits;
			dictionary["NumberGroupSeparator"] = numberFormat.NumberGroupSeparator;
			dictionary["NumberDecimalSeparator"] = numberFormat.NumberDecimalSeparator;
			dictionary["NumberGroupSizes"] = numberFormat.NumberGroupSizes;
			dictionary["PercentPattern"] = new string[]
			{
				Html5GlobalizationInfo.percentNegativePatterns[cultureInfo.NumberFormat.PercentNegativePattern],
				Html5GlobalizationInfo.percentPositivePatterns[cultureInfo.NumberFormat.PercentPositivePattern]
			};
			dictionary["PercentDecimalDigits"] = cultureInfo.NumberFormat.PercentDecimalDigits;
			dictionary["PercentGroupSeparator"] = numberFormat.PercentGroupSeparator;
			dictionary["PercentDecimalSeparator"] = numberFormat.PercentDecimalSeparator;
			dictionary["PercentGroupSizes"] = numberFormat.PercentGroupSizes;
			dictionary["PercentSymbol"] = numberFormat.PercentSymbol;
			dictionary["CurrencyPattern"] = new string[]
			{
				Html5GlobalizationInfo.currencyNegativePatterns[cultureInfo.NumberFormat.CurrencyNegativePattern],
				Html5GlobalizationInfo.currencyPositivePatterns[cultureInfo.NumberFormat.CurrencyPositivePattern]
			};
			dictionary["CurrencyDecimalDigits"] = cultureInfo.NumberFormat.CurrencyDecimalDigits;
			dictionary["CurrencyGroupSeparator"] = numberFormat.CurrencyGroupSeparator;
			dictionary["CurrencyDecimalSeparator"] = numberFormat.CurrencyDecimalSeparator;
			dictionary["CurrencyGroupSizes"] = numberFormat.CurrencyGroupSizes;
			dictionary["CurrencySymbol"] = numberFormat.CurrencySymbol;
			dictionary["DayNames"] = dateTimeFormat.DayNames;
			dictionary["AbbreviatedDayNames"] = dateTimeFormat.AbbreviatedDayNames;
			dictionary["ShortestDayNames"] = dateTimeFormat.ShortestDayNames;
			dictionary["MonthNames"] = dateTimeFormat.MonthNames;
			dictionary["AbbreviatedMonthNames"] = dateTimeFormat.AbbreviatedMonthNames;
			dictionary["d"] = dateTimeFormat.ShortDatePattern;
			dictionary["D"] = dateTimeFormat.LongDatePattern;
			dictionary["F"] = dateTimeFormat.FullDateTimePattern;
			dictionary["g"] = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.ShortTimePattern;
			dictionary["G"] = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern;
			dictionary["m"] = dateTimeFormat.MonthDayPattern;
			dictionary["M"] = dateTimeFormat.MonthDayPattern;
			dictionary["s"] = dateTimeFormat.SortableDateTimePattern;
			dictionary["t"] = dateTimeFormat.ShortTimePattern;
			dictionary["T"] = dateTimeFormat.LongTimePattern;
			dictionary["u"] = dateTimeFormat.UniversalSortableDateTimePattern;
			dictionary["y"] = dateTimeFormat.YearMonthPattern;
			dictionary["Y"] = dateTimeFormat.YearMonthPattern;
			string amdesignator = dateTimeFormat.AMDesignator;
			string pmdesignator = dateTimeFormat.PMDesignator;
			dictionary["AM"] = (string.IsNullOrEmpty(amdesignator) ? new string[]
			{
				amdesignator
			} : new string[]
			{
				amdesignator,
				amdesignator.ToLower(cultureInfo),
				amdesignator.ToUpper(cultureInfo)
			});
			dictionary["PM"] = (string.IsNullOrEmpty(pmdesignator) ? new string[]
			{
				amdesignator
			} : new string[]
			{
				pmdesignator,
				pmdesignator.ToLower(cultureInfo),
				pmdesignator.ToUpper(cultureInfo)
			});
			dictionary["DateSeparator"] = dateTimeFormat.DateSeparator;
			dictionary["TimeSeparator"] = dateTimeFormat.TimeSeparator;
			dictionary["FirstDayOfWeek"] = (int)dateTimeFormat.FirstDayOfWeek;
			return dictionary;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00031C00 File Offset: 0x0002FE00
		public static string Format(this CultureInfo cultureInfo)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			IDictionary<string, object> dictionary = Html5GlobalizationInfo.BuildFlatDictionary(cultureInfo);
			string text = Html5GlobalizationInfo.culturePattern;
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				object value = keyValuePair.Value;
				text = text.Replace("{{" + key + "}}", (value is Array) ? javaScriptSerializer.Serialize(value) : Convert.ToString(value, cultureInfo));
			}
			Regex regex = new Regex(" *\r\n\\ *");
			text = regex.Replace(text, "");
			return text;
		}

		// Token: 0x04000354 RID: 852
		private static string[] numberNegativePatterns = new string[]
		{
			"(n)",
			"-n",
			"- n",
			"n-",
			"n -"
		};

		// Token: 0x04000355 RID: 853
		private static string[] currencyPositivePatterns = new string[]
		{
			"$n",
			"n$",
			"$ n",
			"n $"
		};

		// Token: 0x04000356 RID: 854
		private static string[] currencyNegativePatterns = new string[]
		{
			"($n)",
			"-$n",
			"$-n",
			"$n-",
			"(n$)",
			"-n$",
			"n-$",
			"n$-",
			"-n $",
			"-$ n",
			"n $-",
			"$ n-",
			"$ -n",
			"n- $",
			"($ n)",
			"(n $)"
		};

		// Token: 0x04000357 RID: 855
		private static string[] percentPositivePatterns = new string[]
		{
			"n %",
			"n%",
			"%n",
			"% n"
		};

		// Token: 0x04000358 RID: 856
		private static string[] percentNegativePatterns = new string[]
		{
			"-n %",
			"-n%",
			"-%n",
			"%-n",
			"%n-",
			"n-%",
			"n%-",
			"-% n",
			"n %-",
			"% n-",
			"% -n",
			"n- %"
		};

		// Token: 0x04000359 RID: 857
		private static readonly string culturePattern = "(function( window, undefined ) {\r\n    kendo.cultures[\"{{Name}}\"] = {\r\n        name: \"{{Name}}\",\r\n        numberFormat: {\r\n            pattern: {{NumberPattern}},\r\n            decimals: {{NumberDecimalDigits}},\r\n            \",\": \"{{NumberGroupSeparator}}\",\r\n            \".\": \"{{NumberDecimalSeparator}}\",\r\n            groupSize: {{NumberGroupSizes}},\r\n            percent: {\r\n                pattern: {{PercentPattern}},\r\n                decimals: {{PercentDecimalDigits}},\r\n                \",\": \"{{PercentGroupSeparator}}\",\r\n                \".\": \"{{PercentDecimalSeparator}}\",\r\n                groupSize: {{PercentGroupSizes}},\r\n                symbol: \"{{PercentSymbol}}\"\r\n            },\r\n            currency: {\r\n                pattern: {{CurrencyPattern}},\r\n                decimals: {{CurrencyDecimalDigits}},\r\n                \",\": \"{{CurrencyGroupSeparator}}\",\r\n                \".\": \"{{CurrencyDecimalSeparator}}\",\r\n                groupSize: {{CurrencyGroupSizes}},\r\n                symbol: \"{{CurrencySymbol}}\"\r\n            }\r\n        },\r\n        calendars: {\r\n            standard: {\r\n                days: {\r\n                    names: {{DayNames}},\r\n                    namesAbbr: {{AbbreviatedDayNames}},\r\n                    namesShort: {{ShortestDayNames}}\r\n                },\r\n                months: {\r\n                    names: {{MonthNames}},\r\n                    namesAbbr: {{AbbreviatedMonthNames}}\r\n                },\r\n                AM: {{AM}},\r\n                PM: {{PM}},\r\n                patterns: {\r\n                    d: \"{{d}}\",\r\n                    D: \"{{D}}\",\r\n                    F: \"{{F}}\",\r\n                    g: \"{{g}}\",\r\n                    G: \"{{G}}\",\r\n                    m: \"{{m}}\",\r\n                    M: \"{{M}}\",\r\n                    s: \"{{s}}\",\r\n                    t: \"{{t}}\",\r\n                    T: \"{{T}}\",\r\n                    u: \"{{u}}\",\r\n                    y: \"{{y}}\",\r\n                    Y: \"{{Y}}\"\r\n                },\r\n                \"/\": \"{{DateSeparator}}\",\r\n                \":\": \"{{TimeSeparator}}\",\r\n                firstDay: {{FirstDayOfWeek}}\r\n            }\r\n        }\r\n    };\r\n    kendo.culture(\"{{Name}}\");\r\n})(this);";
	}
}

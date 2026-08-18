using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012E2 RID: 4834
	[TypeConverter(typeof(RecurrenceRuleConverter))]
	[Serializable]
	public abstract class RecurrenceRule : ISerializable, IEquatable<RecurrenceRule>
	{
		// Token: 0x0600CAEE RID: 51950 RVA: 0x002D574C File Offset: 0x002D394C
		protected RecurrenceRule(SerializationInfo info, StreamingContext context)
		{
			RecurrenceRule recurrenceRule;
			if (!RecurrenceRule.TryParse(info.GetString("RRULE"), out recurrenceRule))
			{
				throw new InvalidOperationException("Deserialization failed. Parsing not successfull.");
			}
			this.rulePattern = recurrenceRule.Pattern;
			this.ruleRange = recurrenceRule.Range;
			this._exceptions = recurrenceRule.Exceptions;
		}

		// Token: 0x0600CAEF RID: 51951 RVA: 0x002D57E4 File Offset: 0x002D39E4
		protected RecurrenceRule()
		{
		}

		// Token: 0x17004184 RID: 16772
		// (get) Token: 0x0600CAF0 RID: 51952 RVA: 0x002D5839 File Offset: 0x002D3A39
		public RecurrenceRange Range
		{
			get
			{
				return this.ruleRange;
			}
		}

		// Token: 0x17004185 RID: 16773
		// (get) Token: 0x0600CAF1 RID: 51953 RVA: 0x002D5841 File Offset: 0x002D3A41
		public RecurrencePattern Pattern
		{
			get
			{
				return this.rulePattern;
			}
		}

		// Token: 0x17004186 RID: 16774
		// (get) Token: 0x0600CAF2 RID: 51954 RVA: 0x002D5A10 File Offset: 0x002D3C10
		public virtual IEnumerable<DateTime> Occurrences
		{
			get
			{
				int candidateIndex = 0;
				int occurrencesCount = 0;
				while (occurrencesCount < this.ruleRange.MaxOccurrences)
				{
					int index;
					candidateIndex = (index = candidateIndex) + 1;
					DateTime nextStart = this.GetOccurrenceStart(index);
					if (this.ruleRange.RecursUntil < nextStart || this._effectiveEnd < nextStart || this.MaximumCandidates < candidateIndex)
					{
						break;
					}
					if (this.MatchAdvancedPattern(nextStart))
					{
						occurrencesCount++;
						if (!(nextStart < this._effectiveStart) && !this._exceptions.Contains(nextStart))
						{
							yield return nextStart;
						}
					}
				}
				yield break;
			}
		}

		// Token: 0x17004187 RID: 16775
		// (get) Token: 0x0600CAF3 RID: 51955 RVA: 0x002D5A2D File Offset: 0x002D3C2D
		public bool HasOccurrences
		{
			get
			{
				return this.Occurrences.GetEnumerator().MoveNext();
			}
		}

		// Token: 0x17004188 RID: 16776
		// (get) Token: 0x0600CAF4 RID: 51956 RVA: 0x002D5A3F File Offset: 0x002D3C3F
		// (set) Token: 0x0600CAF5 RID: 51957 RVA: 0x002D5A47 File Offset: 0x002D3C47
		public virtual IList<DateTime> Exceptions
		{
			get
			{
				return this._exceptions;
			}
			set
			{
				this._exceptions = value;
			}
		}

		// Token: 0x17004189 RID: 16777
		// (get) Token: 0x0600CAF6 RID: 51958 RVA: 0x002D5A50 File Offset: 0x002D3C50
		public bool HasExceptions
		{
			get
			{
				return 0 < this._exceptions.Count;
			}
		}

		// Token: 0x1700418A RID: 16778
		// (get) Token: 0x0600CAF7 RID: 51959 RVA: 0x002D5A60 File Offset: 0x002D3C60
		// (set) Token: 0x0600CAF8 RID: 51960 RVA: 0x002D5A68 File Offset: 0x002D3C68
		public int MaximumCandidates
		{
			get
			{
				return this._maximumCandidates;
			}
			set
			{
				this._maximumCandidates = value;
			}
		}

		// Token: 0x1700418B RID: 16779
		// (get) Token: 0x0600CAF9 RID: 51961 RVA: 0x002D5A71 File Offset: 0x002D3C71
		protected DateTime EffectiveStart
		{
			get
			{
				if (this._effectiveStart < this.ruleRange.Start)
				{
					return this.ruleRange.Start;
				}
				return this._effectiveStart;
			}
		}

		// Token: 0x0600CAFA RID: 51962 RVA: 0x002D5AA0 File Offset: 0x002D3CA0
		public static RecurrenceRule FromPatternAndRange(RecurrencePattern pattern, RecurrenceRange range)
		{
			if (pattern == null || range == null)
			{
				return null;
			}
			switch (pattern.Frequency)
			{
			case RecurrenceFrequency.Hourly:
				return new HourlyRecurrenceRule(pattern.Interval, range);
			case RecurrenceFrequency.Daily:
				if (pattern.DaysOfWeekMask != RecurrenceDay.EveryDay)
				{
					return new DailyRecurrenceRule(pattern.DaysOfWeekMask, range);
				}
				if (0 < pattern.Interval)
				{
					return new DailyRecurrenceRule(pattern.Interval, range);
				}
				break;
			case RecurrenceFrequency.Weekly:
				if (0 < pattern.Interval && pattern.DaysOfWeekMask != RecurrenceDay.None)
				{
					return new WeeklyRecurrenceRule(pattern.Interval, pattern.DaysOfWeekMask, range, pattern.FirstDayOfWeek);
				}
				break;
			case RecurrenceFrequency.Monthly:
				if (0 < pattern.DayOfMonth && 0 < pattern.Interval)
				{
					return new MonthlyRecurrenceRule(pattern.DayOfMonth, pattern.Interval, range);
				}
				if (pattern.DayOrdinal != 0 && pattern.DaysOfWeekMask != RecurrenceDay.None && 0 < pattern.Interval)
				{
					return new MonthlyRecurrenceRule(pattern.DayOrdinal, pattern.DaysOfWeekMask, pattern.Interval, range);
				}
				break;
			case RecurrenceFrequency.Yearly:
				if (pattern.Month != RecurrenceMonth.None && 0 < pattern.DayOfMonth)
				{
					return new YearlyRecurrenceRule(pattern.Month, pattern.DayOfMonth, range, pattern.Interval);
				}
				if (pattern.DayOrdinal != 0 && pattern.Month != RecurrenceMonth.None && pattern.DaysOfWeekMask != RecurrenceDay.None)
				{
					return new YearlyRecurrenceRule(pattern.DayOrdinal, pattern.Month, pattern.DaysOfWeekMask, range, pattern.Interval);
				}
				if (pattern.Month != RecurrenceMonth.None)
				{
					return new YearlyRecurrenceRule(pattern.Month, range.Start.Day, range, pattern.Interval);
				}
				break;
			}
			return null;
		}

		// Token: 0x0600CAFB RID: 51963 RVA: 0x002D5C44 File Offset: 0x002D3E44
		public static bool TryParse(string input, out RecurrenceRule rrule)
		{
			if (string.IsNullOrEmpty(input))
			{
				rrule = null;
				return false;
			}
			RecurrenceRange recurrenceRange = new RecurrenceRange();
			RecurrencePattern recurrencePattern = new RecurrencePattern();
			List<DateTime> list = new List<DateTime>();
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			bool result = false;
			try
			{
				input = input.Trim();
				foreach (string text in input.Split(new char[]
				{
					'\n'
				}))
				{
					string text2 = text.Trim();
					Match match = Regex.Match(text2, "^(DTSTART|DTEND):(.*)$", RegexOptions.IgnoreCase);
					DateTime value;
					if (match.Success && RecurrenceRule.TryParseDateTime(match.Groups[2].Value, out value))
					{
						if (match.Groups[1].Value == "DTSTART")
						{
							dateTime = new DateTime?(value);
						}
						else
						{
							dateTime2 = new DateTime?(value);
						}
					}
					RecurrenceRule.ParseRRule(text2, recurrenceRange, recurrencePattern);
					RecurrenceRule.ParseExceptions(text2, list);
				}
				if (dateTime != null && dateTime2 != null)
				{
					recurrenceRange.Start = dateTime.Value;
					recurrenceRange.EventDuration = dateTime2.Value.Subtract(dateTime.Value);
					rrule = RecurrenceRule.FromPatternAndRange(recurrencePattern, recurrenceRange);
					rrule.Exceptions = list;
					result = (rrule != null);
				}
				else
				{
					rrule = null;
				}
			}
			catch (Exception)
			{
				result = false;
				rrule = null;
			}
			return result;
		}

		// Token: 0x0600CAFC RID: 51964 RVA: 0x002D5DC8 File Offset: 0x002D3FC8
		public static RecurrenceRule TryParse(string input)
		{
			RecurrenceRule result = null;
			RecurrenceRule.TryParse(input, out result);
			return result;
		}

		// Token: 0x0600CAFD RID: 51965 RVA: 0x002D5DE1 File Offset: 0x002D3FE1
		public static bool TryParse(string input, string timeZoneID, out RecurrenceRule rrule)
		{
			if (RecurrenceRule.TryParse(input, out rrule))
			{
				RecurrenceRule.ConvertRecurrenceRuleToTimeZone(timeZoneID, rrule);
			}
			return rrule != null;
		}

		// Token: 0x0600CAFE RID: 51966 RVA: 0x002D5DFC File Offset: 0x002D3FFC
		private static void ConvertRecurrenceRuleToTimeZone(string timeZoneID, RecurrenceRule rrule)
		{
			rrule.Range.Start = TimeZoneInfoProvider.UtcToLocal(rrule.Range.Start, TimeZoneInfoProvider.GetTimeZoneModelById(timeZoneID));
			if (rrule.Range.RecursUntil < DateTime.MaxValue)
			{
				rrule.Range.RecursUntil = TimeZoneInfoProvider.UtcToLocal(rrule.Range.RecursUntil, TimeZoneInfoProvider.GetTimeZoneModelById(timeZoneID));
			}
			for (int i = 0; i < rrule.Exceptions.Count; i++)
			{
				rrule.Exceptions[i] = TimeZoneInfoProvider.UtcToLocal(rrule.Exceptions[i], TimeZoneInfoProvider.GetTimeZoneModelById(timeZoneID));
			}
		}

		// Token: 0x0600CAFF RID: 51967 RVA: 0x002D5E9C File Offset: 0x002D409C
		private static void ParseRRule(string line, RecurrenceRange parsedRange, RecurrencePattern parsedPattern)
		{
			Match match = Regex.Match(line, "^(RRULE:)(.*)$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				string value = match.Groups[2].Value;
				Match match2 = Regex.Match(value, "FREQ=(HOURLY|DAILY|WEEKLY|MONTHLY|YEARLY)", RegexOptions.IgnoreCase);
				if (match2.Success)
				{
					parsedPattern.Frequency = (RecurrenceFrequency)Enum.Parse(typeof(RecurrenceFrequency), match2.Groups[1].Value, true);
				}
				Match match3 = Regex.Match(value, "COUNT=(\\d{1,4})", RegexOptions.IgnoreCase);
				if (match3.Success)
				{
					parsedRange.MaxOccurrences = int.Parse(match3.Groups[1].Value);
				}
				Match match4 = Regex.Match(value, "UNTIL=([\\w\\d]*)", RegexOptions.IgnoreCase);
				DateTime recursUntil;
				if (match4.Success && RecurrenceRule.TryParseDateTime(match4.Groups[1].Value, out recursUntil))
				{
					parsedRange.RecursUntil = recursUntil;
				}
				Match match5 = Regex.Match(value, "INTERVAL=(\\d{1,})", RegexOptions.IgnoreCase);
				if (match5.Success)
				{
					parsedPattern.Interval = int.Parse(match5.Groups[1].Value);
				}
				else
				{
					parsedPattern.Interval = 1;
				}
				Match match6 = Regex.Match(value, "BYSETPOS=(-?\\d{1})", RegexOptions.IgnoreCase);
				if (match6.Success)
				{
					parsedPattern.DayOrdinal = int.Parse(match6.Groups[1].Value);
				}
				Match match7 = Regex.Match(value, "BYMONTHDAY=(\\d{1,2})", RegexOptions.IgnoreCase);
				if (match7.Success)
				{
					parsedPattern.DayOfMonth = int.Parse(match7.Groups[1].Value);
				}
				Match match8 = Regex.Match(value, "BYDAY=(-?\\d{1})?([\\w,]*)", RegexOptions.IgnoreCase);
				if (match8.Success)
				{
					if (!string.IsNullOrEmpty(match8.Groups[1].Value))
					{
						parsedPattern.DayOrdinal = int.Parse(match8.Groups[1].Value);
					}
					RecurrenceDay daysOfWeekMask;
					if (RecurrenceRule.TryParseDayOfWeekMask(match8.Groups[2].Value, out daysOfWeekMask))
					{
						parsedPattern.DaysOfWeekMask = daysOfWeekMask;
					}
				}
				Match match9 = Regex.Match(value, "BYMONTH=(\\d{1,2})", RegexOptions.IgnoreCase);
				if (match9.Success)
				{
					parsedPattern.Month = (RecurrenceMonth)Enum.Parse(typeof(RecurrenceMonth), match9.Groups[1].Value, true);
				}
				Match match10 = Regex.Match(value, "WKST=([\\w,]*)", RegexOptions.IgnoreCase);
				if (match10.Success)
				{
					parsedPattern.FirstDayOfWeek = RecurrenceRule.ParseDayOfWeek(match10.Groups[1].Value);
				}
			}
		}

		// Token: 0x0600CB00 RID: 51968 RVA: 0x002D610C File Offset: 0x002D430C
		private static void ParseExceptions(string line, ICollection<DateTime> parsedExceptions)
		{
			Match match = Regex.Match(line, "^(EXDATE):(.*)$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				foreach (string input in match.Groups[2].Value.Split(new char[]
				{
					','
				}))
				{
					DateTime item;
					if (RecurrenceRule.TryParseDateTime(input, out item))
					{
						parsedExceptions.Add(item);
					}
				}
			}
		}

		// Token: 0x0600CB01 RID: 51969 RVA: 0x002D617D File Offset: 0x002D437D
		protected static bool IsValidValue<T>(T value, T minValue, T maxValue) where T : IComparable
		{
			return value.CompareTo(minValue) >= 0 || value.CompareTo(maxValue) <= 0;
		}

		// Token: 0x0600CB02 RID: 51970 RVA: 0x002D61B0 File Offset: 0x002D43B0
		protected static void ValidateValue<T>(string name, T value, T minValue, T maxValue) where T : IComparable
		{
			if (value.CompareTo(minValue) <= 0 || value.CompareTo(maxValue) >= 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} is out of range. Actual value is {1}, allowed range is [{2} - {3}]", new object[]
				{
					name,
					value,
					minValue,
					maxValue
				}));
			}
		}

		// Token: 0x0600CB03 RID: 51971 RVA: 0x002D6220 File Offset: 0x002D4420
		private static bool TryParseDateTime(string input, out DateTime date)
		{
			Match match = Regex.Match(input, "^(\\d{4})(\\d{2})(\\d{2})T(\\d{2})(\\d{2})(\\d{2})(Z)(.*)$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				int num = int.Parse(match.Groups[1].Value);
				int num2 = int.Parse(match.Groups[2].Value);
				int num3 = int.Parse(match.Groups[3].Value);
				int num4 = int.Parse(match.Groups[4].Value);
				int num5 = int.Parse(match.Groups[5].Value);
				int num6 = int.Parse(match.Groups[6].Value);
				bool flag = true;
				flag &= RecurrenceRule.IsValidValue<int>(num, 1900, 2900);
				flag &= RecurrenceRule.IsValidValue<int>(num2, 1, 12);
				flag &= RecurrenceRule.IsValidValue<int>(num3, 1, 31);
				flag &= RecurrenceRule.IsValidValue<int>(num4, 0, 23);
				flag &= RecurrenceRule.IsValidValue<int>(num5, 0, 59);
				flag &= RecurrenceRule.IsValidValue<int>(num6, 0, 59);
				if (flag)
				{
					date = new DateTime(num, num2, num3, num4, num5, num6, 0, DateTimeKind.Utc);
					return true;
				}
			}
			date = DateTime.MinValue;
			return false;
		}

		// Token: 0x0600CB04 RID: 51972 RVA: 0x002D635C File Offset: 0x002D455C
		private static bool TryParseDayOfWeekMask(string input, out RecurrenceDay mask)
		{
			Dictionary<string, RecurrenceDay> dictionary = new Dictionary<string, RecurrenceDay>();
			dictionary.Add("MO", RecurrenceDay.Monday);
			dictionary.Add("TU", RecurrenceDay.Tuesday);
			dictionary.Add("WE", RecurrenceDay.Wednesday);
			dictionary.Add("TH", RecurrenceDay.Thursday);
			dictionary.Add("FR", RecurrenceDay.Friday);
			dictionary.Add("SA", RecurrenceDay.Saturday);
			dictionary.Add("SU", RecurrenceDay.Sunday);
			mask = RecurrenceDay.None;
			foreach (string key in input.Split(new char[]
			{
				','
			}))
			{
				if (!dictionary.ContainsKey(key))
				{
					return false;
				}
				mask |= dictionary[key];
			}
			return true;
		}

		// Token: 0x0600CB05 RID: 51973 RVA: 0x002D6418 File Offset: 0x002D4618
		private static DayOfWeek ParseDayOfWeek(string input)
		{
			Dictionary<string, DayOfWeek> dictionary = new Dictionary<string, DayOfWeek>();
			dictionary.Add("MO", DayOfWeek.Monday);
			dictionary.Add("TU", DayOfWeek.Tuesday);
			dictionary.Add("WE", DayOfWeek.Wednesday);
			dictionary.Add("TH", DayOfWeek.Thursday);
			dictionary.Add("FR", DayOfWeek.Friday);
			dictionary.Add("SA", DayOfWeek.Saturday);
			dictionary.Add("SU", DayOfWeek.Sunday);
			if (dictionary.ContainsKey(input))
			{
				return dictionary[input];
			}
			return DayOfWeek.Sunday;
		}

		// Token: 0x0600CB06 RID: 51974 RVA: 0x002D6491 File Offset: 0x002D4691
		public void SetEffectiveRange(DateTime start, DateTime end)
		{
			if (end < start)
			{
				throw new ArgumentException("The end date is before the start date.");
			}
			this._effectiveStart = DateHelper.AssumeUtc(start);
			this._effectiveEnd = DateHelper.AssumeUtc(end);
		}

		// Token: 0x0600CB07 RID: 51975 RVA: 0x002D64BF File Offset: 0x002D46BF
		public void ClearEffectiveRange()
		{
			this._effectiveStart = DateTime.MinValue;
			this._effectiveEnd = DateTime.MaxValue;
		}

		// Token: 0x0600CB08 RID: 51976 RVA: 0x002D64D8 File Offset: 0x002D46D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DateTime date = this.ruleRange.Start.Add(this.ruleRange.EventDuration);
			stringBuilder.AppendFormat("DTSTART:{0}\r\n", RecurrenceRule.FormatDateTime(this.ruleRange.Start, true, true));
			stringBuilder.AppendFormat("DTEND:{0}\r\n", RecurrenceRule.FormatDateTime(date, true, true));
			stringBuilder.AppendFormat("RRULE:{0}\r\n", this.FormatRRule());
			stringBuilder.Append(this.FormatExceptions());
			return stringBuilder.ToString();
		}

		// Token: 0x0600CB09 RID: 51977 RVA: 0x002D6564 File Offset: 0x002D4764
		public string ToString(TimeZoneInfoProvider provider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DateTime utc = this.ruleRange.Start.Add(this.ruleRange.EventDuration);
			stringBuilder.Append("DTSTART;");
			stringBuilder.AppendFormat("TZID=\"{0}\":{1}", provider.OperationTimeZone.TimeZoneId, RecurrenceRule.FormatDateTime(provider.UtcToLocal(this.ruleRange.Start), true, false));
			stringBuilder.Append("\r\n");
			stringBuilder.Append("DTEND;");
			stringBuilder.AppendFormat("TZID=\"{0}\":{1}", provider.OperationTimeZone.TimeZoneId, RecurrenceRule.FormatDateTime(provider.UtcToLocal(utc), true, false));
			stringBuilder.Append("\r\n");
			stringBuilder.AppendFormat("RRULE:{0}\r\n", this.FormatRRule());
			stringBuilder.Append(this.FormatExceptions());
			return stringBuilder.ToString();
		}

		// Token: 0x0600CB0A RID: 51978 RVA: 0x002D663F File Offset: 0x002D483F
		public override int GetHashCode()
		{
			return this.Pattern.GetHashCode() ^ this.Range.GetHashCode();
		}

		// Token: 0x0600CB0B RID: 51979 RVA: 0x002D6658 File Offset: 0x002D4858
		public override bool Equals(object obj)
		{
			RecurrenceRule other = obj as RecurrenceRule;
			return obj != null && this.Equals(other);
		}

		// Token: 0x0600CB0C RID: 51980 RVA: 0x002D6678 File Offset: 0x002D4878
		public bool Equals(RecurrenceRule other)
		{
			return !(other == null) && this.Pattern == other.Pattern && this.Range == other.Range;
		}

		// Token: 0x0600CB0D RID: 51981 RVA: 0x002D66AB File Offset: 0x002D48AB
		public static bool operator ==(RecurrenceRule o1, RecurrenceRule o2)
		{
			if (o1 != null)
			{
				return o1.Equals(o2);
			}
			return o2 == null;
		}

		// Token: 0x0600CB0E RID: 51982 RVA: 0x002D66BC File Offset: 0x002D48BC
		public static bool operator !=(RecurrenceRule o1, RecurrenceRule o2)
		{
			if (o1 != null)
			{
				return !o1.Equals(o2);
			}
			return o2 != null;
		}

		// Token: 0x0600CB0F RID: 51983 RVA: 0x002D66D3 File Offset: 0x002D48D3
		protected virtual DateTime GetOccurrenceStart(int index)
		{
			throw new InvalidOperationException("Must override GetOccurrenceStart(int index).");
		}

		// Token: 0x0600CB10 RID: 51984 RVA: 0x002D66DF File Offset: 0x002D48DF
		protected virtual bool MatchAdvancedPattern(DateTime start)
		{
			throw new InvalidOperationException("Must override MatchAdvancedPattern(DateTime start).");
		}

		// Token: 0x0600CB11 RID: 51985 RVA: 0x002D66EC File Offset: 0x002D48EC
		protected bool MatchDayOfWeekMask(DateTime start)
		{
			RecurrenceDay recurrenceDay = (RecurrenceDay)Enum.Parse(typeof(RecurrenceDay), start.DayOfWeek.ToString());
			return (recurrenceDay & this.rulePattern.DaysOfWeekMask) == recurrenceDay;
		}

		// Token: 0x0600CB12 RID: 51986 RVA: 0x002D672F File Offset: 0x002D492F
		protected bool MatchDayOrdinal(DateTime date)
		{
			if (this.rulePattern.DayOrdinal == 0)
			{
				return true;
			}
			if (0 >= this.rulePattern.DayOrdinal)
			{
				return this.MatchDayOrdinalNegative(date);
			}
			return this.MatchDayOrdinalPositive(date);
		}

		// Token: 0x0600CB13 RID: 51987 RVA: 0x002D6760 File Offset: 0x002D4960
		private static string FormatDateTime(DateTime date, bool containsTime, bool utc = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DateTime dateTime = date.ToUniversalTime();
			stringBuilder.AppendFormat("{0:00}{1:00}{2:00}", dateTime.Year, dateTime.Month, dateTime.Day);
			if (containsTime)
			{
				stringBuilder.AppendFormat("T{0:00}{1:00}{2:00}", dateTime.Hour, dateTime.Minute, dateTime.Second);
			}
			if (utc)
			{
				stringBuilder.Append("Z");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600CB14 RID: 51988 RVA: 0x002D67F4 File Offset: 0x002D49F4
		private string FormatRRule()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("FREQ={0};", this.rulePattern.Frequency.ToString().ToUpperInvariant());
			if (0 < this.ruleRange.MaxOccurrences && this.ruleRange.MaxOccurrences < 2147483647)
			{
				stringBuilder.AppendFormat("COUNT={0};", this.ruleRange.MaxOccurrences);
			}
			else if (DateTime.MinValue < this.ruleRange.RecursUntil && this.ruleRange.RecursUntil < DateTime.MaxValue)
			{
				stringBuilder.AppendFormat("UNTIL={0};", RecurrenceRule.FormatDateTime(this.ruleRange.RecursUntil, true, true));
			}
			if (0 < this.rulePattern.Interval)
			{
				stringBuilder.AppendFormat("INTERVAL={0};", this.rulePattern.Interval);
			}
			if (this.rulePattern.DayOrdinal != 0)
			{
				stringBuilder.AppendFormat("BYSETPOS={0};", this.rulePattern.DayOrdinal);
			}
			if (0 < this.rulePattern.DayOfMonth)
			{
				stringBuilder.AppendFormat("BYMONTHDAY={0};", this.rulePattern.DayOfMonth);
			}
			if (this.rulePattern.DaysOfWeekMask != RecurrenceDay.None)
			{
				Dictionary<RecurrenceDay, string> dictionary = new Dictionary<RecurrenceDay, string>();
				dictionary.Add(RecurrenceDay.Monday, "MO");
				dictionary.Add(RecurrenceDay.Tuesday, "TU");
				dictionary.Add(RecurrenceDay.Wednesday, "WE");
				dictionary.Add(RecurrenceDay.Thursday, "TH");
				dictionary.Add(RecurrenceDay.Friday, "FR");
				dictionary.Add(RecurrenceDay.Saturday, "SA");
				dictionary.Add(RecurrenceDay.Sunday, "SU");
				List<string> list = new List<string>();
				foreach (RecurrenceDay recurrenceDay in dictionary.Keys)
				{
					if ((this.rulePattern.DaysOfWeekMask & recurrenceDay) == recurrenceDay)
					{
						list.Add(dictionary[recurrenceDay]);
					}
				}
				stringBuilder.AppendFormat("BYDAY={0};", string.Join(",", list.ToArray()));
			}
			if (this.rulePattern.Month != RecurrenceMonth.None)
			{
				stringBuilder.AppendFormat("BYMONTH={0};", (int)this.rulePattern.Month);
			}
			if (this.rulePattern.FirstDayOfWeek != DayOfWeek.Sunday)
			{
				string arg = this.rulePattern.FirstDayOfWeek.ToString().ToUpperInvariant().Substring(0, 2);
				stringBuilder.AppendFormat("WKST={0};", arg);
			}
			string text = stringBuilder.ToString();
			if (text[text.Length - 1] == ';')
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		// Token: 0x0600CB15 RID: 51989 RVA: 0x002D6AB8 File Offset: 0x002D4CB8
		private string FormatExceptions()
		{
			if (this._exceptions.Count == 0)
			{
				return string.Empty;
			}
			string[] array = new string[this._exceptions.Count];
			for (int i = 0; i < this._exceptions.Count; i++)
			{
				array[i] = RecurrenceRule.FormatDateTime(this._exceptions[i], true, true);
			}
			return string.Format("EXDATE:{0}\r\n", string.Join(",", array));
		}

		// Token: 0x0600CB16 RID: 51990 RVA: 0x002D6B2C File Offset: 0x002D4D2C
		private bool MatchDayOrdinalPositive(DateTime date)
		{
			DateTime dateTime = DateHelper.GetFirstDayOfMonth(date);
			int num = 0;
			while (dateTime <= date)
			{
				if (this.MatchDayOfWeekMask(dateTime))
				{
					num++;
				}
				dateTime = dateTime.AddDays(1.0);
			}
			return num == this.rulePattern.DayOrdinal;
		}

		// Token: 0x0600CB17 RID: 51991 RVA: 0x002D6B7C File Offset: 0x002D4D7C
		private bool MatchDayOrdinalNegative(DateTime date)
		{
			DateTime dateTime = DateHelper.GetLastDayOfMonth(date).AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
			int num = 0;
			while (date < dateTime)
			{
				if (this.MatchDayOfWeekMask(dateTime))
				{
					num--;
				}
				dateTime = dateTime.AddDays(-1.0);
			}
			return num == this.rulePattern.DayOrdinal;
		}

		// Token: 0x0600CB18 RID: 51992 RVA: 0x002D6BFD File Offset: 0x002D4DFD
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("RRULE", this.ToString());
		}

		// Token: 0x04003544 RID: 13636
		private DateTime _effectiveStart = DateTime.MinValue;

		// Token: 0x04003545 RID: 13637
		private DateTime _effectiveEnd = DateTime.MaxValue;

		// Token: 0x04003546 RID: 13638
		private IList<DateTime> _exceptions = new List<DateTime>();

		// Token: 0x04003547 RID: 13639
		private int _maximumCandidates = 3000;

		// Token: 0x04003548 RID: 13640
		protected RecurrencePattern rulePattern = new RecurrencePattern();

		// Token: 0x04003549 RID: 13641
		protected RecurrenceRange ruleRange = new RecurrenceRange();

		// Token: 0x0400354A RID: 13642
		public static readonly RecurrenceRule Empty = new RecurrenceRule.EmptyRecurrenceRule();

		// Token: 0x020012E3 RID: 4835
		private class EmptyRecurrenceRule : RecurrenceRule
		{
		}
	}
}

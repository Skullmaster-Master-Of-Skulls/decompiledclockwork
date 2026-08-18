using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NCrontab
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public sealed class CrontabSchedule
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002C9E File Offset: 0x00000E9E
		public static CrontabSchedule Parse(string expression)
		{
			return CrontabSchedule.Parse(expression, null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static CrontabSchedule Parse(string expression, CrontabSchedule.ParseOptions options)
		{
			return CrontabSchedule.TryParse<CrontabSchedule>(expression, options, (CrontabSchedule v) => v, delegate(ExceptionProvider e)
			{
				throw e();
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002CFA File Offset: 0x00000EFA
		public static CrontabSchedule TryParse(string expression)
		{
			return CrontabSchedule.TryParse(expression, null);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D04 File Offset: 0x00000F04
		public static CrontabSchedule TryParse(string expression, CrontabSchedule.ParseOptions options)
		{
			return CrontabSchedule.TryParse<CrontabSchedule>(expression ?? string.Empty, options, (CrontabSchedule v) => v, (ExceptionProvider _) => null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D5F File Offset: 0x00000F5F
		public static T TryParse<T>(string expression, Func<CrontabSchedule, T> valueSelector, Func<ExceptionProvider, T> errorSelector)
		{
			return CrontabSchedule.TryParse<T>(expression ?? string.Empty, null, valueSelector, errorSelector);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D74 File Offset: 0x00000F74
		public static T TryParse<T>(string expression, CrontabSchedule.ParseOptions options, Func<CrontabSchedule, T> valueSelector, Func<ExceptionProvider, T> errorSelector)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			string[] array = expression.Split(StringSeparatorStock.Space, StringSplitOptions.RemoveEmptyEntries);
			bool includingSeconds = options != null && options.IncludingSeconds;
			int num = includingSeconds ? 6 : 5;
			if (array.Length < num || array.Length > num)
			{
				return errorSelector.Invoke(delegate()
				{
					string text = includingSeconds ? "6 components of a schedule in the sequence of seconds, minutes, hours, days, months, and days of week" : "5 components of a schedule in the sequence of minutes, hours, days, months, and days of week";
					return new CrontabException(string.Concat(new string[]
					{
						"'",
						expression,
						"' is an invalid crontab expression. It must contain ",
						text,
						"."
					}));
				});
			}
			CrontabField[] array2 = new CrontabField[6];
			int num2 = includingSeconds ? 0 : 1;
			for (int i = 0; i < array.Length; i++)
			{
				var <>f__AnonymousType = CrontabField.TryParse((CrontabFieldKind)(i + num2), array[i], (CrontabField v) => new
				{
					ErrorProvider = null,
					Value = v
				}, (ExceptionProvider e) => new
				{
					ErrorProvider = e,
					Value = null
				});
				if (<>f__AnonymousType.ErrorProvider != null)
				{
					return errorSelector.Invoke(<>f__AnonymousType.ErrorProvider);
				}
				array2[i + num2] = <>f__AnonymousType.Value;
			}
			return valueSelector.Invoke(new CrontabSchedule(array2[0], array2[1], array2[2], array2[3], array2[4], array2[5]));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002EA8 File Offset: 0x000010A8
		private CrontabSchedule(CrontabField seconds, CrontabField minutes, CrontabField hours, CrontabField days, CrontabField months, CrontabField daysOfWeek)
		{
			this._seconds = seconds;
			this._minutes = minutes;
			this._hours = hours;
			this._days = days;
			this._months = months;
			this._daysOfWeek = daysOfWeek;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002EDD File Offset: 0x000010DD
		public IEnumerable<DateTime> GetNextOccurrences(DateTime baseTime, DateTime endTime)
		{
			DateTime? occurrence = this.TryGetNextOccurrence(baseTime, endTime);
			while (occurrence != null && occurrence < endTime)
			{
				yield return occurrence.Value;
				occurrence = this.TryGetNextOccurrence(occurrence.Value, endTime);
			}
			occurrence = null;
			yield break;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002EFB File Offset: 0x000010FB
		public DateTime GetNextOccurrence(DateTime baseTime)
		{
			return this.GetNextOccurrence(baseTime, DateTime.MaxValue);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F0C File Offset: 0x0000110C
		public DateTime GetNextOccurrence(DateTime baseTime, DateTime endTime)
		{
			DateTime? dateTime = this.TryGetNextOccurrence(baseTime, endTime);
			if (dateTime == null)
			{
				return endTime;
			}
			return dateTime.GetValueOrDefault();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F34 File Offset: 0x00001134
		private DateTime? TryGetNextOccurrence(DateTime baseTime, DateTime endTime)
		{
			int year = baseTime.Year;
			int month = baseTime.Month;
			int day = baseTime.Day;
			int hour = baseTime.Hour;
			int minute = baseTime.Minute;
			int second = baseTime.Second;
			int year2 = endTime.Year;
			int month2 = endTime.Month;
			int day2 = endTime.Day;
			int num = year;
			int num2 = month;
			int num3 = day;
			int num4 = hour;
			int num5 = minute;
			int num6 = second + 1;
			CrontabField crontabField = this._seconds ?? CrontabSchedule.SecondZero;
			num6 = crontabField.Next(num6);
			if (num6 == -1)
			{
				num6 = crontabField.GetFirst();
				num5++;
			}
			num5 = this._minutes.Next(num5);
			if (num5 == -1)
			{
				num5 = this._minutes.GetFirst();
				num4++;
			}
			num4 = this._hours.Next(num4);
			if (num4 == -1)
			{
				num5 = this._minutes.GetFirst();
				num4 = this._hours.GetFirst();
				num3++;
			}
			else if (num4 > hour)
			{
				num5 = this._minutes.GetFirst();
			}
			num3 = this._days.Next(num3);
			for (;;)
			{
				if (num3 == -1)
				{
					num6 = crontabField.GetFirst();
					num5 = this._minutes.GetFirst();
					num4 = this._hours.GetFirst();
					num3 = this._days.GetFirst();
					num2++;
				}
				else if (num3 > day)
				{
					num6 = crontabField.GetFirst();
					num5 = this._minutes.GetFirst();
					num4 = this._hours.GetFirst();
				}
				num2 = this._months.Next(num2);
				if (num2 == -1)
				{
					num6 = crontabField.GetFirst();
					num5 = this._minutes.GetFirst();
					num4 = this._hours.GetFirst();
					num3 = this._days.GetFirst();
					num2 = this._months.GetFirst();
					num++;
				}
				else if (num2 > month)
				{
					num6 = crontabField.GetFirst();
					num5 = this._minutes.GetFirst();
					num4 = this._hours.GetFirst();
					num3 = this._days.GetFirst();
				}
				if (num > CrontabSchedule.Calendar.MaxSupportedDateTime.Year)
				{
					break;
				}
				bool flag = num3 != day || num2 != month || num != year;
				if (num3 <= 28 || !flag || num3 <= CrontabSchedule.Calendar.GetDaysInMonth(num, num2))
				{
					goto IL_277;
				}
				if (num >= year2 && num2 >= month2 && num3 >= day2)
				{
					goto Block_17;
				}
				num3 = -1;
			}
			return null;
			Block_17:
			return new DateTime?(endTime);
			IL_277:
			DateTime dateTime = new DateTime(num, num2, num3, num4, num5, num6, 0, baseTime.Kind);
			if (dateTime >= endTime)
			{
				return new DateTime?(endTime);
			}
			if (this._daysOfWeek.Contains((int)dateTime.DayOfWeek))
			{
				return new DateTime?(dateTime);
			}
			return this.TryGetNextOccurrence(new DateTime(num, num2, num3, 23, 59, 59, 0, baseTime.Kind), endTime);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003220 File Offset: 0x00001420
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			if (this._seconds != null)
			{
				this._seconds.Format(stringWriter, true);
				stringWriter.Write(' ');
			}
			this._minutes.Format(stringWriter, true);
			stringWriter.Write(' ');
			this._hours.Format(stringWriter, true);
			stringWriter.Write(' ');
			this._days.Format(stringWriter, true);
			stringWriter.Write(' ');
			this._months.Format(stringWriter, true);
			stringWriter.Write(' ');
			this._daysOfWeek.Format(stringWriter, true);
			return stringWriter.ToString();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000032BC File Offset: 0x000014BC
		private static Calendar Calendar
		{
			get
			{
				return CultureInfo.InvariantCulture.Calendar;
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly CrontabField _seconds;

		// Token: 0x0400001B RID: 27
		private readonly CrontabField _minutes;

		// Token: 0x0400001C RID: 28
		private readonly CrontabField _hours;

		// Token: 0x0400001D RID: 29
		private readonly CrontabField _days;

		// Token: 0x0400001E RID: 30
		private readonly CrontabField _months;

		// Token: 0x0400001F RID: 31
		private readonly CrontabField _daysOfWeek;

		// Token: 0x04000020 RID: 32
		private static readonly CrontabField SecondZero = CrontabField.Seconds("0");

		// Token: 0x02000013 RID: 19
		[Serializable]
		public sealed class ParseOptions
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000061 RID: 97 RVA: 0x000034A6 File Offset: 0x000016A6
			// (set) Token: 0x06000062 RID: 98 RVA: 0x000034AE File Offset: 0x000016AE
			public bool IncludingSeconds { get; set; }
		}
	}
}

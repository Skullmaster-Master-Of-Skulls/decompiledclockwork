using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Groups;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CC7 RID: 3271
	[DataContract]
	public sealed class DateTimeGroupDescription : PropertyGroupDescriptionBase
	{
		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x06007A5D RID: 31325 RVA: 0x001C0709 File Offset: 0x001BE909
		internal override bool TransformsData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002750 RID: 10064
		// (get) Token: 0x06007A5E RID: 31326 RVA: 0x001C070C File Offset: 0x001BE90C
		// (set) Token: 0x06007A5F RID: 31327 RVA: 0x001C0714 File Offset: 0x001BE914
		[DataMember]
		public DateTimeStep Step
		{
			get
			{
				return this.step;
			}
			set
			{
				if (this.step != value)
				{
					this.step = value;
					base.OnPropertyChanged("Step");
				}
			}
		}

		// Token: 0x17002751 RID: 10065
		// (get) Token: 0x06007A60 RID: 31328 RVA: 0x001C0731 File Offset: 0x001BE931
		// (set) Token: 0x06007A61 RID: 31329 RVA: 0x001C0738 File Offset: 0x001BE938
		private static int[] DaysCountInMonth { get; set; } = new int[]
		{
			31,
			29,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

		// Token: 0x17002752 RID: 10066
		// (get) Token: 0x06007A62 RID: 31330 RVA: 0x001C0740 File Offset: 0x001BE940
		private Calendar Calendar
		{
			get
			{
				return base.Culture.Calendar;
			}
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x001C0750 File Offset: 0x001BE950
		public override string GetUniqueName()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				base.PropertyName,
				this.Step
			});
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x001C0790 File Offset: 0x001BE990
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DateTimeOffset", Justification = "It is a common type.")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DateTime", Justification = "It is a common type.")]
		protected internal override object GroupNameFromItem(object item, int level)
		{
			object obj = base.GroupNameFromItem(item, level);
			if (obj == null)
			{
				return null;
			}
			if (obj is DateTime)
			{
				return this.GetGroupNameForValidDate((DateTime)obj);
			}
			if (obj is DateTimeOffset)
			{
				return this.GetGroupNameForValidDate(((DateTimeOffset)obj).DateTime);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Value {0} for Property {1} of item {2} should be DateTime or DateTimeOffset or null.", new object[]
			{
				obj,
				base.PropertyName,
				item
			}));
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x001C080C File Offset: 0x001BEA0C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "We need to check all the DateTimeSteps ")]
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			IEnumerable<object> result = null;
			switch (this.Step)
			{
			case DateTimeStep.Year:
				if (uniqueNames != null)
				{
					IEnumerable<YearGroup> enumerable = uniqueNames.OfType<YearGroup>();
					if (enumerable.Any<YearGroup>())
					{
						int num = int.MaxValue;
						int num2 = int.MinValue;
						foreach (YearGroup yearGroup in enumerable)
						{
							int year = yearGroup.Year;
							num = Math.Min(num, year);
							num2 = Math.Max(num2, year);
						}
						result = this.YearsInRange(num, num2);
					}
					else
					{
						result = uniqueNames;
					}
				}
				else
				{
					result = Enumerable.Empty<object>();
				}
				break;
			case DateTimeStep.Quarter:
				result = this.QuartersInYear();
				break;
			case DateTimeStep.Month:
			{
				QuarterGroup quarterGroup;
				if (DateTimeGroupDescription.FirstOrDefault<QuarterGroup>(parentGroupNames, out quarterGroup))
				{
					result = this.MonthsInQuarter(quarterGroup.Quarter);
				}
				else
				{
					result = this.MonthsInYear();
				}
				break;
			}
			case DateTimeStep.Week:
				if (uniqueNames != null)
				{
					IEnumerable<WeekGroup> source = uniqueNames.OfType<WeekGroup>();
					if (source.Any<WeekGroup>())
					{
						result = source.OfType<object>();
					}
					else
					{
						result = uniqueNames;
					}
				}
				else
				{
					result = Enumerable.Empty<object>();
				}
				break;
			case DateTimeStep.Day:
			{
				YearGroup yearGroup2;
				bool flag = DateTimeGroupDescription.FirstOrDefault<YearGroup>(parentGroupNames, out yearGroup2);
				QuarterGroup quarterGroup;
				bool flag2 = DateTimeGroupDescription.FirstOrDefault<QuarterGroup>(parentGroupNames, out quarterGroup);
				MonthGroup monthGroup;
				bool flag3 = DateTimeGroupDescription.FirstOrDefault<MonthGroup>(parentGroupNames, out monthGroup);
				if (flag3)
				{
					if (flag)
					{
						result = this.DaysInMonth(yearGroup2.Year, monthGroup.Month);
					}
					else
					{
						result = this.DaysInMonth(monthGroup.Month);
					}
				}
				else if (flag2)
				{
					if (flag)
					{
						result = this.DaysInQuarter(yearGroup2.Year, quarterGroup.Quarter);
					}
					else
					{
						result = this.DaysInQuarter(quarterGroup.Quarter);
					}
				}
				else if (flag)
				{
					result = this.DaysInYear(yearGroup2.Year);
				}
				else
				{
					result = this.DaysInLeapYear();
				}
				break;
			}
			case DateTimeStep.Hour:
				if (uniqueNames != null)
				{
					IEnumerable<HourGroup> source2 = uniqueNames.OfType<HourGroup>();
					if (source2.Any<HourGroup>())
					{
						result = source2.OfType<object>();
					}
					else
					{
						result = uniqueNames;
					}
				}
				else
				{
					result = Enumerable.Empty<object>();
				}
				break;
			case DateTimeStep.Minute:
				if (uniqueNames != null)
				{
					IEnumerable<MinuteGroup> source3 = uniqueNames.OfType<MinuteGroup>();
					if (source3.Any<MinuteGroup>())
					{
						result = source3.OfType<object>();
					}
					else
					{
						result = uniqueNames;
					}
				}
				else
				{
					result = Enumerable.Empty<object>();
				}
				break;
			case DateTimeStep.Second:
				if (uniqueNames != null)
				{
					IEnumerable<SecondGroup> source4 = uniqueNames.OfType<SecondGroup>();
					if (source4.Any<SecondGroup>())
					{
						result = source4.OfType<object>();
					}
					else
					{
						result = uniqueNames;
					}
				}
				else
				{
					result = Enumerable.Empty<object>();
				}
				break;
			}
			return result;
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x001C0A80 File Offset: 0x001BEC80
		protected override Cloneable CreateInstanceCore()
		{
			return new DateTimeGroupDescription();
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x001C0A88 File Offset: 0x001BEC88
		protected override void CloneOverride(Cloneable source)
		{
			DateTimeGroupDescription dateTimeGroupDescription = source as DateTimeGroupDescription;
			if (dateTimeGroupDescription != null)
			{
				this.step = dateTimeGroupDescription.step;
			}
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x001C0BAC File Offset: 0x001BEDAC
		private IEnumerable<object> MonthsInYear()
		{
			for (int i = 1; i <= 12; i++)
			{
				yield return new MonthGroup(i, base.Culture);
			}
			yield break;
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x001C0CE4 File Offset: 0x001BEEE4
		private IEnumerable<object> MonthsInQuarter(int quarter)
		{
			for (int month = quarter * 3 - 2; month <= quarter * 3; month++)
			{
				yield return new MonthGroup(month, base.Culture);
			}
			yield break;
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x001C0E04 File Offset: 0x001BF004
		private IEnumerable<object> QuartersInYear()
		{
			for (int i = 1; i <= 4; i++)
			{
				yield return new QuarterGroup(i, base.Culture);
			}
			yield break;
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x001C0E24 File Offset: 0x001BF024
		private static bool FirstOrDefault<T>(IEnumerable<object> source, out T value) where T : struct
		{
			if (source != null)
			{
				foreach (object obj in source)
				{
					if (obj is T)
					{
						value = (T)((object)obj);
						return true;
					}
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x001C0FAC File Offset: 0x001BF1AC
		private IEnumerable<object> YearsInRange(int firstYear, int lastYear)
		{
			for (int year = firstYear; year <= lastYear; year++)
			{
				yield return new YearGroup(year, base.Culture);
			}
			yield break;
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x001C0FD8 File Offset: 0x001BF1D8
		private IEnumerable<object> DaysInQuarter(int year, int quarter)
		{
			bool isLeapYear = this.Calendar.IsLeapYear(year);
			return this.DaysInQuarter(isLeapYear, quarter);
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x001C0FFA File Offset: 0x001BF1FA
		private IEnumerable<object> DaysInQuarter(int quarter)
		{
			return this.DaysInQuarter(true, quarter);
		}

		// Token: 0x06007A6F RID: 31343 RVA: 0x001C11A4 File Offset: 0x001BF3A4
		private IEnumerable<object> DaysInQuarter(bool isLeapYear, int quarter)
		{
			int startMonth = (quarter - 1) * 3 + 1;
			int endMonth = startMonth + 3;
			for (int month = startMonth; month < endMonth; month++)
			{
				int daysInMonth = DateTimeGroupDescription.DaysCountInMonth[month - 1];
				if (month == 2 && !isLeapYear)
				{
					daysInMonth = 28;
				}
				for (int day = 1; day <= daysInMonth; day++)
				{
					yield return new DayGroup(month, day, base.Culture);
				}
			}
			yield break;
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x001C1314 File Offset: 0x001BF514
		private IEnumerable<object> DaysInLeapYear()
		{
			for (int month = 1; month <= 12; month++)
			{
				int daysInMonth = DateTimeGroupDescription.DaysCountInMonth[month - 1];
				for (int day = 1; day <= daysInMonth; day++)
				{
					yield return new DayGroup(month, day, base.Culture);
				}
			}
			yield break;
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x001C148C File Offset: 0x001BF68C
		private IEnumerable<object> DaysInNonLeapYear()
		{
			for (int month = 1; month <= 12; month++)
			{
				int daysInMonth = DateTimeGroupDescription.DaysCountInMonth[month - 1];
				if (month == 2)
				{
					daysInMonth = 28;
				}
				for (int day = 1; day <= daysInMonth; day++)
				{
					yield return new DayGroup(month, day, base.Culture);
				}
			}
			yield break;
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x001C14A9 File Offset: 0x001BF6A9
		private IEnumerable<object> DaysInYear(int year)
		{
			if (this.Calendar.IsLeapYear(year))
			{
				return this.DaysInLeapYear();
			}
			return this.DaysInNonLeapYear();
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x001C14C6 File Offset: 0x001BF6C6
		private IEnumerable<object> DaysInMonth(int year, int month)
		{
			if (month == 2 && !this.Calendar.IsLeapYear(year))
			{
				return this.NonLeapDaysInDebruary();
			}
			return this.DaysInMonth(month);
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x001C15E8 File Offset: 0x001BF7E8
		private IEnumerable<object> NonLeapDaysInDebruary()
		{
			for (int day = 1; day <= 28; day++)
			{
				yield return new DayGroup(2, day, base.Culture);
			}
			yield break;
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x001C1730 File Offset: 0x001BF930
		private IEnumerable<object> DaysInMonth(int month)
		{
			int daysInMonth = DateTimeGroupDescription.DaysCountInMonth[month - 1];
			for (int day = 1; day <= daysInMonth; day++)
			{
				yield return new DayGroup(month, day, base.Culture);
			}
			yield break;
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x001C1754 File Offset: 0x001BF954
		private object GetGroupNameForValidDate(DateTime date)
		{
			switch (this.Step)
			{
			case DateTimeStep.Year:
				return new YearGroup(date.Year, base.Culture);
			case DateTimeStep.Quarter:
				return new QuarterGroup((date.Month - 1) / 3 + 1, base.Culture);
			case DateTimeStep.Month:
				return new MonthGroup(date.Month, base.Culture);
			case DateTimeStep.Week:
				return new WeekGroup(this.Calendar.GetWeekOfYear(date, base.Culture.DateTimeFormat.CalendarWeekRule, base.Culture.DateTimeFormat.FirstDayOfWeek), base.Culture);
			case DateTimeStep.Hour:
				return new HourGroup(date.Hour, base.Culture);
			case DateTimeStep.Minute:
				return new MinuteGroup(date.Minute, base.Culture);
			case DateTimeStep.Second:
				return new SecondGroup(date.Second, base.Culture);
			}
			return new DayGroup(date.Month, date.Day, base.Culture);
		}

		// Token: 0x04002181 RID: 8577
		private DateTimeStep step;
	}
}

using System;

namespace AutoComboBox
{
	// Token: 0x020000B9 RID: 185
	public class DateScope
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x0003872B File Offset: 0x0003772B
		public DateScope(int StartMonth, int StartDay, int EndMonth, int EndDay, int NumYearsBetween, string Description)
		{
			this.startMonth = StartMonth;
			this.startDay = StartDay;
			this.endMonth = EndMonth;
			this.endDay = EndDay;
			this.numYearsBetween = NumYearsBetween;
			this.description = Description;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00038764 File Offset: 0x00037764
		public DateTime GetStartDate(DateTime date)
		{
			DateTime result;
			if (date.Month >= this.startMonth && date.Day >= this.startDay && date.Month <= this.endMonth && date.Day <= this.endDay)
			{
				result = new DateTime(date.Year, this.startMonth, this.startDay);
			}
			else if (this.numYearsBetween < 1)
			{
				result = DateTime.MinValue;
			}
			else if (this.numYearsBetween > 1)
			{
				if (date.Month >= this.startMonth && date.Day >= this.startDay)
				{
					result = new DateTime(date.Year, this.startMonth, this.startDay);
				}
				else
				{
					result = new DateTime(date.Year - 1, this.startMonth, this.startDay);
				}
			}
			else if (this.startMonth <= this.endMonth && this.startDay <= this.endDay)
			{
				if (date.Month >= this.startMonth && date.Day >= this.startDay)
				{
					result = new DateTime(date.Year, this.startMonth, this.startDay);
				}
				else
				{
					result = new DateTime(date.Year - 1, this.startMonth, this.startDay);
				}
			}
			else if (date.Month <= this.endMonth && date.Day <= this.endDay)
			{
				result = new DateTime(date.Year - 1, this.startMonth, this.startDay);
			}
			else if (date.Month >= this.startMonth && date.Day >= this.startDay)
			{
				result = new DateTime(date.Year, this.startMonth, this.startDay);
			}
			else
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0003897C File Offset: 0x0003797C
		public override string ToString()
		{
			return this.description;
		}

		// Token: 0x04000569 RID: 1385
		public int startMonth;

		// Token: 0x0400056A RID: 1386
		public int startDay;

		// Token: 0x0400056B RID: 1387
		public int endMonth;

		// Token: 0x0400056C RID: 1388
		public int endDay;

		// Token: 0x0400056D RID: 1389
		public int numYearsBetween;

		// Token: 0x0400056E RID: 1390
		public string description;
	}
}

using System;

namespace ClockWorkAPI
{
	// Token: 0x0200007D RID: 125
	public class AvailabilityDayOfWeek
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0002408E File Offset: 0x0002308E
		public AvailabilityDayOfWeek(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
		{
			this.dayOfWeek = dayOfWeek;
			this.startTime = startTime;
			this.endTime = endTime;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000240B0 File Offset: 0x000230B0
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.dayOfWeek;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000240C8 File Offset: 0x000230C8
		public TimeSpan StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000240E0 File Offset: 0x000230E0
		public TimeSpan EndTime
		{
			get
			{
				return this.endTime;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000240F8 File Offset: 0x000230F8
		public string Caption
		{
			get
			{
				int year = DateTime.Now.Year;
				DateTime dateTime = new DateTime(year, 1, 1, this.startTime.Hours, this.startTime.Minutes, 0);
				DateTime dateTime2 = new DateTime(year, 1, 1, this.endTime.Hours, this.endTime.Minutes, 0);
				return string.Concat(new string[]
				{
					this.dayOfWeek.ToString(),
					" ",
					dateTime.ToString("h:mm tt"),
					" - ",
					dateTime2.ToString(),
					"h:mm tt"
				});
			}
		}

		// Token: 0x04000341 RID: 833
		private DayOfWeek dayOfWeek;

		// Token: 0x04000342 RID: 834
		private TimeSpan startTime;

		// Token: 0x04000343 RID: 835
		private TimeSpan endTime;
	}
}

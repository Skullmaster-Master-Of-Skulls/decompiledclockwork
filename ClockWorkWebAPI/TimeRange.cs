using System;

namespace ClockWorkWebAPI
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class TimeRange
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public int StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00010004 File Offset: 0x0000E204
		public int EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00010010 File Offset: 0x0000E210
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00010028 File Offset: 0x0000E228
		public int StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00010034 File Offset: 0x0000E234
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0001004C File Offset: 0x0000E24C
		public int EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00010056 File Offset: 0x0000E256
		public TimeRange(int startdate, int enddate, int starttime, int endtime)
		{
			this.startDate = startdate;
			this.endDate = enddate;
			this.startTime = starttime;
			this.endTime = endtime;
		}

		// Token: 0x04000140 RID: 320
		private int startDate;

		// Token: 0x04000141 RID: 321
		private int endDate;

		// Token: 0x04000142 RID: 322
		private int startTime;

		// Token: 0x04000143 RID: 323
		private int endTime;
	}
}

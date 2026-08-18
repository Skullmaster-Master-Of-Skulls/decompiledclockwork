using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A44 RID: 2628
	internal struct SystemTime
	{
		// Token: 0x06006810 RID: 26640 RVA: 0x0018474C File Offset: 0x0018294C
		public SystemTime(DateTime date)
		{
			this.wYear = (short)date.Year;
			this.wMonth = (short)date.Month;
			this.wDayOfWeek = (short)date.DayOfWeek;
			this.wDay = (short)date.Day;
			this.wHour = (short)date.Hour;
			this.wMinute = (short)date.Minute;
			this.wSecond = (short)date.Second;
			this.wMilliseconds = (short)date.Millisecond;
		}

		// Token: 0x04003BAE RID: 15278
		public short wYear;

		// Token: 0x04003BAF RID: 15279
		public short wMonth;

		// Token: 0x04003BB0 RID: 15280
		public short wDayOfWeek;

		// Token: 0x04003BB1 RID: 15281
		public short wDay;

		// Token: 0x04003BB2 RID: 15282
		public short wHour;

		// Token: 0x04003BB3 RID: 15283
		public short wMinute;

		// Token: 0x04003BB4 RID: 15284
		public short wSecond;

		// Token: 0x04003BB5 RID: 15285
		public short wMilliseconds;
	}
}

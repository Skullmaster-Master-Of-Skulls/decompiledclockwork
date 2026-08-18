using System;

namespace ReportFunctions.ClockWorkDataSync.Courses
{
	// Token: 0x02000023 RID: 35
	public class DataSyncTermScope
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00039A6C File Offset: 0x00038A6C
		public DataSyncTermScope()
		{
			DateTime now = DateTime.Now;
			int month = now.Month;
			int year = now.Year;
			if (month < 5)
			{
				this.StartDate = new DateTime(year, 1, 1);
				this.EndDate = new DateTime(year, 4, 30);
			}
			else if (month < 9)
			{
				this.StartDate = new DateTime(year, 5, 1);
				this.EndDate = new DateTime(year, 8, 30);
			}
			else
			{
				this.StartDate = new DateTime(year, 9, 1);
				this.EndDate = new DateTime(year, 12, 31);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00039B18 File Offset: 0x00038B18
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00039B2F File Offset: 0x00038B2F
		public DateTime StartDate { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00039B38 File Offset: 0x00038B38
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00039B4F File Offset: 0x00038B4F
		public DateTime EndDate { get; set; }
	}
}

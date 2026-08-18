using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000241 RID: 577
	public class DataSyncFixTimetableParameters
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00018412 File Offset: 0x00016612
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x0001841A File Offset: 0x0001661A
		public eDataSyncFixTimetableDayOfWeekType DayOfWeekType { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00018423 File Offset: 0x00016623
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x0001842B File Offset: 0x0001662B
		public bool IsDayOfWeekInSeparateColumns { get; set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00018434 File Offset: 0x00016634
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x0001843C File Offset: 0x0001663C
		public eDataSyncFixTimetableTimeType TimeType { get; set; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00018445 File Offset: 0x00016645
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x0001844D File Offset: 0x0001664D
		public string DayOfWeekColName { get; set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00018456 File Offset: 0x00016656
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0001845E File Offset: 0x0001665E
		public string StartTimeColName { get; set; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00018467 File Offset: 0x00016667
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x0001846F File Offset: 0x0001666F
		public string EndTimeColName { get; set; }
	}
}

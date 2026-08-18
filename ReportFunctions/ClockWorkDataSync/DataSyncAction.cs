using System;

namespace ReportFunctions.ClockWorkDataSync
{
	// Token: 0x0200002B RID: 43
	public class DataSyncAction
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0003B9D4 File Offset: 0x0003A9D4
		public DataSyncAction()
		{
			this.ActionType = DataSyncActionType.Unknown;
			this.ActionResult = DataSyncActionResult.Unknown;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0003B9F0 File Offset: 0x0003A9F0
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0003BA07 File Offset: 0x0003AA07
		public DataSyncActionType ActionType { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0003BA10 File Offset: 0x0003AA10
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0003BA27 File Offset: 0x0003AA27
		public int Pid { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0003BA30 File Offset: 0x0003AA30
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0003BA47 File Offset: 0x0003AA47
		public DataSyncActionResult ActionResult { get; set; }
	}
}

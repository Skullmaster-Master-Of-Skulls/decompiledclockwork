using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.SummaryManagement
{
	// Token: 0x02000183 RID: 387
	public class SummaryManagementInstance
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00012E3E File Offset: 0x0001103E
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00012E46 File Offset: 0x00011046
		public string Title { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00012E4F File Offset: 0x0001104F
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00012E57 File Offset: 0x00011057
		public int ReportId { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00012E60 File Offset: 0x00011060
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00012E68 File Offset: 0x00011068
		public byte[] ButtonImage { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00012E71 File Offset: 0x00011071
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00012E79 File Offset: 0x00011079
		public IList<int> ScreenNumsToTriggerUpdateWhenChanged { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00012E82 File Offset: 0x00011082
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00012E8A File Offset: 0x0001108A
		public int[] Screens { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00012E93 File Offset: 0x00011093
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00012E9B File Offset: 0x0001109B
		public int EmailCidOnPerDateFormToUpdateWhenEmailSent { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00012EA4 File Offset: 0x000110A4
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00012EAC File Offset: 0x000110AC
		public eSummaryManagementType SummaryManagementType { get; set; }
	}
}

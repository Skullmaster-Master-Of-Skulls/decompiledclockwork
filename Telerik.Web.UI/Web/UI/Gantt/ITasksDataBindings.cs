using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000354 RID: 852
	public interface ITasksDataBindings
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001D7B RID: 7547
		// (set) Token: 0x06001D7C RID: 7548
		string IdField { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001D7D RID: 7549
		// (set) Token: 0x06001D7E RID: 7550
		string ParentIdField { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001D7F RID: 7551
		// (set) Token: 0x06001D80 RID: 7552
		string OrderIdField { get; set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001D81 RID: 7553
		// (set) Token: 0x06001D82 RID: 7554
		string StartField { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001D83 RID: 7555
		// (set) Token: 0x06001D84 RID: 7556
		string PlannedStartField { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001D85 RID: 7557
		// (set) Token: 0x06001D86 RID: 7558
		string EndField { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001D87 RID: 7559
		// (set) Token: 0x06001D88 RID: 7560
		string PlannedEndField { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001D89 RID: 7561
		// (set) Token: 0x06001D8A RID: 7562
		string TitleField { get; set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001D8B RID: 7563
		// (set) Token: 0x06001D8C RID: 7564
		string ExpandedField { get; set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001D8D RID: 7565
		// (set) Token: 0x06001D8E RID: 7566
		string SummaryField { get; set; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001D8F RID: 7567
		// (set) Token: 0x06001D90 RID: 7568
		string PercentCompleteField { get; set; }
	}
}

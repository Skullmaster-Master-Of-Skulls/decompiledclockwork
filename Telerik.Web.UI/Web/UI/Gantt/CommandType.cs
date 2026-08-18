using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020004A1 RID: 1185
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum CommandType
	{
		// Token: 0x04000AC5 RID: 2757
		UpdateTask,
		// Token: 0x04000AC6 RID: 2758
		DeleteTask,
		// Token: 0x04000AC7 RID: 2759
		InsertTask,
		// Token: 0x04000AC8 RID: 2760
		UpdateDependency,
		// Token: 0x04000AC9 RID: 2761
		DeleteDependency,
		// Token: 0x04000ACA RID: 2762
		InsertDependency,
		// Token: 0x04000ACB RID: 2763
		UpdateAssignment,
		// Token: 0x04000ACC RID: 2764
		DeleteAssignment,
		// Token: 0x04000ACD RID: 2765
		InsertAssignment,
		// Token: 0x04000ACE RID: 2766
		SwitchToDayView,
		// Token: 0x04000ACF RID: 2767
		SwitchToWeekView,
		// Token: 0x04000AD0 RID: 2768
		SwitchToMonthView,
		// Token: 0x04000AD1 RID: 2769
		SwitchToYearView,
		// Token: 0x04000AD2 RID: 2770
		Postback
	}
}

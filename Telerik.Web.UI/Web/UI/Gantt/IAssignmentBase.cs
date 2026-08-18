using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002E4 RID: 740
	public interface IAssignmentBase
	{
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060019A5 RID: 6565
		// (set) Token: 0x060019A6 RID: 6566
		object ID { get; set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060019A7 RID: 6567
		// (set) Token: 0x060019A8 RID: 6568
		object TaskID { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060019A9 RID: 6569
		// (set) Token: 0x060019AA RID: 6570
		object ResourceID { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060019AB RID: 6571
		// (set) Token: 0x060019AC RID: 6572
		object Units { get; set; }
	}
}

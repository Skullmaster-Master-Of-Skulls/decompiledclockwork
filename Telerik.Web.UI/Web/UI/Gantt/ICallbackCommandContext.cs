using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002EC RID: 748
	public interface ICallbackCommandContext
	{
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060019D4 RID: 6612
		// (set) Token: 0x060019D5 RID: 6613
		CommandType Command { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060019D6 RID: 6614
		// (set) Token: 0x060019D7 RID: 6615
		List<ITask> Tasks { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060019D8 RID: 6616
		// (set) Token: 0x060019D9 RID: 6617
		List<IDependency> Dependencies { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060019DA RID: 6618
		List<IAssignment> Assignments { get; }
	}
}

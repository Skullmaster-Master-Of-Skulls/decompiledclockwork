using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000314 RID: 788
	public interface IAssignmentEvent
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001A9E RID: 6814
		IEnumerable<IAssignment> Assignments { get; }
	}
}

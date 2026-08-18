using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000336 RID: 822
	public interface ITaskEvent
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001C3D RID: 7229
		IEnumerable<ITask> Tasks { get; }
	}
}

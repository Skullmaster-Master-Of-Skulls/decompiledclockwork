using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000316 RID: 790
	public interface IDependencyEvent
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001AA3 RID: 6819
		IEnumerable<IDependency> Dependencies { get; }
	}
}

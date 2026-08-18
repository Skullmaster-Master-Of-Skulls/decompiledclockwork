using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000234 RID: 564
	[ParseChildren(typeof(DiagramConnection))]
	public class DiagramConnectionsCollection : StronglyTypedStateManagedCollection<DiagramConnection>
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0004783A File Offset: 0x00045A3A
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

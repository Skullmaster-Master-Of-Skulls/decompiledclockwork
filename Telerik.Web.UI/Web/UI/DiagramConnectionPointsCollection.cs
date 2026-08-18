using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000233 RID: 563
	[ParseChildren(typeof(DiagramConnectionPoint))]
	public class DiagramConnectionPointsCollection : StronglyTypedStateManagedCollection<DiagramConnectionPoint>
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0004782A File Offset: 0x00045A2A
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

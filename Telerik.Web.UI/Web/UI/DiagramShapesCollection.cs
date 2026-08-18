using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200024B RID: 587
	[ParseChildren(typeof(DiagramShape))]
	public class DiagramShapesCollection : StronglyTypedStateManagedCollection<DiagramShape>
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00049B0A File Offset: 0x00047D0A
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

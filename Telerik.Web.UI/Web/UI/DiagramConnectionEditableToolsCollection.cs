using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000230 RID: 560
	[ParseChildren(typeof(DiagramConnectionEditableTool))]
	public class DiagramConnectionEditableToolsCollection : StronglyTypedStateManagedCollection<DiagramConnectionEditableTool>
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0004770A File Offset: 0x0004590A
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

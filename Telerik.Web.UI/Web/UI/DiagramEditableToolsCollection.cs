using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000239 RID: 569
	[ParseChildren(typeof(DiagramEditableTool))]
	public class DiagramEditableToolsCollection : StronglyTypedStateManagedCollection<DiagramEditableTool>
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00047E7A File Offset: 0x0004607A
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200024A RID: 586
	[ParseChildren(typeof(DiagramShapeEditableTool))]
	public class DiagramShapeEditableToolsCollection : StronglyTypedStateManagedCollection<DiagramShapeEditableTool>
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00049AFA File Offset: 0x00047CFA
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

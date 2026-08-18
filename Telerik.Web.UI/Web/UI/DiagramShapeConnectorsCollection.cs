using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000246 RID: 582
	[ParseChildren(typeof(DiagramShapeConnector))]
	public class DiagramShapeConnectorsCollection : StronglyTypedStateManagedCollection<DiagramShapeConnector>
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00049772 File Offset: 0x00047972
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

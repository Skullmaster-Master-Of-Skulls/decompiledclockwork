using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005A3 RID: 1443
	[ParseChildren(typeof(MapMarker))]
	public class MapMarkersCollection : StronglyTypedStateManagedCollection<MapMarker>
	{
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000AC24A File Offset: 0x000AA44A
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

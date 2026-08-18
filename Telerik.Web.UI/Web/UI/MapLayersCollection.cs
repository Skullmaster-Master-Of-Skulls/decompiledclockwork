using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005A0 RID: 1440
	[ParseChildren(typeof(MapLayer))]
	public class MapLayersCollection : StronglyTypedStateManagedCollection<MapLayer>
	{
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000AC036 File Offset: 0x000AA236
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}

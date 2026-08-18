using System;

namespace System.Web.Routing
{
	// Token: 0x0200013D RID: 317
	internal class BoundUrl
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00036819 File Offset: 0x00034A19
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x00036821 File Offset: 0x00034A21
		public string Url { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0003682A File Offset: 0x00034A2A
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x00036832 File Offset: 0x00034A32
		public RouteValueDictionary Values { get; set; }
	}
}

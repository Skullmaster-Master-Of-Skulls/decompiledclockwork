using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000103 RID: 259
	internal class BoundRouteTemplate
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00014D30 File Offset: 0x00012F30
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x00014D38 File Offset: 0x00012F38
		public string BoundTemplate { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00014D41 File Offset: 0x00012F41
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00014D49 File Offset: 0x00012F49
		public HttpRouteValueDictionary Values { get; set; }
	}
}

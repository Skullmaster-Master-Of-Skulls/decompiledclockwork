using System;
using System.Web;

namespace Telerik.Web
{
	// Token: 0x020000CF RID: 207
	internal class RenderModeBrowserAdaptorConfiguration
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001E0FF File Offset: 0x0001C2FF
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001E107 File Offset: 0x0001C307
		public HttpContext Context { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001E110 File Offset: 0x0001C310
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001E118 File Offset: 0x0001C318
		public bool IsEdge { get; set; }
	}
}

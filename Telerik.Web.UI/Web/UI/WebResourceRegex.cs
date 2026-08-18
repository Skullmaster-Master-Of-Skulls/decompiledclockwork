using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000B0E RID: 2830
	internal class WebResourceRegex : CharArrayNameBoundaryRegex
	{
		// Token: 0x060069E9 RID: 27113 RVA: 0x0018DD9F File Offset: 0x0018BF9F
		public WebResourceRegex() : base("<% = WebResource(^\") %>")
		{
		}
	}
}

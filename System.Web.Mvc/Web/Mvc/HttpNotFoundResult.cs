using System;
using System.Net;

namespace System.Web.Mvc
{
	// Token: 0x020000DD RID: 221
	public class HttpNotFoundResult : HttpStatusCodeResult
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0000FB3F File Offset: 0x0000DD3F
		public HttpNotFoundResult() : this(null)
		{
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000FB48 File Offset: 0x0000DD48
		public HttpNotFoundResult(string statusDescription) : base(HttpStatusCode.NotFound, statusDescription)
		{
		}
	}
}

using System;
using System.Net;

namespace System.Web.Mvc
{
	// Token: 0x020001C8 RID: 456
	public class HttpUnauthorizedResult : HttpStatusCodeResult
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x00023999 File Offset: 0x00021B99
		public HttpUnauthorizedResult() : this(null)
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000239A2 File Offset: 0x00021BA2
		public HttpUnauthorizedResult(string statusDescription) : base(HttpStatusCode.Unauthorized, statusDescription)
		{
		}
	}
}

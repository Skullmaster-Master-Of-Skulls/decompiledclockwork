using System;

namespace System.Web
{
	// Token: 0x020000CE RID: 206
	public interface IHttpHandler
	{
		// Token: 0x06000DE0 RID: 3552
		void ProcessRequest(HttpContext context);

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000DE1 RID: 3553
		bool IsReusable { get; }
	}
}

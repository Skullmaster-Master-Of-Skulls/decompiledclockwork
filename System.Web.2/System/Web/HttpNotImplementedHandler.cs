using System;

namespace System.Web
{
	// Token: 0x020000A8 RID: 168
	internal class HttpNotImplementedHandler : IHttpHandler
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x000030B5 File Offset: 0x000012B5
		internal HttpNotImplementedHandler()
		{
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00017E20 File Offset: 0x00016020
		public void ProcessRequest(HttpContext context)
		{
			throw new HttpException(501, SR.GetString("Method_for_path_not_implemented", new object[]
			{
				context.Request.HttpMethod,
				context.Request.Path
			}));
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

using System;

namespace System.Web
{
	// Token: 0x020000A7 RID: 167
	internal class HttpMethodNotAllowedHandler : IHttpHandler
	{
		// Token: 0x06000A62 RID: 2658 RVA: 0x000030B5 File Offset: 0x000012B5
		internal HttpMethodNotAllowedHandler()
		{
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00017DF6 File Offset: 0x00015FF6
		public void ProcessRequest(HttpContext context)
		{
			throw new HttpException(405, SR.GetString("Path_forbidden", new object[]
			{
				context.Request.HttpMethod
			}));
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

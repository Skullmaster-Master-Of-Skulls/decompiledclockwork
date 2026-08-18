using System;

namespace System.Web
{
	// Token: 0x020000A6 RID: 166
	internal class HttpForbiddenHandler : IHttpHandler
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x000030B5 File Offset: 0x000012B5
		internal HttpForbiddenHandler()
		{
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00017DC5 File Offset: 0x00015FC5
		public void ProcessRequest(HttpContext context)
		{
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_NOT_FOUND);
			throw new HttpException(403, SR.GetString("Path_forbidden", new object[]
			{
				context.Request.Path
			}));
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

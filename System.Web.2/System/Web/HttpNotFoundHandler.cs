using System;

namespace System.Web
{
	// Token: 0x020000A5 RID: 165
	internal class HttpNotFoundHandler : IHttpHandler
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x000030B5 File Offset: 0x000012B5
		internal HttpNotFoundHandler()
		{
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00017D94 File Offset: 0x00015F94
		public void ProcessRequest(HttpContext context)
		{
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_NOT_FOUND);
			throw new HttpException(404, SR.GetString("Path_not_found", new object[]
			{
				context.Request.Path
			}));
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}

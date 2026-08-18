using System;
using System.Runtime.ExceptionServices;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200000C RID: 12
	internal class HttpRouteExceptionRouteHandler : IRouteHandler
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003106 File Offset: 0x00001306
		public HttpRouteExceptionRouteHandler(ExceptionDispatchInfo exceptionInfo)
		{
			this._exceptionInfo = exceptionInfo;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003115 File Offset: 0x00001315
		internal ExceptionDispatchInfo ExceptionInfo
		{
			get
			{
				return this._exceptionInfo;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000311D File Offset: 0x0000131D
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new HttpRouteExceptionHandler(this._exceptionInfo);
		}

		// Token: 0x0400000D RID: 13
		private readonly ExceptionDispatchInfo _exceptionInfo;
	}
}

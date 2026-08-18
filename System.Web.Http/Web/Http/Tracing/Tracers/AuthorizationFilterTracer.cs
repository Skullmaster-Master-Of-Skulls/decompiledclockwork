using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000164 RID: 356
	internal class AuthorizationFilterTracer : FilterTracer, IAuthorizationFilter, IFilter, IDecorator<IAuthorizationFilter>
	{
		// Token: 0x060008FE RID: 2302 RVA: 0x0001D9A2 File Offset: 0x0001BBA2
		public AuthorizationFilterTracer(IAuthorizationFilter innerFilter, ITraceWriter traceWriter) : base(innerFilter, traceWriter)
		{
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		public new IAuthorizationFilter Inner
		{
			get
			{
				return this.InnerAuthorizationFilter;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		private IAuthorizationFilter InnerAuthorizationFilter
		{
			get
			{
				return base.InnerFilter as IAuthorizationFilter;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001DA00 File Offset: 0x0001BC00
		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			return base.TraceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this.InnerAuthorizationFilter.GetType().Name, "ExecuteAuthorizationFilterAsync", null, () => this.InnerAuthorizationFilter.ExecuteAuthorizationFilterAsync(actionContext, cancellationToken, continuation), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x040002AD RID: 685
		private const string ExecuteAuthorizationFilterAsyncMethodName = "ExecuteAuthorizationFilterAsync";
	}
}

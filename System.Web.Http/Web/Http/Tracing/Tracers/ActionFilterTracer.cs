using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200015D RID: 349
	internal class ActionFilterTracer : FilterTracer, IActionFilter, IFilter, IDecorator<IActionFilter>
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0001CE4D File Offset: 0x0001B04D
		public ActionFilterTracer(IActionFilter innerFilter, ITraceWriter traceWriter) : base(innerFilter, traceWriter)
		{
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001CE57 File Offset: 0x0001B057
		public new IActionFilter Inner
		{
			get
			{
				return this.InnerActionFilter;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0001CE5F File Offset: 0x0001B05F
		private IActionFilter InnerActionFilter
		{
			get
			{
				return base.InnerFilter as IActionFilter;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		Task<HttpResponseMessage> IActionFilter.ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			return base.TraceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this.InnerActionFilter.GetType().Name, "ExecuteActionFilterAsync", null, () => this.InnerActionFilter.ExecuteActionFilterAsync(actionContext, cancellationToken, continuation), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x04000297 RID: 663
		private const string ExecuteActionFilterAsyncMethodName = "ExecuteActionFilterAsync";
	}
}

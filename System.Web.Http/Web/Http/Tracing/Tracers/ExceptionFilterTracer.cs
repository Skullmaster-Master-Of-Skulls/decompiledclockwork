using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000169 RID: 361
	internal class ExceptionFilterTracer : FilterTracer, IExceptionFilter, IFilter, IDecorator<IExceptionFilter>
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0001E479 File Offset: 0x0001C679
		public ExceptionFilterTracer(IExceptionFilter innerFilter, ITraceWriter traceWriter) : base(innerFilter, traceWriter)
		{
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001E483 File Offset: 0x0001C683
		public new IExceptionFilter Inner
		{
			get
			{
				return this.InnerExceptionFilter;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001E48B File Offset: 0x0001C68B
		public IExceptionFilter InnerExceptionFilter
		{
			get
			{
				return base.InnerFilter as IExceptionFilter;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			return base.TraceWriter.TraceBeginEndAsync(actionExecutedContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this.InnerExceptionFilter.GetType().Name, "ExecuteExceptionFilterAsync", delegate(TraceRecord tr)
			{
				tr.Exception = actionExecutedContext.Exception;
			}, () => this.InnerExceptionFilter.ExecuteExceptionFilterAsync(actionExecutedContext, cancellationToken), delegate(TraceRecord tr)
			{
				tr.Exception = actionExecutedContext.Exception;
			}, null);
		}

		// Token: 0x040002BB RID: 699
		private const string ExecuteExceptionFilterAsyncMethodName = "ExecuteExceptionFilterAsync";
	}
}

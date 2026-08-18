using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000168 RID: 360
	internal class ExceptionFilterAttributeTracer : ExceptionFilterAttribute, IDecorator<ExceptionFilterAttribute>
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x0001E19B File Offset: 0x0001C39B
		public ExceptionFilterAttributeTracer(ExceptionFilterAttribute innerFilter, ITraceWriter traceWriter)
		{
			this._innerFilter = innerFilter;
			this._traceStore = traceWriter;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001E1B1 File Offset: 0x0001C3B1
		public ExceptionFilterAttribute Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001E1B9 File Offset: 0x0001C3B9
		public override bool AllowMultiple
		{
			get
			{
				return this._innerFilter.AllowMultiple;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001E1C6 File Offset: 0x0001C3C6
		public override object TypeId
		{
			get
			{
				return this._innerFilter.TypeId;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001E1D3 File Offset: 0x0001C3D3
		public override bool Equals(object obj)
		{
			return this._innerFilter.Equals(obj);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001E1E1 File Offset: 0x0001C3E1
		public override int GetHashCode()
		{
			return this._innerFilter.GetHashCode();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001E1EE File Offset: 0x0001C3EE
		public override bool IsDefaultAttribute()
		{
			return this._innerFilter.IsDefaultAttribute();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001E1FB File Offset: 0x0001C3FB
		public override bool Match(object obj)
		{
			return this._innerFilter.Match(obj);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001E209 File Offset: 0x0001C409
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001E20B File Offset: 0x0001C40B
		public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			return this.OnExceptionAsyncCore(actionExecutedContext, cancellationToken, "OnExceptionAsync");
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		private Task OnExceptionAsyncCore(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			return this._traceStore.TraceBeginEndAsync(actionExecutedContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = actionExecutedContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, async delegate()
			{
				await this._innerFilter.OnExceptionAsync(actionExecutedContext, cancellationToken);
			}, delegate(TraceRecord tr)
			{
				Exception exception = actionExecutedContext.Exception;
				tr.Level = ((exception == null) ? TraceLevel.Info : TraceLevel.Error);
				tr.Exception = exception;
				HttpResponseMessage response = actionExecutedContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = actionExecutedContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			});
		}

		// Token: 0x040002B9 RID: 697
		private readonly ExceptionFilterAttribute _innerFilter;

		// Token: 0x040002BA RID: 698
		private readonly ITraceWriter _traceStore;
	}
}

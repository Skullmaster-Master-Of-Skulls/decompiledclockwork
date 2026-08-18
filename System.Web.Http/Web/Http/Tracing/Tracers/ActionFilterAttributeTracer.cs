using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200015C RID: 348
	internal class ActionFilterAttributeTracer : ActionFilterAttribute, IDecorator<ActionFilterAttribute>
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0001C8BD File Offset: 0x0001AABD
		public ActionFilterAttributeTracer(ActionFilterAttribute innerFilter, ITraceWriter traceWriter)
		{
			this._innerFilter = innerFilter;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001C8D3 File Offset: 0x0001AAD3
		public ActionFilterAttribute Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001C8DB File Offset: 0x0001AADB
		public override bool AllowMultiple
		{
			get
			{
				return this._innerFilter.AllowMultiple;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001C8E8 File Offset: 0x0001AAE8
		public override object TypeId
		{
			get
			{
				return this._innerFilter.TypeId;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001C8F5 File Offset: 0x0001AAF5
		public override bool Equals(object obj)
		{
			return this._innerFilter.Equals(obj);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C903 File Offset: 0x0001AB03
		public override int GetHashCode()
		{
			return this._innerFilter.GetHashCode();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001C910 File Offset: 0x0001AB10
		public override bool IsDefaultAttribute()
		{
			return this._innerFilter.IsDefaultAttribute();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001C91D File Offset: 0x0001AB1D
		public override bool Match(object obj)
		{
			return this._innerFilter.Match(obj);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001C92B File Offset: 0x0001AB2B
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001C92D File Offset: 0x0001AB2D
		public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			return this.OnActionExecutedAsyncCore(actionExecutedContext, cancellationToken, "OnActionExecutedAsync");
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001CB4C File Offset: 0x0001AD4C
		private Task OnActionExecutedAsyncCore(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			return this._traceWriter.TraceBeginEndAsync(actionExecutedContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionFilterMessage, new object[]
				{
					FormattingUtilities.ActionDescriptorToString(actionExecutedContext.ActionContext.ActionDescriptor)
				});
				tr.Exception = actionExecutedContext.Exception;
				HttpResponseMessage response = actionExecutedContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, async delegate()
			{
				await this._innerFilter.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
			}, delegate(TraceRecord tr)
			{
				tr.Exception = actionExecutedContext.Exception;
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

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001CBD1 File Offset: 0x0001ADD1
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001CBD3 File Offset: 0x0001ADD3
		public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this.OnActionExecutingAsyncCore(actionContext, cancellationToken, "OnActionExecutingAsync");
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
		private Task OnActionExecutingAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			return this._traceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionFilterMessage, new object[]
				{
					FormattingUtilities.ActionDescriptorToString(actionContext.ActionDescriptor)
				});
				HttpResponseMessage response = actionContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, async delegate()
			{
				await this._innerFilter.OnActionExecutingAsync(actionContext, cancellationToken);
			}, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = actionContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = actionContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			});
		}

		// Token: 0x04000295 RID: 661
		private readonly ActionFilterAttribute _innerFilter;

		// Token: 0x04000296 RID: 662
		private readonly ITraceWriter _traceWriter;
	}
}

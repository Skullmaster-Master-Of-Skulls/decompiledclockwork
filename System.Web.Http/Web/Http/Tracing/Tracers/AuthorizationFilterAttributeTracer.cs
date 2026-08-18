using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000163 RID: 355
	internal class AuthorizationFilterAttributeTracer : AuthorizationFilterAttribute, IDecorator<AuthorizationFilterAttribute>
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		public AuthorizationFilterAttributeTracer(AuthorizationFilterAttribute innerFilter, ITraceWriter traceWriter)
		{
			this._innerFilter = innerFilter;
			this._traceStore = traceWriter;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0001D6F6 File Offset: 0x0001B8F6
		public AuthorizationFilterAttribute Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001D6FE File Offset: 0x0001B8FE
		public override bool AllowMultiple
		{
			get
			{
				return this._innerFilter.AllowMultiple;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001D70B File Offset: 0x0001B90B
		public override object TypeId
		{
			get
			{
				return this._innerFilter.TypeId;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001D718 File Offset: 0x0001B918
		public override bool Equals(object obj)
		{
			return this._innerFilter.Equals(obj);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001D726 File Offset: 0x0001B926
		public override int GetHashCode()
		{
			return this._innerFilter.GetHashCode();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001D733 File Offset: 0x0001B933
		public override bool IsDefaultAttribute()
		{
			return this._innerFilter.IsDefaultAttribute();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001D740 File Offset: 0x0001B940
		public override bool Match(object obj)
		{
			return this._innerFilter.Match(obj);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001D74E File Offset: 0x0001B94E
		public override void OnAuthorization(HttpActionContext actionContext)
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001D750 File Offset: 0x0001B950
		public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this.OnAuthorizationSyncCore(actionContext, cancellationToken, "OnAuthorizationAsync");
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001D918 File Offset: 0x0001BB18
		private Task OnAuthorizationSyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			return this._traceStore.TraceBeginEndAsync(actionContext.ControllerContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = actionContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, async delegate()
			{
				await this._innerFilter.OnAuthorizationAsync(actionContext, cancellationToken);
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

		// Token: 0x040002AB RID: 683
		private readonly AuthorizationFilterAttribute _innerFilter;

		// Token: 0x040002AC RID: 684
		private readonly ITraceWriter _traceStore;
	}
}

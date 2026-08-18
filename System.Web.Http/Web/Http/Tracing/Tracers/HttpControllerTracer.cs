using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000162 RID: 354
	internal class HttpControllerTracer : IHttpController, IDisposable, IDecorator<IHttpController>
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0001D3B9 File Offset: 0x0001B5B9
		public HttpControllerTracer(HttpRequestMessage request, IHttpController innerController, ITraceWriter traceWriter)
		{
			this._innerController = innerController;
			this._request = request;
			this._traceWriter = traceWriter;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001D3D6 File Offset: 0x0001B5D6
		public IHttpController Inner
		{
			get
			{
				return this._innerController;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		void IDisposable.Dispose()
		{
			IDisposable disposable = this._innerController as IDisposable;
			if (disposable != null)
			{
				this._traceWriter.TraceBeginEnd(this._request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerController.GetType().Name, "Dispose", null, new Action(disposable.Dispose), null, null);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001D488 File Offset: 0x0001B688
		Task<HttpResponseMessage> IHttpController.ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(controllerContext.Request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerController.GetType().Name, "ExecuteAsync", null, delegate()
			{
				controllerContext.Controller = HttpControllerTracer.ActualController(controllerContext.Controller);
				return this.ExecuteAsyncCore(controllerContext, cancellationToken);
			}, delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001D65C File Offset: 0x0001B85C
		private async Task<HttpResponseMessage> ExecuteAsyncCore(HttpControllerContext controllerContext, CancellationToken cancellationToken)
		{
			HttpResponseMessage result;
			try
			{
				result = await this._innerController.ExecuteAsync(controllerContext, cancellationToken);
			}
			finally
			{
				IDisposable disposable = this._innerController as IDisposable;
				IList<IDisposable> list;
				if (disposable != null && this._request.Properties.TryGetValue(HttpPropertyKeys.DisposableRequestResourcesKey, out list))
				{
					list.Remove(disposable);
					list.Add(this);
				}
			}
			return result;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		public static IHttpController ActualController(IHttpController controller)
		{
			HttpControllerTracer httpControllerTracer = controller as HttpControllerTracer;
			if (httpControllerTracer != null)
			{
				return httpControllerTracer._innerController;
			}
			return controller;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001D6D3 File Offset: 0x0001B8D3
		public static Type ActualControllerType(IHttpController controller)
		{
			return HttpControllerTracer.ActualController(controller).GetType();
		}

		// Token: 0x040002A5 RID: 677
		private const string DisposeMethodName = "Dispose";

		// Token: 0x040002A6 RID: 678
		private const string ExecuteAsyncMethodName = "ExecuteAsync";

		// Token: 0x040002A7 RID: 679
		private readonly IHttpController _innerController;

		// Token: 0x040002A8 RID: 680
		private readonly HttpRequestMessage _request;

		// Token: 0x040002A9 RID: 681
		private readonly ITraceWriter _traceWriter;
	}
}

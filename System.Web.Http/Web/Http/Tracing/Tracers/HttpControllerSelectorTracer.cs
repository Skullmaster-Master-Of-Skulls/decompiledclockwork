using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000167 RID: 359
	internal class HttpControllerSelectorTracer : IHttpControllerSelector, IDecorator<IHttpControllerSelector>
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0001E048 File Offset: 0x0001C248
		public HttpControllerSelectorTracer(IHttpControllerSelector innerSelector, ITraceWriter traceWriter)
		{
			this._innerSelector = innerSelector;
			this._traceWriter = traceWriter;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001E05E File Offset: 0x0001C25E
		public IHttpControllerSelector Inner
		{
			get
			{
				return this._innerSelector;
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		HttpControllerDescriptor IHttpControllerSelector.SelectController(HttpRequestMessage request)
		{
			HttpControllerDescriptor controllerDescriptor = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerSelector.GetType().Name, "SelectController", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceRouteMessage, new object[]
				{
					FormattingUtilities.RouteToString(request.GetRouteData())
				});
			}, delegate
			{
				controllerDescriptor = this._innerSelector.SelectController(request);
			}, delegate(TraceRecord tr)
			{
				tr.Message = ((controllerDescriptor == null) ? SRResources.TraceNoneObjectMessage : controllerDescriptor.ControllerName);
			}, null);
			if (controllerDescriptor != null && !(controllerDescriptor is HttpControllerDescriptorTracer))
			{
				return new HttpControllerDescriptorTracer(controllerDescriptor, this._traceWriter);
			}
			return controllerDescriptor;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001E18E File Offset: 0x0001C38E
		IDictionary<string, HttpControllerDescriptor> IHttpControllerSelector.GetControllerMapping()
		{
			return this._innerSelector.GetControllerMapping();
		}

		// Token: 0x040002B6 RID: 694
		private const string SelectControllerMethodName = "SelectController";

		// Token: 0x040002B7 RID: 695
		private readonly IHttpControllerSelector _innerSelector;

		// Token: 0x040002B8 RID: 696
		private readonly ITraceWriter _traceWriter;
	}
}

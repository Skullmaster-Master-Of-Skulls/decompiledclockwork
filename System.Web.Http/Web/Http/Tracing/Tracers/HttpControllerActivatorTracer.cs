using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000166 RID: 358
	internal class HttpControllerActivatorTracer : IHttpControllerActivator, IDecorator<IHttpControllerActivator>
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x0001DF1B File Offset: 0x0001C11B
		public HttpControllerActivatorTracer(IHttpControllerActivator innerActivator, ITraceWriter traceWriter)
		{
			this._innerActivator = innerActivator;
			this._traceWriter = traceWriter;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001DF31 File Offset: 0x0001C131
		public IHttpControllerActivator Inner
		{
			get
			{
				return this._innerActivator;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001DF94 File Offset: 0x0001C194
		IHttpController IHttpControllerActivator.Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			IHttpController controller = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerActivator.GetType().Name, "Create", null, delegate
			{
				controller = this._innerActivator.Create(request, controllerDescriptor, controllerType);
			}, delegate(TraceRecord tr)
			{
				tr.Message = ((controller == null) ? SRResources.TraceNoneObjectMessage : controller.GetType().FullName);
			}, null);
			if (controller != null && !(controller is HttpControllerTracer))
			{
				controller = new HttpControllerTracer(request, controller, this._traceWriter);
			}
			return controller;
		}

		// Token: 0x040002B3 RID: 691
		private const string CreateMethodName = "Create";

		// Token: 0x040002B4 RID: 692
		private readonly IHttpControllerActivator _innerActivator;

		// Token: 0x040002B5 RID: 693
		private readonly ITraceWriter _traceWriter;
	}
}

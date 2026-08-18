using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000161 RID: 353
	internal class HttpControllerDescriptorTracer : HttpControllerDescriptor, IDecorator<HttpControllerDescriptor>
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0001D253 File Offset: 0x0001B453
		public HttpControllerDescriptorTracer(HttpControllerDescriptor innerDescriptor, ITraceWriter traceWriter)
		{
			base.Configuration = innerDescriptor.Configuration;
			base.ControllerName = innerDescriptor.ControllerName;
			base.ControllerType = innerDescriptor.ControllerType;
			this._innerDescriptor = innerDescriptor;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001D28D File Offset: 0x0001B48D
		public HttpControllerDescriptor Inner
		{
			get
			{
				return this._innerDescriptor;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0001D295 File Offset: 0x0001B495
		public override ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._innerDescriptor.Properties;
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001D2A2 File Offset: 0x0001B4A2
		public override Collection<T> GetCustomAttributes<T>()
		{
			return this._innerDescriptor.GetCustomAttributes<T>();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001D2AF File Offset: 0x0001B4AF
		public override Collection<T> GetCustomAttributes<T>(bool inherit)
		{
			return this._innerDescriptor.GetCustomAttributes<T>(inherit);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001D2BD File Offset: 0x0001B4BD
		public override Collection<IFilter> GetFilters()
		{
			return this._innerDescriptor.GetFilters();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001D318 File Offset: 0x0001B518
		public override IHttpController CreateController(HttpRequestMessage request)
		{
			IHttpController controller = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerDescriptor.GetType().Name, "CreateController", null, delegate
			{
				controller = this._innerDescriptor.CreateController(request);
			}, delegate(TraceRecord tr)
			{
				tr.Message = ((controller == null) ? SRResources.TraceNoneObjectMessage : HttpControllerTracer.ActualControllerType(controller).FullName);
			}, null);
			if (controller != null && !(controller is HttpControllerTracer))
			{
				return new HttpControllerTracer(request, controller, this._traceWriter);
			}
			return controller;
		}

		// Token: 0x040002A2 RID: 674
		private const string CreateControllerMethodName = "CreateController";

		// Token: 0x040002A3 RID: 675
		private readonly HttpControllerDescriptor _innerDescriptor;

		// Token: 0x040002A4 RID: 676
		private readonly ITraceWriter _traceWriter;
	}
}

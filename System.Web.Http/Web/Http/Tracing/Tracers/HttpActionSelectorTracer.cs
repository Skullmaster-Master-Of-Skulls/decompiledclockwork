using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200015F RID: 351
	internal class HttpActionSelectorTracer : IHttpActionSelector, IDecorator<IHttpActionSelector>
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0001D07B File Offset: 0x0001B27B
		public HttpActionSelectorTracer(IHttpActionSelector innerSelector, ITraceWriter traceWriter)
		{
			this._innerSelector = innerSelector;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0001D091 File Offset: 0x0001B291
		public IHttpActionSelector Inner
		{
			get
			{
				return this._innerSelector;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001D099 File Offset: 0x0001B299
		public ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor)
		{
			return this._innerSelector.GetActionMapping(controllerDescriptor);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D104 File Offset: 0x0001B304
		HttpActionDescriptor IHttpActionSelector.SelectAction(HttpControllerContext controllerContext)
		{
			HttpActionDescriptor actionDescriptor = null;
			this._traceWriter.TraceBeginEnd(controllerContext.Request, TraceCategories.ActionCategory, TraceLevel.Info, this._innerSelector.GetType().Name, "SelectAction", null, delegate
			{
				actionDescriptor = this._innerSelector.SelectAction(controllerContext);
			}, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionSelectedMessage, new object[]
				{
					FormattingUtilities.ActionDescriptorToString(actionDescriptor)
				});
			}, null);
			if (actionDescriptor != null && !(actionDescriptor is HttpActionDescriptorTracer))
			{
				return new HttpActionDescriptorTracer(controllerContext, actionDescriptor, this._traceWriter);
			}
			return actionDescriptor;
		}

		// Token: 0x0400029D RID: 669
		private const string SelectActionMethodName = "SelectAction";

		// Token: 0x0400029E RID: 670
		private readonly IHttpActionSelector _innerSelector;

		// Token: 0x0400029F RID: 671
		private readonly ITraceWriter _traceWriter;
	}
}

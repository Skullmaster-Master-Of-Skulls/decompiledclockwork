using System;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.SessionState;

namespace System.Web.Mvc
{
	// Token: 0x020001E5 RID: 485
	public class MvcRouteHandler : IRouteHandler
	{
		// Token: 0x06000E9D RID: 3741 RVA: 0x00026A94 File Offset: 0x00024C94
		public MvcRouteHandler()
		{
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00026A9C File Offset: 0x00024C9C
		public MvcRouteHandler(IControllerFactory controllerFactory)
		{
			this._controllerFactory = controllerFactory;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00026AAB File Offset: 0x00024CAB
		protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			requestContext.HttpContext.SetSessionStateBehavior(this.GetSessionStateBehavior(requestContext));
			return new MvcHandler(requestContext);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00026AC8 File Offset: 0x00024CC8
		protected virtual SessionStateBehavior GetSessionStateBehavior(RequestContext requestContext)
		{
			string text = (string)requestContext.RouteData.Values["controller"];
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new InvalidOperationException(MvcResources.MvcRouteHandler_RouteValuesHasNoController);
			}
			IControllerFactory controllerFactory = this._controllerFactory ?? ControllerBuilder.Current.GetControllerFactory();
			return controllerFactory.GetControllerSessionBehavior(requestContext, text);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00026B20 File Offset: 0x00024D20
		IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
		{
			return this.GetHttpHandler(requestContext);
		}

		// Token: 0x040003DB RID: 987
		private IControllerFactory _controllerFactory;
	}
}

using System;
using System.Globalization;
using System.Web.Routing;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x02000010 RID: 16
	internal class ResourceRouteHandler : IRouteHandler
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002F90 File Offset: 0x00001190
		public ResourceRouteHandler(ApplicationPartRegistry partRegistry)
		{
			this._partRegistry = partRegistry;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FA0 File Offset: 0x000011A0
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			string requiredString = requestContext.RouteData.GetRequiredString("module");
			ApplicationPart applicationPart = this._partRegistry[requiredString];
			if (applicationPart == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.ApplicationPart_ModuleCannotBeFound, new object[]
				{
					requiredString
				}));
			}
			string requiredString2 = requestContext.RouteData.GetRequiredString("path");
			return new ResourceHandler(applicationPart, requiredString2);
		}

		// Token: 0x0400001C RID: 28
		private ApplicationPartRegistry _partRegistry;
	}
}

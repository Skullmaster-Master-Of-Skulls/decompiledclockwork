using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x02000151 RID: 337
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class StopRoutingHandler : IRouteHandler
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00038A22 File Offset: 0x00036C22
		IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
		{
			return this.GetHttpHandler(requestContext);
		}
	}
}

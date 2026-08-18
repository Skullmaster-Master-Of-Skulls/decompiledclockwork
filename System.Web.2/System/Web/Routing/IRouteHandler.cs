using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x02000141 RID: 321
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public interface IRouteHandler
	{
		// Token: 0x06001300 RID: 4864
		IHttpHandler GetHttpHandler(RequestContext requestContext);
	}
}

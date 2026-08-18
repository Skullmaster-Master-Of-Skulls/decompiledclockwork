using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000DB RID: 219
	public interface IHttpControllerActivator
	{
		// Token: 0x06000549 RID: 1353
		IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType);
	}
}

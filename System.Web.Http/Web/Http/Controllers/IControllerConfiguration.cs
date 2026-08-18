using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000034 RID: 52
	public interface IControllerConfiguration
	{
		// Token: 0x06000140 RID: 320
		void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor);
	}
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000116 RID: 278
	public interface IHttpControllerSelector
	{
		// Token: 0x060006A7 RID: 1703
		HttpControllerDescriptor SelectController(HttpRequestMessage request);

		// Token: 0x060006A8 RID: 1704
		IDictionary<string, HttpControllerDescriptor> GetControllerMapping();
	}
}

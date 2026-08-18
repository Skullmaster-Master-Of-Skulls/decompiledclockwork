using System;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200001F RID: 31
	public interface IActionHttpMethodProvider
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BF RID: 191
		Collection<HttpMethod> HttpMethods { get; }
	}
}

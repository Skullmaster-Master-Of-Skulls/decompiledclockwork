using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000B7 RID: 183
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpOptionsAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000CDD9 File Offset: 0x0000AFD9
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpOptionsAttribute._supportedMethods;
			}
		}

		// Token: 0x04000130 RID: 304
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			HttpMethod.Options
		});
	}
}

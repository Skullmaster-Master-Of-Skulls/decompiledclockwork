using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000B9 RID: 185
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPatchAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000CE47 File Offset: 0x0000B047
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpPatchAttribute._supportedMethods;
			}
		}

		// Token: 0x04000132 RID: 306
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			new HttpMethod("PATCH")
		});
	}
}

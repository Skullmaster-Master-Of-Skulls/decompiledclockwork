using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000E0 RID: 224
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpDeleteAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00011C2B File Offset: 0x0000FE2B
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpDeleteAttribute._supportedMethods;
			}
		}

		// Token: 0x04000194 RID: 404
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			HttpMethod.Delete
		});
	}
}

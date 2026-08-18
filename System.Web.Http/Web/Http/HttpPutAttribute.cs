using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000DF RID: 223
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPutAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00011BF5 File Offset: 0x0000FDF5
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpPutAttribute._supportedMethods;
			}
		}

		// Token: 0x04000193 RID: 403
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			HttpMethod.Put
		});
	}
}

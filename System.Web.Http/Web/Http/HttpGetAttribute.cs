using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpGetAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00012605 File Offset: 0x00010805
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpGetAttribute._supportedMethods;
			}
		}

		// Token: 0x040001A0 RID: 416
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			HttpMethod.Get
		});
	}
}

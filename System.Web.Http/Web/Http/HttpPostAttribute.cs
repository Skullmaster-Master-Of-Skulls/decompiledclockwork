using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000E8 RID: 232
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPostAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001263B File Offset: 0x0001083B
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpPostAttribute._supportedMethods;
			}
		}

		// Token: 0x040001A1 RID: 417
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			HttpMethod.Post
		});
	}
}

using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x020000B8 RID: 184
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpHeadAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000CE0F File Offset: 0x0000B00F
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpHeadAttribute._supportedMethods;
			}
		}

		// Token: 0x04000131 RID: 305
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			HttpMethod.Head
		});
	}
}

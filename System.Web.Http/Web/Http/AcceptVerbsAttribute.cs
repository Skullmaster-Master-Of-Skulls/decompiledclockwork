using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class AcceptVerbsAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public AcceptVerbsAttribute(string method) : this(new string[]
		{
			method
		})
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public AcceptVerbsAttribute(params string[] methods)
		{
			Collection<HttpMethod> httpMethods;
			if (methods == null)
			{
				httpMethods = new Collection<HttpMethod>(new HttpMethod[0]);
			}
			else
			{
				httpMethods = new Collection<HttpMethod>((from method in methods
				select HttpMethodHelper.GetHttpMethod(method)).ToArray<HttpMethod>());
			}
			this._httpMethods = httpMethods;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004B46 File Offset: 0x00002D46
		internal AcceptVerbsAttribute(params HttpMethod[] methods)
		{
			this._httpMethods = new Collection<HttpMethod>(methods);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004B5A File Offset: 0x00002D5A
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return this._httpMethods;
			}
		}

		// Token: 0x04000037 RID: 55
		private readonly Collection<HttpMethod> _httpMethods;
	}
}

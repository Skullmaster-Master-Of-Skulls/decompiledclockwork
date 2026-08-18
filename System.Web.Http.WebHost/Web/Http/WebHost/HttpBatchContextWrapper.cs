using System;
using System.Collections;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.WebHost.Routing;

namespace System.Web.Http.WebHost
{
	// Token: 0x0200000F RID: 15
	internal class HttpBatchContextWrapper : HttpContextBase
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000035AC File Offset: 0x000017AC
		public HttpBatchContextWrapper(HttpContextBase httpContext, HttpRequestMessage httpRequest)
		{
			this._httpContextBase = httpContext;
			this._items = new Hashtable();
			this._httpRequestWrapper = new HttpRequestMessageWrapper(httpContext.Request.ApplicationPath, httpRequest);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000035DD File Offset: 0x000017DD
		public override HttpRequestBase Request
		{
			get
			{
				return this._httpRequestWrapper;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000035E5 File Offset: 0x000017E5
		public override HttpResponseBase Response
		{
			get
			{
				return this._httpContextBase.Response;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000035F2 File Offset: 0x000017F2
		public override IDictionary Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000035FA File Offset: 0x000017FA
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003607 File Offset: 0x00001807
		public override IPrincipal User
		{
			get
			{
				return this._httpContextBase.User;
			}
			set
			{
				this._httpContextBase.User = value;
			}
		}

		// Token: 0x04000016 RID: 22
		private HttpRequestMessageWrapper _httpRequestWrapper;

		// Token: 0x04000017 RID: 23
		private HttpContextBase _httpContextBase;

		// Token: 0x04000018 RID: 24
		private Hashtable _items;
	}
}

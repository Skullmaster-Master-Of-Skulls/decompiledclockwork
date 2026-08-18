using System;
using System.Collections;
using System.Net.Http;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x02000012 RID: 18
	internal class HttpRequestMessageContextWrapper : HttpContextBase
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003837 File Offset: 0x00001A37
		public HttpRequestMessageContextWrapper(string virtualPathRoot, HttpRequestMessage httpRequest)
		{
			this._httpWrapper = new HttpRequestMessageWrapper(virtualPathRoot, httpRequest);
			this._items = new Hashtable();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003857 File Offset: 0x00001A57
		public override HttpRequestBase Request
		{
			get
			{
				return this._httpWrapper;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000385F File Offset: 0x00001A5F
		public override IDictionary Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x0400001D RID: 29
		private HttpRequestMessageWrapper _httpWrapper;

		// Token: 0x0400001E RID: 30
		private Hashtable _items;
	}
}

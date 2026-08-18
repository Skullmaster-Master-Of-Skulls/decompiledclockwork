using System;
using System.Collections.Specialized;

namespace System.Web.Helpers
{
	// Token: 0x02000064 RID: 100
	[Obsolete("Use System.Web.HttpRequest.Unvalidated instead.")]
	public sealed class UnvalidatedRequestValues
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00009B60 File Offset: 0x00007D60
		internal UnvalidatedRequestValues(HttpRequestBase request)
		{
			this._request = request;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009B6F File Offset: 0x00007D6F
		public NameValueCollection Form
		{
			get
			{
				return this._request.Unvalidated.Form;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00009B81 File Offset: 0x00007D81
		public NameValueCollection QueryString
		{
			get
			{
				return this._request.Unvalidated.QueryString;
			}
		}

		// Token: 0x1700007A RID: 122
		public string this[string key]
		{
			get
			{
				return this._request.Unvalidated[key];
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly HttpRequestBase _request;
	}
}

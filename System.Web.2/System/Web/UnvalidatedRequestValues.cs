using System;
using System.Collections.Specialized;

namespace System.Web
{
	// Token: 0x02000051 RID: 81
	public sealed class UnvalidatedRequestValues
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00007A08 File Offset: 0x00005C08
		internal UnvalidatedRequestValues(HttpRequest request)
		{
			this._request = request;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00007A18 File Offset: 0x00005C18
		public NameValueCollection Form
		{
			get
			{
				if (this._form == null)
				{
					HttpValueCollection col = this._request.EnsureForm();
					this._form = new HttpValueCollection(col);
				}
				return this._form;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00007A4B File Offset: 0x00005C4B
		internal void InvalidateForm()
		{
			this._form = null;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00007A54 File Offset: 0x00005C54
		public NameValueCollection QueryString
		{
			get
			{
				if (this._queryString == null)
				{
					HttpValueCollection col = this._request.EnsureQueryString();
					this._queryString = new HttpValueCollection(col);
				}
				return this._queryString;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00007A87 File Offset: 0x00005C87
		internal void InvalidateQueryString()
		{
			this._queryString = null;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00007A90 File Offset: 0x00005C90
		public NameValueCollection Headers
		{
			get
			{
				if (this._headers == null)
				{
					HttpHeaderCollection col = this._request.EnsureHeaders();
					this._headers = new HttpHeaderCollection(col);
				}
				return this._headers;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public HttpCookieCollection Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					HttpCookieCollection col = this._request.EnsureCookies();
					this._cookies = new HttpCookieCollection(col);
				}
				return this._cookies;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public HttpFileCollection Files
		{
			get
			{
				if (this._files == null)
				{
					HttpFileCollection col = this._request.EnsureFiles();
					this._files = new HttpFileCollection(col);
				}
				return this._files;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00007B2B File Offset: 0x00005D2B
		public string RawUrl
		{
			get
			{
				return this._request.EnsureRawUrl();
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00007B38 File Offset: 0x00005D38
		public string Path
		{
			get
			{
				return this._request.GetUnvalidatedPath();
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00007B45 File Offset: 0x00005D45
		public string PathInfo
		{
			get
			{
				return this._request.GetUnvalidatedPathInfo();
			}
		}

		// Token: 0x17000289 RID: 649
		public string this[string field]
		{
			get
			{
				string text = this.QueryString[field];
				if (text != null)
				{
					return text;
				}
				string text2 = this.Form[field];
				if (text2 != null)
				{
					return text2;
				}
				HttpCookie httpCookie = this.Cookies[field];
				if (httpCookie != null)
				{
					return httpCookie.Value;
				}
				string text3 = this._request.ServerVariables[field];
				if (text3 != null)
				{
					return text3;
				}
				return null;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public Uri Url
		{
			get
			{
				if (this._url == null)
				{
					this._url = this._request.BuildUrl(() => this.Path);
				}
				return this._url;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00007BE7 File Offset: 0x00005DE7
		internal void InvalidateUrl()
		{
			this._url = null;
		}

		// Token: 0x04000154 RID: 340
		private readonly HttpRequest _request;

		// Token: 0x04000155 RID: 341
		private HttpValueCollection _form;

		// Token: 0x04000156 RID: 342
		private HttpValueCollection _queryString;

		// Token: 0x04000157 RID: 343
		private HttpHeaderCollection _headers;

		// Token: 0x04000158 RID: 344
		private HttpCookieCollection _cookies;

		// Token: 0x04000159 RID: 345
		private HttpFileCollection _files;

		// Token: 0x0400015A RID: 346
		private Uri _url;
	}
}

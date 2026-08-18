using System;
using System.Collections.Specialized;

namespace System.Web
{
	// Token: 0x0200004F RID: 79
	public class UnvalidatedRequestValuesWrapper : UnvalidatedRequestValuesBase
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00007963 File Offset: 0x00005B63
		public UnvalidatedRequestValuesWrapper(UnvalidatedRequestValues requestValues)
		{
			if (requestValues == null)
			{
				throw new ArgumentNullException("requestValues");
			}
			this._requestValues = requestValues;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00007980 File Offset: 0x00005B80
		public override NameValueCollection Form
		{
			get
			{
				return this._requestValues.Form;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000798D File Offset: 0x00005B8D
		public override NameValueCollection QueryString
		{
			get
			{
				return this._requestValues.QueryString;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000799A File Offset: 0x00005B9A
		public override NameValueCollection Headers
		{
			get
			{
				return this._requestValues.Headers;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000079A7 File Offset: 0x00005BA7
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._requestValues.Cookies;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000079B4 File Offset: 0x00005BB4
		public override HttpFileCollectionBase Files
		{
			get
			{
				return new HttpFileCollectionWrapper(this._requestValues.Files);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000079C6 File Offset: 0x00005BC6
		public override string RawUrl
		{
			get
			{
				return this._requestValues.RawUrl;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000079D3 File Offset: 0x00005BD3
		public override string Path
		{
			get
			{
				return this._requestValues.Path;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000079E0 File Offset: 0x00005BE0
		public override string PathInfo
		{
			get
			{
				return this._requestValues.PathInfo;
			}
		}

		// Token: 0x17000275 RID: 629
		public override string this[string field]
		{
			get
			{
				return this._requestValues[field];
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000079FB File Offset: 0x00005BFB
		public override Uri Url
		{
			get
			{
				return this._requestValues.Url;
			}
		}

		// Token: 0x04000153 RID: 339
		private readonly UnvalidatedRequestValues _requestValues;
	}
}

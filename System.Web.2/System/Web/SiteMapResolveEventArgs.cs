using System;

namespace System.Web
{
	// Token: 0x020000F5 RID: 245
	public class SiteMapResolveEventArgs : EventArgs
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00029961 File Offset: 0x00027B61
		public SiteMapResolveEventArgs(HttpContext context, SiteMapProvider provider)
		{
			this._context = context;
			this._provider = provider;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00029977 File Offset: 0x00027B77
		public SiteMapProvider Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0002997F File Offset: 0x00027B7F
		public HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x040005A7 RID: 1447
		private HttpContext _context;

		// Token: 0x040005A8 RID: 1448
		private SiteMapProvider _provider;
	}
}

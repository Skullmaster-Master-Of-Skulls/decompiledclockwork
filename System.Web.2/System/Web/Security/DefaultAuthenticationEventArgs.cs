using System;

namespace System.Web.Security
{
	// Token: 0x020005E4 RID: 1508
	public sealed class DefaultAuthenticationEventArgs : EventArgs
	{
		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x001041FA File Offset: 0x001023FA
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x00104202 File Offset: 0x00102402
		public DefaultAuthenticationEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x040028F7 RID: 10487
		private HttpContext _Context;
	}
}

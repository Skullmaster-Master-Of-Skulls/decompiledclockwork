using System;

namespace System.Web.Security
{
	// Token: 0x020005D1 RID: 1489
	public sealed class AnonymousIdentificationEventArgs : EventArgs
	{
		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x000FFBBB File Offset: 0x000FDDBB
		// (set) Token: 0x06004B62 RID: 19298 RVA: 0x000FFBC3 File Offset: 0x000FDDC3
		public string AnonymousID
		{
			get
			{
				return this._AnonymousId;
			}
			set
			{
				this._AnonymousId = value;
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x000FFBCC File Offset: 0x000FDDCC
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x000FFBD4 File Offset: 0x000FDDD4
		public AnonymousIdentificationEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x040028A6 RID: 10406
		private string _AnonymousId;

		// Token: 0x040028A7 RID: 10407
		private HttpContext _Context;
	}
}

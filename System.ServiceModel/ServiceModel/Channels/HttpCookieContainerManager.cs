using System;
using System.Net;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200088F RID: 2191
	internal class HttpCookieContainerManager : IHttpCookieContainerManager
	{
		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x00132CAC File Offset: 0x00130EAC
		// (set) Token: 0x0600533E RID: 21310 RVA: 0x00132CB4 File Offset: 0x00130EB4
		public bool IsInitialized { get; private set; }

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x00132CBD File Offset: 0x00130EBD
		// (set) Token: 0x06005340 RID: 21312 RVA: 0x00132CC5 File Offset: 0x00130EC5
		public CookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.IsInitialized = true;
				this.cookieContainer = value;
			}
		}

		// Token: 0x040032B5 RID: 12981
		private CookieContainer cookieContainer;
	}
}

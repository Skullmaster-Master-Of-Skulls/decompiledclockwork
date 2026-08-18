using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000041 RID: 65
	public sealed class UserLoginInfo
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00006B63 File Offset: 0x00004D63
		public UserLoginInfo(string loginProvider, string providerKey)
		{
			this.LoginProvider = loginProvider;
			this.ProviderKey = providerKey;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006B79 File Offset: 0x00004D79
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006B81 File Offset: 0x00004D81
		public string LoginProvider { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006B8A File Offset: 0x00004D8A
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006B92 File Offset: 0x00004D92
		public string ProviderKey { get; set; }
	}
}

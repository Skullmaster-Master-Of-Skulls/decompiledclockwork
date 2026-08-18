using System;

namespace TechnoPro.Common.Public.Entities.Azure
{
	// Token: 0x02000472 RID: 1138
	public class UsernameClientCredentials : ClientCredentials
	{
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000265F7 File Offset: 0x000247F7
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x000265FF File Offset: 0x000247FF
		public string Username { get; set; }

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x00026608 File Offset: 0x00024808
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x00026610 File Offset: 0x00024810
		public string Password { get; set; }
	}
}

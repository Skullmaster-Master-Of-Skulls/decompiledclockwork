using System;

namespace TechnoPro.Common.Public.Entities.Azure
{
	// Token: 0x02000473 RID: 1139
	public class TokenBasedClientCredentials : ClientCredentials
	{
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x00026622 File Offset: 0x00024822
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0002662A File Offset: 0x0002482A
		public DateTime TokenIssuedDateTime { get; set; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x00026633 File Offset: 0x00024833
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0002663B File Offset: 0x0002483B
		public string Token { get; set; }
	}
}

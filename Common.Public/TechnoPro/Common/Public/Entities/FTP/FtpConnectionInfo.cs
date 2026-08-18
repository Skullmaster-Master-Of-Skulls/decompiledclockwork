using System;

namespace TechnoPro.Common.Public.Entities.FTP
{
	// Token: 0x02000331 RID: 817
	public class FtpConnectionInfo
	{
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0001DFE5 File Offset: 0x0001C1E5
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x0001DFED File Offset: 0x0001C1ED
		public string Host { get; set; }

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0001DFF6 File Offset: 0x0001C1F6
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x0001DFFE File Offset: 0x0001C1FE
		public string Username { get; set; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0001E007 File Offset: 0x0001C207
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x0001E00F File Offset: 0x0001C20F
		public string Password { get; set; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0001E018 File Offset: 0x0001C218
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x0001E020 File Offset: 0x0001C220
		public bool Passive { get; set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0001E029 File Offset: 0x0001C229
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x0001E031 File Offset: 0x0001C231
		public bool AuthTls { get; set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0001E03A File Offset: 0x0001C23A
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x0001E042 File Offset: 0x0001C242
		public bool Ssl { get; set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0001E04B File Offset: 0x0001C24B
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x0001E053 File Offset: 0x0001C253
		public int Port { get; set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0001E05C File Offset: 0x0001C25C
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x0001E064 File Offset: 0x0001C264
		public string RemoteDir { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0001E06D File Offset: 0x0001C26D
		// (set) Token: 0x0600199E RID: 6558 RVA: 0x0001E075 File Offset: 0x0001C275
		public string RootDir { get; set; }
	}
}

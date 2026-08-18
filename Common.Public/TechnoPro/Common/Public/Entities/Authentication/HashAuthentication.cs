using System;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x0200048D RID: 1165
	public class HashAuthentication
	{
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x00026BE9 File Offset: 0x00024DE9
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x00026BF1 File Offset: 0x00024DF1
		public string SecretKey { get; set; }

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00026BFA File Offset: 0x00024DFA
		// (set) Token: 0x0600231B RID: 8987 RVA: 0x00026C02 File Offset: 0x00024E02
		public string Username { get; set; }

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00026C0B File Offset: 0x00024E0B
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x00026C13 File Offset: 0x00024E13
		public string StampTime { get; set; }

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00026C1C File Offset: 0x00024E1C
		// (set) Token: 0x0600231F RID: 8991 RVA: 0x00026C24 File Offset: 0x00024E24
		public string Seed { get; set; }

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x00026C2D File Offset: 0x00024E2D
		// (set) Token: 0x06002321 RID: 8993 RVA: 0x00026C35 File Offset: 0x00024E35
		public string HashValue { get; set; }
	}
}

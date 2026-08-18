using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002A5 RID: 677
	public class AuthenticationSessionInfo
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00019FC8 File Offset: 0x000181C8
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00019FD0 File Offset: 0x000181D0
		public eSessionTokenStatus Status { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00019FD9 File Offset: 0x000181D9
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00019FE1 File Offset: 0x000181E1
		public IList<LogonUserInfo> LogonUsers { get; set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00019FEA File Offset: 0x000181EA
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00019FF2 File Offset: 0x000181F2
		public int MaxAllowConcurrentUsers { get; set; }
	}
}

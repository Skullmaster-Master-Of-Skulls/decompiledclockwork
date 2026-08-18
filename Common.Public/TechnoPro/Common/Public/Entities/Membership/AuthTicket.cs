using System;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002A8 RID: 680
	public class AuthTicket
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0001A038 File Offset: 0x00018238
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0001A040 File Offset: 0x00018240
		public bool IsValid { get; set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0001A051 File Offset: 0x00018251
		public bool IsSessionBased { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0001A05A File Offset: 0x0001825A
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0001A062 File Offset: 0x00018262
		public Guid SessionTicket { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0001A06B File Offset: 0x0001826B
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0001A073 File Offset: 0x00018273
		public string Username { get; set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0001A07C File Offset: 0x0001827C
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0001A084 File Offset: 0x00018284
		public User User { get; set; }
	}
}

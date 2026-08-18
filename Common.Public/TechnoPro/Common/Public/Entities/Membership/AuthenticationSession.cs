using System;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002A4 RID: 676
	public class AuthenticationSession : BusinessBase<Guid>
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00019F62 File Offset: 0x00018162
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00019F6A File Offset: 0x0001816A
		public virtual DateTime IssuedOn { get; set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00019F73 File Offset: 0x00018173
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00019F7B File Offset: 0x0001817B
		public virtual DateTime LastCheckedTime { get; set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00019F84 File Offset: 0x00018184
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00019F8C File Offset: 0x0001818C
		public virtual bool NeverExpires { get; set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00019F95 File Offset: 0x00018195
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00019F9D File Offset: 0x0001819D
		public virtual User User { get; set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00019FA6 File Offset: 0x000181A6
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00019FAE File Offset: 0x000181AE
		public virtual ClientParameters ClientParameters { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00019FB7 File Offset: 0x000181B7
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00019FBF File Offset: 0x000181BF
		public virtual AuthenticationSessionInfo TokenStatus { get; set; }
	}
}

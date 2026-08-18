using System;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002A7 RID: 679
	[Flags]
	public enum eSessionTokenStatus
	{
		// Token: 0x0400114D RID: 4429
		BelowConcurrentUserLimit = 0,
		// Token: 0x0400114E RID: 4430
		AboveConcurrentUserLimit = 1
	}
}

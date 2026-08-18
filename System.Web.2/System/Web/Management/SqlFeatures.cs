using System;

namespace System.Web.Management
{
	// Token: 0x02000176 RID: 374
	[Flags]
	public enum SqlFeatures
	{
		// Token: 0x04001570 RID: 5488
		None = 0,
		// Token: 0x04001571 RID: 5489
		Membership = 1,
		// Token: 0x04001572 RID: 5490
		Profile = 2,
		// Token: 0x04001573 RID: 5491
		RoleManager = 4,
		// Token: 0x04001574 RID: 5492
		Personalization = 8,
		// Token: 0x04001575 RID: 5493
		SqlWebEventProvider = 16,
		// Token: 0x04001576 RID: 5494
		All = 1073741855
	}
}

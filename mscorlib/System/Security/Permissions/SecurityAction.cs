using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200063A RID: 1594
	[ComVisible(true)]
	[Serializable]
	public enum SecurityAction
	{
		// Token: 0x04001DF0 RID: 7664
		Demand = 2,
		// Token: 0x04001DF1 RID: 7665
		Assert,
		// Token: 0x04001DF2 RID: 7666
		Deny,
		// Token: 0x04001DF3 RID: 7667
		PermitOnly,
		// Token: 0x04001DF4 RID: 7668
		LinkDemand,
		// Token: 0x04001DF5 RID: 7669
		InheritanceDemand,
		// Token: 0x04001DF6 RID: 7670
		RequestMinimum,
		// Token: 0x04001DF7 RID: 7671
		RequestOptional,
		// Token: 0x04001DF8 RID: 7672
		RequestRefuse
	}
}

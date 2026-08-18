using System;

namespace System.Reflection
{
	// Token: 0x0200000B RID: 11
	public enum DeclarativeSecurityAction : short
	{
		// Token: 0x0400001D RID: 29
		None,
		// Token: 0x0400001E RID: 30
		Demand = 2,
		// Token: 0x0400001F RID: 31
		Assert,
		// Token: 0x04000020 RID: 32
		Deny,
		// Token: 0x04000021 RID: 33
		PermitOnly,
		// Token: 0x04000022 RID: 34
		LinkDemand,
		// Token: 0x04000023 RID: 35
		InheritanceDemand,
		// Token: 0x04000024 RID: 36
		RequestMinimum,
		// Token: 0x04000025 RID: 37
		RequestOptional,
		// Token: 0x04000026 RID: 38
		RequestRefuse
	}
}

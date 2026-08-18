using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000047 RID: 71
	public enum BuilderStage
	{
		// Token: 0x0400003D RID: 61
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", Justification = "Backwards compatibility")]
		PreCreation,
		// Token: 0x0400003E RID: 62
		Creation,
		// Token: 0x0400003F RID: 63
		Initialization,
		// Token: 0x04000040 RID: 64
		PostInitialization
	}
}

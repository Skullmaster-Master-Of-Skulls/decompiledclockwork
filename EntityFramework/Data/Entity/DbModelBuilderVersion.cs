using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity
{
	// Token: 0x02000735 RID: 1845
	public enum DbModelBuilderVersion
	{
		// Token: 0x04002269 RID: 8809
		Latest,
		// Token: 0x0400226A RID: 8810
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
		V4_1,
		// Token: 0x0400226B RID: 8811
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
		V5_0_Net4,
		// Token: 0x0400226C RID: 8812
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
		V5_0,
		// Token: 0x0400226D RID: 8813
		[SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
		V6_0
	}
}

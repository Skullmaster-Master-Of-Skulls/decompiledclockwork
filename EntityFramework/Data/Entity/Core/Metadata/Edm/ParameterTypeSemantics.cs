using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000521 RID: 1313
	[SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
	public enum ParameterTypeSemantics
	{
		// Token: 0x040012AD RID: 4781
		AllowImplicitConversion,
		// Token: 0x040012AE RID: 4782
		AllowImplicitPromotion,
		// Token: 0x040012AF RID: 4783
		ExactMatchOnly
	}
}

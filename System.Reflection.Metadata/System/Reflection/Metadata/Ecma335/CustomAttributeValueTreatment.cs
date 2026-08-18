using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000115 RID: 277
	[Flags]
	internal enum CustomAttributeValueTreatment : byte
	{
		// Token: 0x04000819 RID: 2073
		None = 0,
		// Token: 0x0400081A RID: 2074
		AttributeUsageAllowSingle = 1,
		// Token: 0x0400081B RID: 2075
		AttributeUsageAllowMultiple = 2,
		// Token: 0x0400081C RID: 2076
		AttributeUsageVersionAttribute = 3,
		// Token: 0x0400081D RID: 2077
		AttributeUsageDeprecatedAttribute = 4
	}
}

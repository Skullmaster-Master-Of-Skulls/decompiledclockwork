using System;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C9 RID: 713
	[Flags]
	internal enum PropagatorFlags : byte
	{
		// Token: 0x040012BA RID: 4794
		NoFlags = 0,
		// Token: 0x040012BB RID: 4795
		Preserve = 1,
		// Token: 0x040012BC RID: 4796
		ConcurrencyValue = 2,
		// Token: 0x040012BD RID: 4797
		Unknown = 8,
		// Token: 0x040012BE RID: 4798
		Key = 16,
		// Token: 0x040012BF RID: 4799
		ForeignKey = 32
	}
}

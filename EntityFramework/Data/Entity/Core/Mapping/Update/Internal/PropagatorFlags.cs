using System;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x02000406 RID: 1030
	[Flags]
	internal enum PropagatorFlags : byte
	{
		// Token: 0x04000E3D RID: 3645
		NoFlags = 0,
		// Token: 0x04000E3E RID: 3646
		Preserve = 1,
		// Token: 0x04000E3F RID: 3647
		ConcurrencyValue = 2,
		// Token: 0x04000E40 RID: 3648
		Unknown = 8,
		// Token: 0x04000E41 RID: 3649
		Key = 16,
		// Token: 0x04000E42 RID: 3650
		ForeignKey = 32
	}
}

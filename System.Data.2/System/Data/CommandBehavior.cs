using System;

namespace System.Data
{
	// Token: 0x02000095 RID: 149
	[Flags]
	public enum CommandBehavior
	{
		// Token: 0x040002C1 RID: 705
		Default = 0,
		// Token: 0x040002C2 RID: 706
		SingleResult = 1,
		// Token: 0x040002C3 RID: 707
		SchemaOnly = 2,
		// Token: 0x040002C4 RID: 708
		KeyInfo = 4,
		// Token: 0x040002C5 RID: 709
		SingleRow = 8,
		// Token: 0x040002C6 RID: 710
		SequentialAccess = 16,
		// Token: 0x040002C7 RID: 711
		CloseConnection = 32
	}
}

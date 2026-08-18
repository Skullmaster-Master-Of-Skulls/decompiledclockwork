using System;

namespace System.Data
{
	// Token: 0x0200005D RID: 93
	[Flags]
	public enum CommandBehavior
	{
		// Token: 0x040006BD RID: 1725
		Default = 0,
		// Token: 0x040006BE RID: 1726
		SingleResult = 1,
		// Token: 0x040006BF RID: 1727
		SchemaOnly = 2,
		// Token: 0x040006C0 RID: 1728
		KeyInfo = 4,
		// Token: 0x040006C1 RID: 1729
		SingleRow = 8,
		// Token: 0x040006C2 RID: 1730
		SequentialAccess = 16,
		// Token: 0x040006C3 RID: 1731
		CloseConnection = 32
	}
}

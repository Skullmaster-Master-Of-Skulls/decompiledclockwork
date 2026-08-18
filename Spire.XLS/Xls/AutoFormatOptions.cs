using System;

namespace Spire.Xls
{
	// Token: 0x020000CC RID: 204
	[Flags]
	public enum AutoFormatOptions
	{
		// Token: 0x04000846 RID: 2118
		Number = 1,
		// Token: 0x04000847 RID: 2119
		Border = 2,
		// Token: 0x04000848 RID: 2120
		Font = 4,
		// Token: 0x04000849 RID: 2121
		Patterns = 8,
		// Token: 0x0400084A RID: 2122
		Alignment = 16,
		// Token: 0x0400084B RID: 2123
		Width_Height = 32,
		// Token: 0x0400084C RID: 2124
		None = 0,
		// Token: 0x0400084D RID: 2125
		All = 63
	}
}

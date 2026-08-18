using System;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x02000016 RID: 22
	public enum RtfTokenType
	{
		// Token: 0x0400006E RID: 110
		None,
		// Token: 0x0400006F RID: 111
		Keyword,
		// Token: 0x04000070 RID: 112
		Control,
		// Token: 0x04000071 RID: 113
		Text,
		// Token: 0x04000072 RID: 114
		Eof,
		// Token: 0x04000073 RID: 115
		GroupStart,
		// Token: 0x04000074 RID: 116
		GroupEnd
	}
}

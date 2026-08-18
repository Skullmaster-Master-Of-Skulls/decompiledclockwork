using System;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x02000048 RID: 72
	[Flags]
	public enum AcceptedCharacters
	{
		// Token: 0x040000DD RID: 221
		None = 0,
		// Token: 0x040000DE RID: 222
		NewLine = 1,
		// Token: 0x040000DF RID: 223
		WhiteSpace = 2,
		// Token: 0x040000E0 RID: 224
		NonWhiteSpace = 4,
		// Token: 0x040000E1 RID: 225
		AllWhiteSpace = 3,
		// Token: 0x040000E2 RID: 226
		Any = 7,
		// Token: 0x040000E3 RID: 227
		AnyExceptNewline = 6
	}
}

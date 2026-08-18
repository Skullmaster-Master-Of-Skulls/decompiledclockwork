using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011EE RID: 4590
	[Flags]
	public enum WordIgnoreOptions
	{
		// Token: 0x040031DC RID: 12764
		None = 0,
		// Token: 0x040031DD RID: 12765
		UPPERCASE = 1,
		// Token: 0x040031DE RID: 12766
		WordsWithCapitalLetters = 2,
		// Token: 0x040031DF RID: 12767
		RepeatedWords = 4,
		// Token: 0x040031E0 RID: 12768
		WordsWithNumbers = 8
	}
}

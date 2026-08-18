using System;

namespace System.Drawing.Printing
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public enum PrintRange
	{
		// Token: 0x040006FB RID: 1787
		AllPages,
		// Token: 0x040006FC RID: 1788
		SomePages = 2,
		// Token: 0x040006FD RID: 1789
		Selection = 1,
		// Token: 0x040006FE RID: 1790
		CurrentPage = 4194304
	}
}

using System;

namespace Ionic.Zip
{
	// Token: 0x02000030 RID: 48
	[Flags]
	public enum ZipEntryTimestamp
	{
		// Token: 0x040000EF RID: 239
		None = 0,
		// Token: 0x040000F0 RID: 240
		DOS = 1,
		// Token: 0x040000F1 RID: 241
		Windows = 2,
		// Token: 0x040000F2 RID: 242
		Unix = 4,
		// Token: 0x040000F3 RID: 243
		InfoZip1 = 8
	}
}

using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000013 RID: 19
	public enum CompressionMethod
	{
		// Token: 0x04000077 RID: 119
		Stored,
		// Token: 0x04000078 RID: 120
		Deflated = 8,
		// Token: 0x04000079 RID: 121
		Deflate64,
		// Token: 0x0400007A RID: 122
		BZip2 = 11,
		// Token: 0x0400007B RID: 123
		WinZipAES = 99
	}
}

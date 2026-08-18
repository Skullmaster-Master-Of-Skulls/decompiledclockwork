using System;

namespace System.IO.Compression
{
	// Token: 0x02000011 RID: 17
	internal enum ZipVersionNeededValues : ushort
	{
		// Token: 0x0400008A RID: 138
		Default = 10,
		// Token: 0x0400008B RID: 139
		ExplicitDirectory = 20,
		// Token: 0x0400008C RID: 140
		Deflate = 20,
		// Token: 0x0400008D RID: 141
		Zip64 = 45
	}
}

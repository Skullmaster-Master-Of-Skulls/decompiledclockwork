using System;

namespace Ionic.Zip
{
	// Token: 0x02000032 RID: 50
	public enum ZipEntrySource
	{
		// Token: 0x040000F9 RID: 249
		None,
		// Token: 0x040000FA RID: 250
		FileSystem,
		// Token: 0x040000FB RID: 251
		Stream,
		// Token: 0x040000FC RID: 252
		ZipFile,
		// Token: 0x040000FD RID: 253
		WriteDelegate,
		// Token: 0x040000FE RID: 254
		JitStream,
		// Token: 0x040000FF RID: 255
		ZipOutputStream
	}
}

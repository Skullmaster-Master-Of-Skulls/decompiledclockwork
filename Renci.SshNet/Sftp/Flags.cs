using System;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000032 RID: 50
	[Flags]
	internal enum Flags
	{
		// Token: 0x04000127 RID: 295
		None = 0,
		// Token: 0x04000128 RID: 296
		Read = 1,
		// Token: 0x04000129 RID: 297
		Write = 2,
		// Token: 0x0400012A RID: 298
		Append = 4,
		// Token: 0x0400012B RID: 299
		CreateNewOrOpen = 8,
		// Token: 0x0400012C RID: 300
		Truncate = 16,
		// Token: 0x0400012D RID: 301
		CreateNew = 40
	}
}

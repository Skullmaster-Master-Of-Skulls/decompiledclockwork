using System;

namespace Renci.SshNet.Sftp
{
	// Token: 0x0200003A RID: 58
	internal enum SftpMessageTypes : byte
	{
		// Token: 0x0400018D RID: 397
		Init = 1,
		// Token: 0x0400018E RID: 398
		Version,
		// Token: 0x0400018F RID: 399
		Open,
		// Token: 0x04000190 RID: 400
		Close,
		// Token: 0x04000191 RID: 401
		Read,
		// Token: 0x04000192 RID: 402
		Write,
		// Token: 0x04000193 RID: 403
		LStat,
		// Token: 0x04000194 RID: 404
		FStat,
		// Token: 0x04000195 RID: 405
		SetStat,
		// Token: 0x04000196 RID: 406
		FSetStat,
		// Token: 0x04000197 RID: 407
		OpenDir,
		// Token: 0x04000198 RID: 408
		ReadDir,
		// Token: 0x04000199 RID: 409
		Remove,
		// Token: 0x0400019A RID: 410
		MkDir,
		// Token: 0x0400019B RID: 411
		RmDir,
		// Token: 0x0400019C RID: 412
		RealPath,
		// Token: 0x0400019D RID: 413
		Stat,
		// Token: 0x0400019E RID: 414
		Rename,
		// Token: 0x0400019F RID: 415
		ReadLink,
		// Token: 0x040001A0 RID: 416
		SymLink,
		// Token: 0x040001A1 RID: 417
		Link,
		// Token: 0x040001A2 RID: 418
		Block,
		// Token: 0x040001A3 RID: 419
		Unblock,
		// Token: 0x040001A4 RID: 420
		Status = 101,
		// Token: 0x040001A5 RID: 421
		Handle,
		// Token: 0x040001A6 RID: 422
		Data,
		// Token: 0x040001A7 RID: 423
		Name,
		// Token: 0x040001A8 RID: 424
		Attrs,
		// Token: 0x040001A9 RID: 425
		Extended = 200,
		// Token: 0x040001AA RID: 426
		ExtendedReply
	}
}

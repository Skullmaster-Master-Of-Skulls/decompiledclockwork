using System;

namespace Renci.SshNet.Sftp
{
	// Token: 0x0200003E RID: 62
	internal enum StatusCodes : uint
	{
		// Token: 0x040001B8 RID: 440
		Ok,
		// Token: 0x040001B9 RID: 441
		Eof,
		// Token: 0x040001BA RID: 442
		NoSuchFile,
		// Token: 0x040001BB RID: 443
		PermissionDenied,
		// Token: 0x040001BC RID: 444
		Failure,
		// Token: 0x040001BD RID: 445
		BadMessage,
		// Token: 0x040001BE RID: 446
		NoConnection,
		// Token: 0x040001BF RID: 447
		ConnectionLost,
		// Token: 0x040001C0 RID: 448
		OperationUnsupported,
		// Token: 0x040001C1 RID: 449
		InvalidHandle,
		// Token: 0x040001C2 RID: 450
		NoSuchPath,
		// Token: 0x040001C3 RID: 451
		FileAlreadyExists,
		// Token: 0x040001C4 RID: 452
		WriteProtect,
		// Token: 0x040001C5 RID: 453
		NoMedia,
		// Token: 0x040001C6 RID: 454
		NoSpaceOnFilesystem,
		// Token: 0x040001C7 RID: 455
		QuotaExceeded,
		// Token: 0x040001C8 RID: 456
		UnknownPrincipal,
		// Token: 0x040001C9 RID: 457
		LockConflict,
		// Token: 0x040001CA RID: 458
		DirNotEmpty,
		// Token: 0x040001CB RID: 459
		NotDirectory,
		// Token: 0x040001CC RID: 460
		InvalidFilename,
		// Token: 0x040001CD RID: 461
		LinkLoop,
		// Token: 0x040001CE RID: 462
		CannotDelete,
		// Token: 0x040001CF RID: 463
		InvalidParameter,
		// Token: 0x040001D0 RID: 464
		FileIsADirectory,
		// Token: 0x040001D1 RID: 465
		ByteRangeLockConflict,
		// Token: 0x040001D2 RID: 466
		ByteRangeLockRefused,
		// Token: 0x040001D3 RID: 467
		DeletePending,
		// Token: 0x040001D4 RID: 468
		FileCorrupt,
		// Token: 0x040001D5 RID: 469
		OwnerInvalid,
		// Token: 0x040001D6 RID: 470
		GroupInvalid,
		// Token: 0x040001D7 RID: 471
		NoMatchingByteRangeLock
	}
}

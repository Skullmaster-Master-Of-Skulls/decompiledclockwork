using System;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x02000400 RID: 1024
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal static class Direct
	{
		// Token: 0x040020D7 RID: 8407
		public const int FILE_ACTION_ADDED = 1;

		// Token: 0x040020D8 RID: 8408
		public const int FILE_ACTION_REMOVED = 2;

		// Token: 0x040020D9 RID: 8409
		public const int FILE_ACTION_MODIFIED = 3;

		// Token: 0x040020DA RID: 8410
		public const int FILE_ACTION_RENAMED_OLD_NAME = 4;

		// Token: 0x040020DB RID: 8411
		public const int FILE_ACTION_RENAMED_NEW_NAME = 5;

		// Token: 0x040020DC RID: 8412
		public const int FILE_NOTIFY_CHANGE_FILE_NAME = 1;

		// Token: 0x040020DD RID: 8413
		public const int FILE_NOTIFY_CHANGE_DIR_NAME = 2;

		// Token: 0x040020DE RID: 8414
		public const int FILE_NOTIFY_CHANGE_NAME = 3;

		// Token: 0x040020DF RID: 8415
		public const int FILE_NOTIFY_CHANGE_ATTRIBUTES = 4;

		// Token: 0x040020E0 RID: 8416
		public const int FILE_NOTIFY_CHANGE_SIZE = 8;

		// Token: 0x040020E1 RID: 8417
		public const int FILE_NOTIFY_CHANGE_LAST_WRITE = 16;

		// Token: 0x040020E2 RID: 8418
		public const int FILE_NOTIFY_CHANGE_LAST_ACCESS = 32;

		// Token: 0x040020E3 RID: 8419
		public const int FILE_NOTIFY_CHANGE_CREATION = 64;

		// Token: 0x040020E4 RID: 8420
		public const int FILE_NOTIFY_CHANGE_SECURITY = 256;
	}
}

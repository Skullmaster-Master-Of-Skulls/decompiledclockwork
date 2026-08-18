using System;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x0200072D RID: 1837
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal static class Direct
	{
		// Token: 0x04003222 RID: 12834
		public const int FILE_ACTION_ADDED = 1;

		// Token: 0x04003223 RID: 12835
		public const int FILE_ACTION_REMOVED = 2;

		// Token: 0x04003224 RID: 12836
		public const int FILE_ACTION_MODIFIED = 3;

		// Token: 0x04003225 RID: 12837
		public const int FILE_ACTION_RENAMED_OLD_NAME = 4;

		// Token: 0x04003226 RID: 12838
		public const int FILE_ACTION_RENAMED_NEW_NAME = 5;

		// Token: 0x04003227 RID: 12839
		public const int FILE_NOTIFY_CHANGE_FILE_NAME = 1;

		// Token: 0x04003228 RID: 12840
		public const int FILE_NOTIFY_CHANGE_DIR_NAME = 2;

		// Token: 0x04003229 RID: 12841
		public const int FILE_NOTIFY_CHANGE_NAME = 3;

		// Token: 0x0400322A RID: 12842
		public const int FILE_NOTIFY_CHANGE_ATTRIBUTES = 4;

		// Token: 0x0400322B RID: 12843
		public const int FILE_NOTIFY_CHANGE_SIZE = 8;

		// Token: 0x0400322C RID: 12844
		public const int FILE_NOTIFY_CHANGE_LAST_WRITE = 16;

		// Token: 0x0400322D RID: 12845
		public const int FILE_NOTIFY_CHANGE_LAST_ACCESS = 32;

		// Token: 0x0400322E RID: 12846
		public const int FILE_NOTIFY_CHANGE_CREATION = 64;

		// Token: 0x0400322F RID: 12847
		public const int FILE_NOTIFY_CHANGE_SECURITY = 256;
	}
}

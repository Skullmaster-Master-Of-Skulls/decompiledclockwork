using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200091F RID: 2335
	[Flags]
	public enum EventWaitHandleRights
	{
		// Token: 0x04002BC6 RID: 11206
		Modify = 2,
		// Token: 0x04002BC7 RID: 11207
		Delete = 65536,
		// Token: 0x04002BC8 RID: 11208
		ReadPermissions = 131072,
		// Token: 0x04002BC9 RID: 11209
		ChangePermissions = 262144,
		// Token: 0x04002BCA RID: 11210
		TakeOwnership = 524288,
		// Token: 0x04002BCB RID: 11211
		Synchronize = 1048576,
		// Token: 0x04002BCC RID: 11212
		FullControl = 2031619
	}
}

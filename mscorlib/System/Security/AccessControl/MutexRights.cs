using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000929 RID: 2345
	[Flags]
	public enum MutexRights
	{
		// Token: 0x04002BE7 RID: 11239
		Modify = 1,
		// Token: 0x04002BE8 RID: 11240
		Delete = 65536,
		// Token: 0x04002BE9 RID: 11241
		ReadPermissions = 131072,
		// Token: 0x04002BEA RID: 11242
		ChangePermissions = 262144,
		// Token: 0x04002BEB RID: 11243
		TakeOwnership = 524288,
		// Token: 0x04002BEC RID: 11244
		Synchronize = 1048576,
		// Token: 0x04002BED RID: 11245
		FullControl = 2031617
	}
}

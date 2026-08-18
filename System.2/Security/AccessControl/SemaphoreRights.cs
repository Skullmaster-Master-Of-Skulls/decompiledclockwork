using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	// Token: 0x0200048C RID: 1164
	[Flags]
	[ComVisible(false)]
	public enum SemaphoreRights
	{
		// Token: 0x0400267B RID: 9851
		Modify = 2,
		// Token: 0x0400267C RID: 9852
		Delete = 65536,
		// Token: 0x0400267D RID: 9853
		ReadPermissions = 131072,
		// Token: 0x0400267E RID: 9854
		ChangePermissions = 262144,
		// Token: 0x0400267F RID: 9855
		TakeOwnership = 524288,
		// Token: 0x04002680 RID: 9856
		Synchronize = 1048576,
		// Token: 0x04002681 RID: 9857
		FullControl = 2031619
	}
}

using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000252 RID: 594
	[Flags]
	internal enum PrivilegeAttribute : uint
	{
		// Token: 0x04001932 RID: 6450
		SE_PRIVILEGE_DISABLED = 0U,
		// Token: 0x04001933 RID: 6451
		SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1U,
		// Token: 0x04001934 RID: 6452
		SE_PRIVILEGE_ENABLED = 2U,
		// Token: 0x04001935 RID: 6453
		SE_PRIVILEGE_REMOVED = 4U,
		// Token: 0x04001936 RID: 6454
		SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648U
	}
}

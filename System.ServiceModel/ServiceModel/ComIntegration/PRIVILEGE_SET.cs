using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000254 RID: 596
	[StructLayout(LayoutKind.Sequential)]
	internal class PRIVILEGE_SET
	{
		// Token: 0x04001939 RID: 6457
		internal uint PrivilegeCount = 1U;

		// Token: 0x0400193A RID: 6458
		internal uint Control;

		// Token: 0x0400193B RID: 6459
		internal LUID_AND_ATTRIBUTES Privilege;
	}
}

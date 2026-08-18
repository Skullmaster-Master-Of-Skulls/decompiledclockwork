using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000059 RID: 89
	internal struct TOKEN_PRIVILEGE
	{
		// Token: 0x040002F1 RID: 753
		internal uint PrivilegeCount;

		// Token: 0x040002F2 RID: 754
		internal LUID_AND_ATTRIBUTES Privilege;

		// Token: 0x040002F3 RID: 755
		internal static readonly uint Size = (uint)Marshal.SizeOf(typeof(TOKEN_PRIVILEGE));
	}
}

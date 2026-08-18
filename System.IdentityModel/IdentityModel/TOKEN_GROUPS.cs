using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000055 RID: 85
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct TOKEN_GROUPS
	{
		// Token: 0x040002E5 RID: 741
		internal uint GroupCount;

		// Token: 0x040002E6 RID: 742
		internal SID_AND_ATTRIBUTES Groups;
	}
}

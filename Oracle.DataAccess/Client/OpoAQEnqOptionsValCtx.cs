using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000103 RID: 259
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQEnqOptionsValCtx
	{
		// Token: 0x04000866 RID: 2150
		internal int isDirty;

		// Token: 0x04000867 RID: 2151
		internal int deliveryMode;

		// Token: 0x04000868 RID: 2152
		internal int visibility;
	}
}

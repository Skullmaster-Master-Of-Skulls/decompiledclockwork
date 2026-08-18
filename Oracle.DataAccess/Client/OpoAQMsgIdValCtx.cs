using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQMsgIdValCtx
	{
		// Token: 0x04000895 RID: 2197
		internal IntPtr pMsgId;

		// Token: 0x04000896 RID: 2198
		internal int msgIdLen;

		// Token: 0x04000897 RID: 2199
		internal IntPtr pMsgIdObject;
	}
}

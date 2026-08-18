using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000107 RID: 263
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoAQMsgPropsRefCtx
	{
		// Token: 0x04000886 RID: 2182
		internal string exceptionQueue;

		// Token: 0x04000887 RID: 2183
		internal string correlationId;

		// Token: 0x04000888 RID: 2184
		internal string transNo;

		// Token: 0x04000889 RID: 2185
		internal OpoAQAgentRefCtx senderId;
	}
}

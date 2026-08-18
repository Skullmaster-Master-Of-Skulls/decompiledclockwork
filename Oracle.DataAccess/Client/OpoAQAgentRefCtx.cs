using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200010B RID: 267
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQAgentRefCtx
	{
		// Token: 0x040008A0 RID: 2208
		internal string name;

		// Token: 0x040008A1 RID: 2209
		internal string address;
	}
}

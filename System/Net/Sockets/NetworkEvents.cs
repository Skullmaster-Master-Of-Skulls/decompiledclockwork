using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x020005A8 RID: 1448
	internal struct NetworkEvents
	{
		// Token: 0x04002AAE RID: 10926
		public AsyncEventBits Events;

		// Token: 0x04002AAF RID: 10927
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public int[] ErrorCodes;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x02000369 RID: 873
	internal struct NetworkEvents
	{
		// Token: 0x04001DBA RID: 7610
		public AsyncEventBits Events;

		// Token: 0x04001DBB RID: 7611
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public int[] ErrorCodes;
	}
}

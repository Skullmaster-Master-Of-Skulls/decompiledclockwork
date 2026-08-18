using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D5 RID: 469
	[StructLayout(LayoutKind.Sequential)]
	internal class TransmitFileBuffers
	{
		// Token: 0x040014E0 RID: 5344
		internal IntPtr preBuffer;

		// Token: 0x040014E1 RID: 5345
		internal int preBufferLength;

		// Token: 0x040014E2 RID: 5346
		internal IntPtr postBuffer;

		// Token: 0x040014E3 RID: 5347
		internal int postBufferLength;
	}
}

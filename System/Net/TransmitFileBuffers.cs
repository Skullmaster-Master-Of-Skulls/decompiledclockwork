using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020004FE RID: 1278
	[StructLayout(LayoutKind.Sequential)]
	internal class TransmitFileBuffers
	{
		// Token: 0x04002722 RID: 10018
		internal IntPtr preBuffer;

		// Token: 0x04002723 RID: 10019
		internal int preBufferLength;

		// Token: 0x04002724 RID: 10020
		internal IntPtr postBuffer;

		// Token: 0x04002725 RID: 10021
		internal int postBufferLength;
	}
}

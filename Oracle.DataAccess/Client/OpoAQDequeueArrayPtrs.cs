using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQDequeueArrayPtrs
	{
		// Token: 0x04000898 RID: 2200
		internal IntPtr ppOCIAQMsgProperties;

		// Token: 0x04000899 RID: 2201
		internal IntPtr pInd;

		// Token: 0x0400089A RID: 2202
		internal IntPtr ppOCIInd;

		// Token: 0x0400089B RID: 2203
		internal IntPtr ppUDTNullInd;

		// Token: 0x0400089C RID: 2204
		internal IntPtr deqmesg;

		// Token: 0x0400089D RID: 2205
		internal IntPtr ppUDTArray;

		// Token: 0x0400089E RID: 2206
		internal IntPtr ppXMLArray;

		// Token: 0x0400089F RID: 2207
		internal IntPtr ppMsgIdObject;
	}
}

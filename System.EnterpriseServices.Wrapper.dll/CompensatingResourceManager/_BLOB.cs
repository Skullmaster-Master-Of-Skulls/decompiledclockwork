using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	// Token: 0x0200007F RID: 127
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct _BLOB
	{
		// Token: 0x04000142 RID: 322
		public int cbSize;

		// Token: 0x04000143 RID: 323
		public IntPtr pBlobData;
	}
}

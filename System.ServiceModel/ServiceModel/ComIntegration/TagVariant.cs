using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200024C RID: 588
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct TagVariant
	{
		// Token: 0x040018FC RID: 6396
		public ushort vt;

		// Token: 0x040018FD RID: 6397
		public ushort reserved1;

		// Token: 0x040018FE RID: 6398
		public ushort reserved2;

		// Token: 0x040018FF RID: 6399
		public ushort reserved3;

		// Token: 0x04001900 RID: 6400
		public IntPtr ptr;

		// Token: 0x04001901 RID: 6401
		public IntPtr pRecInfo;
	}
}

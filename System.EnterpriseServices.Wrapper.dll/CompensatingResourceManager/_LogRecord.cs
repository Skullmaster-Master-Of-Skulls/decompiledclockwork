using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	// Token: 0x02000080 RID: 128
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct _LogRecord
	{
		// Token: 0x04000144 RID: 324
		public int dwCrmFlags;

		// Token: 0x04000145 RID: 325
		public int dwSequenceNumber;

		// Token: 0x04000146 RID: 326
		public _BLOB blobUserData;
	}
}

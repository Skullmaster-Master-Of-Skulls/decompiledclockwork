using System;

namespace System.Web.Hosting
{
	// Token: 0x020002A2 RID: 674
	internal enum BufferType : byte
	{
		// Token: 0x04001B95 RID: 7061
		Managed,
		// Token: 0x04001B96 RID: 7062
		UnmanagedPool,
		// Token: 0x04001B97 RID: 7063
		IISAllocatedRequestMemory,
		// Token: 0x04001B98 RID: 7064
		TransmitFile
	}
}

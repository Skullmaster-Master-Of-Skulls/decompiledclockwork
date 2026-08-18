using System;

namespace System.Web.Hosting
{
	// Token: 0x020007C5 RID: 1989
	internal enum BufferType : byte
	{
		// Token: 0x040031AD RID: 12717
		Managed,
		// Token: 0x040031AE RID: 12718
		UnmanagedPool,
		// Token: 0x040031AF RID: 12719
		IISAllocatedRequestMemory,
		// Token: 0x040031B0 RID: 12720
		TransmitFile
	}
}

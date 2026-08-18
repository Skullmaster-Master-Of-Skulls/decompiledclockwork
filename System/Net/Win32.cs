using System;

namespace System.Net
{
	// Token: 0x0200055B RID: 1371
	internal static class Win32
	{
		// Token: 0x0400288A RID: 10378
		internal const int OverlappedInternalOffset = 0;

		// Token: 0x0400288B RID: 10379
		internal static int OverlappedInternalHighOffset = IntPtr.Size;

		// Token: 0x0400288C RID: 10380
		internal static int OverlappedOffsetOffset = IntPtr.Size * 2;

		// Token: 0x0400288D RID: 10381
		internal static int OverlappedOffsetHighOffset = IntPtr.Size * 2 + 4;

		// Token: 0x0400288E RID: 10382
		internal static int OverlappedhEventOffset = IntPtr.Size * 2 + 8;

		// Token: 0x0400288F RID: 10383
		internal static int OverlappedSize = IntPtr.Size * 3 + 8;
	}
}

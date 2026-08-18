using System;

namespace System.Net
{
	// Token: 0x0200021E RID: 542
	internal static class Win32
	{
		// Token: 0x04001605 RID: 5637
		internal const int OverlappedInternalOffset = 0;

		// Token: 0x04001606 RID: 5638
		internal static int OverlappedInternalHighOffset = IntPtr.Size;

		// Token: 0x04001607 RID: 5639
		internal static int OverlappedOffsetOffset = IntPtr.Size * 2;

		// Token: 0x04001608 RID: 5640
		internal static int OverlappedOffsetHighOffset = IntPtr.Size * 2 + 4;

		// Token: 0x04001609 RID: 5641
		internal static int OverlappedhEventOffset = IntPtr.Size * 2 + 8;

		// Token: 0x0400160A RID: 5642
		internal static int OverlappedSize = IntPtr.Size * 3 + 8;
	}
}

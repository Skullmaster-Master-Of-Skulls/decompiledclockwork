using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000D7 RID: 215
	internal static class StringHandleType
	{
		// Token: 0x04000612 RID: 1554
		internal const uint TypeMask = 3758096384U;

		// Token: 0x04000613 RID: 1555
		internal const uint NonVirtualTypeMask = 1610612736U;

		// Token: 0x04000614 RID: 1556
		internal const uint String = 0U;

		// Token: 0x04000615 RID: 1557
		internal const uint DotTerminatedString = 536870912U;

		// Token: 0x04000616 RID: 1558
		internal const uint ReservedString1 = 1073741824U;

		// Token: 0x04000617 RID: 1559
		internal const uint ReservedString2 = 1610612736U;

		// Token: 0x04000618 RID: 1560
		internal const uint VirtualString = 2147483648U;

		// Token: 0x04000619 RID: 1561
		internal const uint WinRTPrefixedString = 2684354560U;

		// Token: 0x0400061A RID: 1562
		internal const uint ReservedVirtualString1 = 3221225472U;

		// Token: 0x0400061B RID: 1563
		internal const uint ReservedVirtualString2 = 3758096384U;
	}
}

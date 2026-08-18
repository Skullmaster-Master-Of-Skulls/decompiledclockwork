using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004DA RID: 1242
	[Flags]
	internal enum DeviceContextLayout
	{
		// Token: 0x04003536 RID: 13622
		Normal = 0,
		// Token: 0x04003537 RID: 13623
		RightToLeft = 1,
		// Token: 0x04003538 RID: 13624
		BottomToTop = 2,
		// Token: 0x04003539 RID: 13625
		VerticalBeforeHorizontal = 4,
		// Token: 0x0400353A RID: 13626
		BitmapOrientationPreserved = 8
	}
}

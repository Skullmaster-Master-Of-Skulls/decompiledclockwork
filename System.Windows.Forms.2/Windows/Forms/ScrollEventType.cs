using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200035D RID: 861
	[ComVisible(true)]
	public enum ScrollEventType
	{
		// Token: 0x040021A3 RID: 8611
		SmallDecrement,
		// Token: 0x040021A4 RID: 8612
		SmallIncrement,
		// Token: 0x040021A5 RID: 8613
		LargeDecrement,
		// Token: 0x040021A6 RID: 8614
		LargeIncrement,
		// Token: 0x040021A7 RID: 8615
		ThumbPosition,
		// Token: 0x040021A8 RID: 8616
		ThumbTrack,
		// Token: 0x040021A9 RID: 8617
		First,
		// Token: 0x040021AA RID: 8618
		Last,
		// Token: 0x040021AB RID: 8619
		EndScroll
	}
}

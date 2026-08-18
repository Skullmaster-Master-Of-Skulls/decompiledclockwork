using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200031B RID: 795
	public interface IChartTextArea : IFont
	{
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06003126 RID: 12582
		// (set) Token: 0x06003127 RID: 12583
		string Text { get; set; }

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003128 RID: 12584
		// (set) Token: 0x06003129 RID: 12585
		int TextRotationAngle { get; set; }

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600312A RID: 12586
		IChartFrameFormat FrameFormat { get; }

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600312B RID: 12587
		// (set) Token: 0x0600312C RID: 12588
		ChartBackgroundMode BackgroundMode { get; set; }

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600312D RID: 12589
		// (set) Token: 0x0600312E RID: 12590
		bool IsAutoMode { get; set; }
	}
}

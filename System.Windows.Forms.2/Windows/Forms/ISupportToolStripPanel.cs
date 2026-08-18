using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A5 RID: 677
	internal interface ISupportToolStripPanel
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002A37 RID: 10807
		// (set) Token: 0x06002A38 RID: 10808
		ToolStripPanelRow ToolStripPanelRow { get; set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002A39 RID: 10809
		ToolStripPanelCell ToolStripPanelCell { get; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002A3A RID: 10810
		// (set) Token: 0x06002A3B RID: 10811
		bool Stretch { get; set; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002A3C RID: 10812
		bool IsCurrentlyDragging { get; }

		// Token: 0x06002A3D RID: 10813
		void BeginDrag();

		// Token: 0x06002A3E RID: 10814
		void EndDrag();
	}
}

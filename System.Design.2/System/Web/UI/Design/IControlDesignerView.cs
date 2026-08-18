using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Web.UI.Design
{
	// Token: 0x02000049 RID: 73
	public interface IControlDesignerView
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000276 RID: 630
		DesignerRegion ContainingRegion { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000277 RID: 631
		IDesigner NamingContainerDesigner { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000278 RID: 632
		bool SupportsRegions { get; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000279 RID: 633
		// (remove) Token: 0x0600027A RID: 634
		event ViewEventHandler ViewEvent;

		// Token: 0x0600027B RID: 635
		Rectangle GetBounds(DesignerRegion region);

		// Token: 0x0600027C RID: 636
		void Invalidate(Rectangle rectangle);

		// Token: 0x0600027D RID: 637
		void SetFlags(ViewFlags viewFlags, bool setFlag);

		// Token: 0x0600027E RID: 638
		void SetRegionContent(EditableDesignerRegion region, string content);

		// Token: 0x0600027F RID: 639
		void Update();
	}
}

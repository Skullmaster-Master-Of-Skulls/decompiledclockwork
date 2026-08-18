using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections
{
	// Token: 0x0200050A RID: 1290
	[ParseChildren(typeof(BubbleSeriesItem))]
	public class BubbleSeriesItemCollection : StronglyTypedStateManagedCollection<BubbleSeriesItem>
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x00097F78 File Offset: 0x00096178
		public new void Add(BubbleSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x00097F88 File Offset: 0x00096188
		public void Add(decimal? x, decimal? y, decimal? size)
		{
			BubbleSeriesItem item = new BubbleSeriesItem(x, y, size);
			base.Add(item);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x00097FA8 File Offset: 0x000961A8
		public void Add(decimal? x, decimal? y, decimal? size, string tooltip)
		{
			BubbleSeriesItem item = new BubbleSeriesItem(x, y, size, tooltip);
			base.Add(item);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x00097FC8 File Offset: 0x000961C8
		public void Add(decimal? x, decimal? y, decimal? size, string tooltip, Color backgroundColor)
		{
			BubbleSeriesItem item = new BubbleSeriesItem(x, y, size, tooltip, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x00097FE9 File Offset: 0x000961E9
		protected override void SetDirtyObject(object o)
		{
			if (o is BubbleSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

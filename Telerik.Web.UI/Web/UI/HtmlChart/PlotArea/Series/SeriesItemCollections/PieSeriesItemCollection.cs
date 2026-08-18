using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections
{
	// Token: 0x0200050E RID: 1294
	[ParseChildren(typeof(PieSeriesItem))]
	public class PieSeriesItemCollection : StronglyTypedStateManagedCollection<PieSeriesItem>
	{
		// Token: 0x06002E52 RID: 11858 RVA: 0x00098169 File Offset: 0x00096369
		public new void Add(PieSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x0009817C File Offset: 0x0009637C
		public void Add(decimal? y)
		{
			PieSeriesItem item = new PieSeriesItem(y);
			base.Add(item);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x00098198 File Offset: 0x00096398
		public void Add(decimal? y, Color backgroundColor)
		{
			PieSeriesItem item = new PieSeriesItem(y, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000981B4 File Offset: 0x000963B4
		public void Add(decimal? y, Color backgroundColor, string name)
		{
			PieSeriesItem item = new PieSeriesItem(y, backgroundColor, name);
			base.Add(item);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000981D4 File Offset: 0x000963D4
		public void Add(decimal? y, Color backgroundColor, string name, bool exploded)
		{
			PieSeriesItem item = new PieSeriesItem(y, backgroundColor, name, exploded);
			base.Add(item);
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000981F4 File Offset: 0x000963F4
		public void Add(decimal? y, Color backgroundColor, string name, bool exploded, bool visible)
		{
			PieSeriesItem item = new PieSeriesItem(y, backgroundColor, name, exploded, visible);
			base.Add(item);
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x00098218 File Offset: 0x00096418
		public void Add(decimal? y, Color backgroundColor, string name, bool exploded, bool visible, bool visibleInLegend)
		{
			PieSeriesItem item = new PieSeriesItem(y, backgroundColor, name, exploded, visible, visibleInLegend);
			base.Add(item);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x0009823B File Offset: 0x0009643B
		protected override void SetDirtyObject(object o)
		{
			if (o is PieSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

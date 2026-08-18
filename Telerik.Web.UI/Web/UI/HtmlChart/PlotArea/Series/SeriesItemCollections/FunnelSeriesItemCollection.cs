using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections
{
	// Token: 0x0200050D RID: 1293
	[ParseChildren(typeof(FunnelSeriesItem))]
	public class FunnelSeriesItemCollection : StronglyTypedStateManagedCollection<FunnelSeriesItem>
	{
		// Token: 0x06002E4D RID: 11853 RVA: 0x00098101 File Offset: 0x00096301
		public new void Add(FunnelSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00098114 File Offset: 0x00096314
		public void Add(decimal? y)
		{
			FunnelSeriesItem item = new FunnelSeriesItem(y);
			base.Add(item);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00098130 File Offset: 0x00096330
		public void Add(decimal? y, Color backgroundColor)
		{
			FunnelSeriesItem item = new FunnelSeriesItem(y, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x0009814C File Offset: 0x0009634C
		protected override void SetDirtyObject(object o)
		{
			if (o is FunnelSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

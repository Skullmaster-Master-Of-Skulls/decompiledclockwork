using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections
{
	// Token: 0x0200050F RID: 1295
	[ParseChildren(typeof(ScatterSeriesItem))]
	public class ScatterSeriesItemCollection : StronglyTypedStateManagedCollection<ScatterSeriesItem>
	{
		// Token: 0x06002E5B RID: 11867 RVA: 0x00098258 File Offset: 0x00096458
		public new void Add(ScatterSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x00098268 File Offset: 0x00096468
		public void Add(decimal? x, decimal? y)
		{
			ScatterSeriesItem item = new ScatterSeriesItem(x, y);
			base.Add(item);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x00098284 File Offset: 0x00096484
		public void Add(decimal? x, decimal? y, Color backgroundColor)
		{
			ScatterSeriesItem item = new ScatterSeriesItem(x, y, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000982A1 File Offset: 0x000964A1
		protected override void SetDirtyObject(object o)
		{
			if (o is ScatterSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

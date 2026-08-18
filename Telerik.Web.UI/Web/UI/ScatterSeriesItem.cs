using System;
using System.Drawing;
using Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems;

namespace Telerik.Web.UI
{
	// Token: 0x02000516 RID: 1302
	public class ScatterSeriesItem : TwoValueSeriesItem
	{
		// Token: 0x06002E9C RID: 11932 RVA: 0x00098832 File Offset: 0x00096A32
		public ScatterSeriesItem()
		{
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0009883A File Offset: 0x00096A3A
		public ScatterSeriesItem(decimal? x, decimal? y)
		{
			base.X = x;
			base.Y = y;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x00098850 File Offset: 0x00096A50
		public ScatterSeriesItem(decimal? x, decimal? y, Color backgroundColor) : this(x, y)
		{
			base.BackgroundColor = backgroundColor;
		}
	}
}

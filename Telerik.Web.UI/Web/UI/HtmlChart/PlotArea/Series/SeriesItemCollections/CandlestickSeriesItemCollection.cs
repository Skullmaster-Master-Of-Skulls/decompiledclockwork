using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItemCollections
{
	// Token: 0x0200050B RID: 1291
	[ParseChildren(typeof(CandlestickSeriesItem))]
	public class CandlestickSeriesItemCollection : StronglyTypedStateManagedCollection<CandlestickSeriesItem>
	{
		// Token: 0x06002E42 RID: 11842 RVA: 0x00098006 File Offset: 0x00096206
		public new void Add(CandlestickSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x00098018 File Offset: 0x00096218
		public void Add(decimal? open, decimal? close, decimal? high, decimal? low)
		{
			CandlestickSeriesItem item = new CandlestickSeriesItem(open, close, high, low);
			base.Add(item);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x00098038 File Offset: 0x00096238
		public void Add(decimal? open, decimal? close, decimal? high, decimal? low, Color backgroundColor)
		{
			CandlestickSeriesItem item = new CandlestickSeriesItem(open, close, high, low, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x0009805C File Offset: 0x0009625C
		public void Add(Color downColor, decimal? open, decimal? close, decimal? high, decimal? low)
		{
			CandlestickSeriesItem item = new CandlestickSeriesItem(downColor, open, close, high, low);
			base.Add(item);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x0009807D File Offset: 0x0009627D
		protected override void SetDirtyObject(object o)
		{
			if (o is CandlestickSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

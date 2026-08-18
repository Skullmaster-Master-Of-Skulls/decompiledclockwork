using System;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.SeriesItemsCollection
{
	// Token: 0x020003EB RID: 1003
	[ParseChildren(typeof(WaterfallSeriesItem))]
	public class WaterfallSeriesItemCollection : StronglyTypedStateManagedCollection<WaterfallSeriesItem>
	{
		// Token: 0x060024E6 RID: 9446 RVA: 0x0007B193 File Offset: 0x00079393
		public new void Add(WaterfallSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0007B1A4 File Offset: 0x000793A4
		public void Add(SummaryType summaryType)
		{
			WaterfallSeriesItem item = new WaterfallSeriesItem(summaryType);
			base.Add(item);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0007B1BF File Offset: 0x000793BF
		protected override void SetDirtyObject(object o)
		{
			if (o is WaterfallSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

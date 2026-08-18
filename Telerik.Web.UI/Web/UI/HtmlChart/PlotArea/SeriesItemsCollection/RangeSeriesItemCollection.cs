using System;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.SeriesItemsCollection
{
	// Token: 0x020003EC RID: 1004
	[ParseChildren(typeof(CategorySeriesItem))]
	public class RangeSeriesItemCollection : StronglyTypedStateManagedCollection<RangeSeriesItem>
	{
		// Token: 0x060024EA RID: 9450 RVA: 0x0007B1DC File Offset: 0x000793DC
		public new void Add(RangeSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0007B1EC File Offset: 0x000793EC
		public void Add(decimal? fromValue, decimal? toValue)
		{
			RangeSeriesItem item = new RangeSeriesItem(fromValue, toValue);
			base.Add(item);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0007B208 File Offset: 0x00079408
		protected override void SetDirtyObject(object o)
		{
			if (o is RangeSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

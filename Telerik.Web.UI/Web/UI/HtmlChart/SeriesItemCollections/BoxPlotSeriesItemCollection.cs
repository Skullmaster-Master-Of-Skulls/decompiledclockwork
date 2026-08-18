using System;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.SeriesItemCollections
{
	// Token: 0x020004F4 RID: 1268
	[ParseChildren(typeof(BoxPlotSeriesItem))]
	public class BoxPlotSeriesItemCollection : StronglyTypedStateManagedCollection<BoxPlotSeriesItem>
	{
		// Token: 0x06002D3A RID: 11578 RVA: 0x000949B2 File Offset: 0x00092BB2
		public new void Add(BoxPlotSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000949C2 File Offset: 0x00092BC2
		protected override void SetDirtyObject(object obj)
		{
			if (obj is BoxPlotSeriesItem)
			{
				((StateManager)obj).SetDirty();
			}
		}
	}
}

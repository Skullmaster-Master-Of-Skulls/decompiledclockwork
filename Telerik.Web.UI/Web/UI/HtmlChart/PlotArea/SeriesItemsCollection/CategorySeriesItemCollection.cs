using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.SeriesItemsCollection
{
	// Token: 0x0200050C RID: 1292
	[ParseChildren(typeof(CategorySeriesItem))]
	public class CategorySeriesItemCollection : StronglyTypedStateManagedCollection<CategorySeriesItem>
	{
		// Token: 0x06002E48 RID: 11848 RVA: 0x0009809A File Offset: 0x0009629A
		public new void Add(CategorySeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000980AC File Offset: 0x000962AC
		public void Add(decimal? y)
		{
			CategorySeriesItem item = new CategorySeriesItem(y);
			base.Add(item);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000980C8 File Offset: 0x000962C8
		public void Add(decimal? y, Color backgroundColor)
		{
			CategorySeriesItem item = new CategorySeriesItem(y, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000980E4 File Offset: 0x000962E4
		protected override void SetDirtyObject(object o)
		{
			if (o is CategorySeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

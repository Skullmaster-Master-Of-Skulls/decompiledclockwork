using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.SeriesItemCollections
{
	// Token: 0x020004F6 RID: 1270
	[ParseChildren(typeof(PolarSeriesItem))]
	public class PolarSeriesItemCollection : StronglyTypedStateManagedCollection<PolarSeriesItem>
	{
		// Token: 0x06002D44 RID: 11588 RVA: 0x00094B18 File Offset: 0x00092D18
		public new void Add(PolarSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00094B28 File Offset: 0x00092D28
		public void Add(decimal? angle, decimal? radius)
		{
			PolarSeriesItem item = new PolarSeriesItem(angle, radius);
			base.Add(item);
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x00094B44 File Offset: 0x00092D44
		public void Add(decimal? angle, decimal? radius, Color backgroundColor)
		{
			PolarSeriesItem item = new PolarSeriesItem(angle, radius, backgroundColor);
			base.Add(item);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00094B61 File Offset: 0x00092D61
		protected override void SetDirtyObject(object o)
		{
			if (o is PolarSeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

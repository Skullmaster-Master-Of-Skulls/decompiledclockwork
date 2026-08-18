using System;
using System.Drawing;

namespace Telerik.Web.UI.HtmlChart.SeriesItemCollections
{
	// Token: 0x020003C6 RID: 966
	public class BulletSeriesItemCollection : StronglyTypedStateManagedCollection<BulletSeriesItem>
	{
		// Token: 0x0600235F RID: 9055 RVA: 0x00076452 File Offset: 0x00074652
		public override void Add(BulletSeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00076462 File Offset: 0x00074662
		public void Add(decimal? current)
		{
			this.Add(new BulletSeriesItem(current));
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00076470 File Offset: 0x00074670
		public void Add(decimal? current, decimal? target)
		{
			this.Add(new BulletSeriesItem(current, target));
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0007647F File Offset: 0x0007467F
		public void Add(decimal? current, decimal? target, Color bgColor)
		{
			this.Add(new BulletSeriesItem(current, target, bgColor));
		}
	}
}

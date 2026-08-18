using System;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020004D3 RID: 1235
	[ParseChildren(typeof(PlotBand))]
	public class PlotBandsCollection : StronglyTypedStateManagedCollection<PlotBand>
	{
		// Token: 0x06002CF1 RID: 11505 RVA: 0x00093B1B File Offset: 0x00091D1B
		public override void Add(PlotBand item)
		{
			base.Add(item);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x00093B24 File Offset: 0x00091D24
		protected override void SetDirtyObject(object o)
		{
			if (o is PlotBand)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

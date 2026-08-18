using System;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B95 RID: 2965
	[ParseChildren(typeof(SeriesItem))]
	public class SeriesItemCollection : StronglyTypedStateManagedCollection<SeriesItem>
	{
		// Token: 0x06007006 RID: 28678 RVA: 0x001A2BC4 File Offset: 0x001A0DC4
		public new virtual void Add(SeriesItem item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06007007 RID: 28679 RVA: 0x001A2BD4 File Offset: 0x001A0DD4
		public virtual void Add(decimal? yValue)
		{
			SeriesItem item = new SeriesItem(yValue);
			base.Add(item);
		}

		// Token: 0x06007008 RID: 28680 RVA: 0x001A2BEF File Offset: 0x001A0DEF
		protected override void SetDirtyObject(object o)
		{
			if (o is SeriesItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

using System;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B8C RID: 2956
	[ParseChildren(typeof(AxisItem))]
	public class AxisItemCollection : StronglyTypedStateManagedCollection<AxisItem>
	{
		// Token: 0x06006F93 RID: 28563 RVA: 0x001A0E5D File Offset: 0x0019F05D
		public new virtual void Add(AxisItem item)
		{
			base.Add(item);
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x001A0E68 File Offset: 0x0019F068
		public virtual void Add(string labelText)
		{
			AxisItem item = new AxisItem(labelText);
			base.Add(item);
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x001A0E83 File Offset: 0x0019F083
		protected override void SetDirtyObject(object o)
		{
			if (o is AxisItem)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}

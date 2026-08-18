using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B6A RID: 2922
	[ToolboxItem(false)]
	[ParseChildren(typeof(GaugeRange))]
	public class GaugeRangeCollection : StronglyTypedStateManagedCollection<GaugeRange>
	{
		// Token: 0x06006E3B RID: 28219 RVA: 0x00198EE2 File Offset: 0x001970E2
		public override void Add(GaugeRange item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x00198EF4 File Offset: 0x001970F4
		protected override void SetDirtyObject(object o)
		{
			GaugeRange gaugeRange = o as GaugeRange;
			if (gaugeRange != null)
			{
				gaugeRange.SetDirty();
			}
		}
	}
}

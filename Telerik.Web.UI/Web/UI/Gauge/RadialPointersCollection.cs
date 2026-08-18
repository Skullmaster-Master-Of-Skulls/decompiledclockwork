using System;
using System.Web.UI;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000362 RID: 866
	[ParseChildren(typeof(RadialPointer))]
	public class RadialPointersCollection : StronglyTypedStateManagedCollection<RadialPointer>
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x0005D269 File Offset: 0x0005B469
		public override void Add(RadialPointer item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0005D279 File Offset: 0x0005B479
		protected override void SetDirtyObject(object obj)
		{
			if (obj is RadialPointer)
			{
				((StateManager)obj).SetDirty();
			}
		}
	}
}

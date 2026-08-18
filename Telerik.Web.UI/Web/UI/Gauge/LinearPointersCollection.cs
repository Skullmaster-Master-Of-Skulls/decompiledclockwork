using System;
using System.Web.UI;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000361 RID: 865
	[ParseChildren(typeof(LinearPointer))]
	public class LinearPointersCollection : StronglyTypedStateManagedCollection<LinearPointer>
	{
		// Token: 0x06001DD3 RID: 7635 RVA: 0x0005D23C File Offset: 0x0005B43C
		public override void Add(LinearPointer item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0005D24C File Offset: 0x0005B44C
		protected override void SetDirtyObject(object obj)
		{
			if (obj is LinearPointer)
			{
				((StateManager)obj).SetDirty();
			}
		}
	}
}

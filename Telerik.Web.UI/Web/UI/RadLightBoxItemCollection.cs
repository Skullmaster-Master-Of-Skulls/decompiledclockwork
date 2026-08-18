using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000570 RID: 1392
	[PersistChildren(true)]
	[ParseChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadLightBoxItemCollection : StronglyTypedStateManagedCollection<RadLightBoxItem>
	{
		// Token: 0x0600320C RID: 12812 RVA: 0x000A4118 File Offset: 0x000A2318
		protected override void SetDirtyObject(object stateManagerObject)
		{
			StateManager stateManager = stateManagerObject as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000A4135 File Offset: 0x000A2335
		public override void Add(RadLightBoxItem item)
		{
			base.Add(item);
		}
	}
}

using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001809 RID: 6153
	public class ColorPickerItemCollection : StronglyTypedStateManagedCollection<ColorPickerItem>
	{
		// Token: 0x0600EFCC RID: 61388 RVA: 0x00369D95 File Offset: 0x00367F95
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0600EFCD RID: 61389 RVA: 0x00369DA2 File Offset: 0x00367FA2
		public virtual void AddRange(ColorPickerItemCollection items)
		{
			this.AddRange((ColorPickerItem[])new ArrayList(items).ToArray(typeof(ColorPickerItem)));
		}
	}
}

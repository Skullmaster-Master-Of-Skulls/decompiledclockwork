using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000E55 RID: 3669
	public class RibbonBarColorPickerItemCollection : List<RibbonBarColorPickerItem>
	{
		// Token: 0x06008B1B RID: 35611 RVA: 0x001FAB8D File Offset: 0x001F8D8D
		public virtual void AddRange(RibbonBarColorPickerItemCollection items)
		{
			base.AddRange((RibbonBarColorPickerItem[])new ArrayList(items).ToArray(typeof(RibbonBarColorPickerItem)));
		}
	}
}

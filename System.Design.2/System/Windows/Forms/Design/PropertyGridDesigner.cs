using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000322 RID: 802
	internal class PropertyGridDesigner : ControlDesigner
	{
		// Token: 0x06001FCC RID: 8140 RVA: 0x00093E53 File Offset: 0x00092053
		public PropertyGridDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000C0DAF File Offset: 0x000BEFAF
		protected override void PreFilterProperties(IDictionary properties)
		{
			properties.Remove("AutoScroll");
			properties.Remove("AutoScrollMargin");
			properties.Remove("DockPadding");
			properties.Remove("AutoScrollMinSize");
			base.PreFilterProperties(properties);
		}
	}
}

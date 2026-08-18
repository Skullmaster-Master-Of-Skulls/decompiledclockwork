using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000337 RID: 823
	internal class StatusBarDesigner : ControlDesigner
	{
		// Token: 0x06002076 RID: 8310 RVA: 0x00093E53 File Offset: 0x00092053
		public StatusBarDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000C5788 File Offset: 0x000C3988
		public override ICollection AssociatedComponents
		{
			get
			{
				StatusBar statusBar = this.Control as StatusBar;
				if (statusBar != null)
				{
					return statusBar.Panels;
				}
				return base.AssociatedComponents;
			}
		}
	}
}

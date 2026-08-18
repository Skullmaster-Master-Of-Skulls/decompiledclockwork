using System;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x02000334 RID: 820
	[ComVisible(true)]
	public class PropertyTabChangedEventArgs : EventArgs
	{
		// Token: 0x06003561 RID: 13665 RVA: 0x000F287B File Offset: 0x000F0A7B
		public PropertyTabChangedEventArgs(PropertyTab oldTab, PropertyTab newTab)
		{
			this.oldTab = oldTab;
			this.newTab = newTab;
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000F2891 File Offset: 0x000F0A91
		public PropertyTab OldTab
		{
			get
			{
				return this.oldTab;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000F2899 File Offset: 0x000F0A99
		public PropertyTab NewTab
		{
			get
			{
				return this.newTab;
			}
		}

		// Token: 0x04001F53 RID: 8019
		private PropertyTab oldTab;

		// Token: 0x04001F54 RID: 8020
		private PropertyTab newTab;
	}
}

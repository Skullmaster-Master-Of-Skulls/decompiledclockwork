using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002DE RID: 734
	[ComVisible(true)]
	public class ListViewItemMouseHoverEventArgs : EventArgs
	{
		// Token: 0x06002E9C RID: 11932 RVA: 0x000D338D File Offset: 0x000D158D
		public ListViewItemMouseHoverEventArgs(ListViewItem item)
		{
			this.item = item;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000D339C File Offset: 0x000D159C
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001340 RID: 4928
		private readonly ListViewItem item;
	}
}

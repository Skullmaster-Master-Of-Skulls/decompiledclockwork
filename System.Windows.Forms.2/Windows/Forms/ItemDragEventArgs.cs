using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002AE RID: 686
	[ComVisible(true)]
	public class ItemDragEventArgs : EventArgs
	{
		// Token: 0x06002A54 RID: 10836 RVA: 0x000BFBCE File Offset: 0x000BDDCE
		public ItemDragEventArgs(MouseButtons button)
		{
			this.button = button;
			this.item = null;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000BFBE4 File Offset: 0x000BDDE4
		public ItemDragEventArgs(MouseButtons button, object item)
		{
			this.button = button;
			this.item = item;
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x000BFBFA File Offset: 0x000BDDFA
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000BFC02 File Offset: 0x000BDE02
		public object Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001133 RID: 4403
		private readonly MouseButtons button;

		// Token: 0x04001134 RID: 4404
		private readonly object item;
	}
}

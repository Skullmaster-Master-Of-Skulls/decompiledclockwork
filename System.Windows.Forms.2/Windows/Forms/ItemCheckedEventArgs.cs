using System;

namespace System.Windows.Forms
{
	// Token: 0x020002AA RID: 682
	public class ItemCheckedEventArgs : EventArgs
	{
		// Token: 0x06002A45 RID: 10821 RVA: 0x000BFB79 File Offset: 0x000BDD79
		public ItemCheckedEventArgs(ListViewItem item)
		{
			this.lvi = item;
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000BFB88 File Offset: 0x000BDD88
		public ListViewItem Item
		{
			get
			{
				return this.lvi;
			}
		}

		// Token: 0x0400112F RID: 4399
		private ListViewItem lvi;
	}
}

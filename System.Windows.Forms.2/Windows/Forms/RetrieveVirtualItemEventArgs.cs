using System;

namespace System.Windows.Forms
{
	// Token: 0x02000345 RID: 837
	public class RetrieveVirtualItemEventArgs : EventArgs
	{
		// Token: 0x060035E8 RID: 13800 RVA: 0x000F3922 File Offset: 0x000F1B22
		public RetrieveVirtualItemEventArgs(int itemIndex)
		{
			this.itemIndex = itemIndex;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000F3931 File Offset: 0x000F1B31
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000F3939 File Offset: 0x000F1B39
		// (set) Token: 0x060035EB RID: 13803 RVA: 0x000F3941 File Offset: 0x000F1B41
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x04001F76 RID: 8054
		private int itemIndex;

		// Token: 0x04001F77 RID: 8055
		private ListViewItem item;
	}
}

using System;

namespace System.Windows.Forms
{
	// Token: 0x020002E0 RID: 736
	public class ListViewItemSelectionChangedEventArgs : EventArgs
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x000D33A4 File Offset: 0x000D15A4
		public ListViewItemSelectionChangedEventArgs(ListViewItem item, int itemIndex, bool isSelected)
		{
			this.item = item;
			this.itemIndex = itemIndex;
			this.isSelected = isSelected;
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000D33C1 File Offset: 0x000D15C1
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000D33C9 File Offset: 0x000D15C9
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000D33D1 File Offset: 0x000D15D1
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x04001341 RID: 4929
		private ListViewItem item;

		// Token: 0x04001342 RID: 4930
		private int itemIndex;

		// Token: 0x04001343 RID: 4931
		private bool isSelected;
	}
}

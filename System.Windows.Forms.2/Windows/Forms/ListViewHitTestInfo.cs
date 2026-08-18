using System;

namespace System.Windows.Forms
{
	// Token: 0x020002DA RID: 730
	public class ListViewHitTestInfo
	{
		// Token: 0x06002E2E RID: 11822 RVA: 0x000D19B6 File Offset: 0x000CFBB6
		public ListViewHitTestInfo(ListViewItem hitItem, ListViewItem.ListViewSubItem hitSubItem, ListViewHitTestLocations hitLocation)
		{
			this.item = hitItem;
			this.subItem = hitSubItem;
			this.loc = hitLocation;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000D19D3 File Offset: 0x000CFBD3
		public ListViewHitTestLocations Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000D19DB File Offset: 0x000CFBDB
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000D19E3 File Offset: 0x000CFBE3
		public ListViewItem.ListViewSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
		}

		// Token: 0x0400131D RID: 4893
		private ListViewHitTestLocations loc;

		// Token: 0x0400131E RID: 4894
		private ListViewItem item;

		// Token: 0x0400131F RID: 4895
		private ListViewItem.ListViewSubItem subItem;
	}
}

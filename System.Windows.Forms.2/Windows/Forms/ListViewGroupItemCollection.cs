using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002D9 RID: 729
	internal class ListViewGroupItemCollection : ListView.ListViewItemCollection.IInnerList
	{
		// Token: 0x06002E1A RID: 11802 RVA: 0x000D16C0 File Offset: 0x000CF8C0
		public ListViewGroupItemCollection(ListViewGroup group)
		{
			this.group = group;
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000D16CF File Offset: 0x000CF8CF
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000D16DC File Offset: 0x000CF8DC
		private ArrayList Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ArrayList();
				}
				return this.items;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000D16F7 File Offset: 0x000CF8F7
		public bool OwnerIsVirtualListView
		{
			get
			{
				return this.group.ListView != null && this.group.ListView.VirtualMode;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000D1718 File Offset: 0x000CF918
		public bool OwnerIsDesignMode
		{
			get
			{
				if (this.group.ListView != null)
				{
					ISite site = this.group.ListView.Site;
					return site != null && site.DesignMode;
				}
				return false;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		public ListViewItem this[int index]
		{
			get
			{
				return (ListViewItem)this.Items[index];
			}
			set
			{
				if (value != this.Items[index])
				{
					this.MoveToGroup((ListViewItem)this.Items[index], null);
					this.Items[index] = value;
					this.MoveToGroup((ListViewItem)this.Items[index], this.group);
				}
			}
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000D17C2 File Offset: 0x000CF9C2
		public ListViewItem Add(ListViewItem value)
		{
			this.CheckListViewItem(value);
			this.MoveToGroup(value, this.group);
			this.Items.Add(value);
			return value;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000D17E8 File Offset: 0x000CF9E8
		public void AddRange(ListViewItem[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				this.CheckListViewItem(items[i]);
			}
			this.Items.AddRange(items);
			for (int j = 0; j < items.Length; j++)
			{
				this.MoveToGroup(items[j], this.group);
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000D1838 File Offset: 0x000CFA38
		private void CheckListViewItem(ListViewItem item)
		{
			if (item.ListView != null && item.ListView != this.group.ListView)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					item.Text
				}), "item");
			}
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000D1884 File Offset: 0x000CFA84
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.MoveToGroup(this[i], null);
			}
			this.Items.Clear();
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000D18BB File Offset: 0x000CFABB
		public bool Contains(ListViewItem item)
		{
			return this.Items.Contains(item);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000D18C9 File Offset: 0x000CFAC9
		public void CopyTo(Array dest, int index)
		{
			this.Items.CopyTo(dest, index);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000D18D8 File Offset: 0x000CFAD8
		public IEnumerator GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000D18E5 File Offset: 0x000CFAE5
		public int IndexOf(ListViewItem item)
		{
			return this.Items.IndexOf(item);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000D18F3 File Offset: 0x000CFAF3
		public ListViewItem Insert(int index, ListViewItem item)
		{
			this.CheckListViewItem(item);
			this.MoveToGroup(item, this.group);
			this.Items.Insert(index, item);
			return item;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000D1918 File Offset: 0x000CFB18
		private void MoveToGroup(ListViewItem item, ListViewGroup newGroup)
		{
			ListViewGroup listViewGroup = item.Group;
			if (listViewGroup != newGroup)
			{
				item.group = newGroup;
				if (listViewGroup != null)
				{
					listViewGroup.Items.Remove(item);
				}
				this.UpdateNativeListViewItem(item);
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000D194D File Offset: 0x000CFB4D
		public void Remove(ListViewItem item)
		{
			this.Items.Remove(item);
			if (item.group == this.group)
			{
				item.group = null;
				this.UpdateNativeListViewItem(item);
			}
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000D1977 File Offset: 0x000CFB77
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000D1986 File Offset: 0x000CFB86
		private void UpdateNativeListViewItem(ListViewItem item)
		{
			if (item.ListView != null && item.ListView.IsHandleCreated && !item.ListView.InsertingItemsNatively)
			{
				item.UpdateStateToListView(item.Index);
			}
		}

		// Token: 0x0400131B RID: 4891
		private ListViewGroup group;

		// Token: 0x0400131C RID: 4892
		private ArrayList items;
	}
}

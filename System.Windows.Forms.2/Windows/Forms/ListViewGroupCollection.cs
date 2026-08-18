using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x020002D7 RID: 727
	[ListBindable(false)]
	public class ListViewGroupCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002DF2 RID: 11762 RVA: 0x000D0F6F File Offset: 0x000CF16F
		internal ListViewGroupCollection(ListView listView)
		{
			this.listView = listView;
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000D0F7E File Offset: 0x000CF17E
		public int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x00013062 File Offset: 0x00011262
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000D0F8B File Offset: 0x000CF18B
		private ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x17000ABE RID: 2750
		public ListViewGroup this[int index]
		{
			get
			{
				return (ListViewGroup)this.List[index];
			}
			set
			{
				if (this.List.Contains(value))
				{
					return;
				}
				this.List[index] = value;
			}
		}

		// Token: 0x17000ABF RID: 2751
		public ListViewGroup this[string key]
		{
			get
			{
				if (this.list == null)
				{
					return null;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					if (string.Compare(key, this[i].Name, false, CultureInfo.CurrentCulture) == 0)
					{
						return this[i];
					}
				}
				return null;
			}
			set
			{
				int num = -1;
				if (this.list == null)
				{
					return;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					if (string.Compare(key, this[i].Name, false, CultureInfo.CurrentCulture) == 0)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					this.list[num] = value;
				}
			}
		}

		// Token: 0x17000AC0 RID: 2752
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is ListViewGroup)
				{
					this[index] = (ListViewGroup)value;
				}
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000D10A8 File Offset: 0x000CF2A8
		public int Add(ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return -1;
			}
			this.CheckListViewItems(group);
			group.ListViewInternal = this.listView;
			int result = this.List.Add(group);
			if (this.listView.IsHandleCreated)
			{
				this.listView.InsertGroupInListView(this.List.Count, group);
				this.MoveGroupItems(group);
			}
			return result;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000D110C File Offset: 0x000CF30C
		public ListViewGroup Add(string key, string headerText)
		{
			ListViewGroup listViewGroup = new ListViewGroup(key, headerText);
			this.Add(listViewGroup);
			return listViewGroup;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000D112A File Offset: 0x000CF32A
		int IList.Add(object value)
		{
			if (value is ListViewGroup)
			{
				return this.Add((ListViewGroup)value);
			}
			throw new ArgumentException("value");
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000D114C File Offset: 0x000CF34C
		public void AddRange(ListViewGroup[] groups)
		{
			for (int i = 0; i < groups.Length; i++)
			{
				this.Add(groups[i]);
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000D1174 File Offset: 0x000CF374
		public void AddRange(ListViewGroupCollection groups)
		{
			for (int i = 0; i < groups.Count; i++)
			{
				this.Add(groups[i]);
			}
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000D11A0 File Offset: 0x000CF3A0
		private void CheckListViewItems(ListViewGroup group)
		{
			for (int i = 0; i < group.Items.Count; i++)
			{
				ListViewItem listViewItem = group.Items[i];
				if (listViewItem.ListView != null && listViewItem.ListView != this.listView)
				{
					throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
					{
						listViewItem.Text
					}));
				}
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000D1208 File Offset: 0x000CF408
		public void Clear()
		{
			if (this.listView.IsHandleCreated)
			{
				for (int i = 0; i < this.Count; i++)
				{
					this.listView.RemoveGroupFromListView(this[i]);
				}
			}
			for (int j = 0; j < this.Count; j++)
			{
				this[j].ListViewInternal = null;
			}
			this.List.Clear();
			this.listView.UpdateGroupView();
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000D1279 File Offset: 0x000CF479
		public bool Contains(ListViewGroup value)
		{
			return this.List.Contains(value);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000D1287 File Offset: 0x000CF487
		bool IList.Contains(object value)
		{
			return value is ListViewGroup && this.Contains((ListViewGroup)value);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000D129F File Offset: 0x000CF49F
		public void CopyTo(Array array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000D12AE File Offset: 0x000CF4AE
		public IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000D12BB File Offset: 0x000CF4BB
		public int IndexOf(ListViewGroup value)
		{
			return this.List.IndexOf(value);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000D12C9 File Offset: 0x000CF4C9
		int IList.IndexOf(object value)
		{
			if (value is ListViewGroup)
			{
				return this.IndexOf((ListViewGroup)value);
			}
			return -1;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000D12E4 File Offset: 0x000CF4E4
		public void Insert(int index, ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return;
			}
			group.ListViewInternal = this.listView;
			this.List.Insert(index, group);
			if (this.listView.IsHandleCreated)
			{
				this.listView.InsertGroupInListView(index, group);
				this.MoveGroupItems(group);
			}
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000D1335 File Offset: 0x000CF535
		void IList.Insert(int index, object value)
		{
			if (value is ListViewGroup)
			{
				this.Insert(index, (ListViewGroup)value);
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000D134C File Offset: 0x000CF54C
		private void MoveGroupItems(ListViewGroup group)
		{
			foreach (object obj in group.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ListView == this.listView)
				{
					listViewItem.UpdateStateToListView(listViewItem.Index);
				}
			}
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000D13B8 File Offset: 0x000CF5B8
		public void Remove(ListViewGroup group)
		{
			group.ListViewInternal = null;
			this.List.Remove(group);
			if (this.listView.IsHandleCreated)
			{
				this.listView.RemoveGroupFromListView(group);
			}
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000D13E6 File Offset: 0x000CF5E6
		void IList.Remove(object value)
		{
			if (value is ListViewGroup)
			{
				this.Remove((ListViewGroup)value);
			}
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000D13FC File Offset: 0x000CF5FC
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x04001319 RID: 4889
		private ListView listView;

		// Token: 0x0400131A RID: 4890
		private ArrayList list;
	}
}

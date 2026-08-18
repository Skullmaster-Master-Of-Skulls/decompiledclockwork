using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000457 RID: 1111
	[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class ListItemCollection : ICollection, IEnumerable, IList, IStateManager
	{
		// Token: 0x060035D0 RID: 13776 RVA: 0x000AE4D4 File Offset: 0x000AC6D4
		public ListItemCollection()
		{
			this.listItems = new ArrayList();
			this.marked = false;
			this.saveAll = false;
		}

		// Token: 0x17000FA3 RID: 4003
		public ListItem this[int index]
		{
			get
			{
				return (ListItem)this.listItems[index];
			}
		}

		// Token: 0x17000FA4 RID: 4004
		object IList.this[int index]
		{
			get
			{
				return this.listItems[index];
			}
			set
			{
				this.listItems[index] = (ListItem)value;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000AE52A File Offset: 0x000AC72A
		// (set) Token: 0x060035D5 RID: 13781 RVA: 0x000AE537 File Offset: 0x000AC737
		public int Capacity
		{
			get
			{
				return this.listItems.Capacity;
			}
			set
			{
				this.listItems.Capacity = value;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000AE545 File Offset: 0x000AC745
		public int Count
		{
			get
			{
				return this.listItems.Count;
			}
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000AE552 File Offset: 0x000AC752
		public void Add(string item)
		{
			this.Add(new ListItem(item));
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000AE560 File Offset: 0x000AC760
		public void Add(ListItem item)
		{
			this.listItems.Add(item);
			if (this.marked)
			{
				item.Dirty = true;
			}
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000AE580 File Offset: 0x000AC780
		int IList.Add(object item)
		{
			ListItem listItem = (ListItem)item;
			int result = this.listItems.Add(listItem);
			if (this.marked)
			{
				listItem.Dirty = true;
			}
			return result;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000AE5B4 File Offset: 0x000AC7B4
		public void AddRange(ListItem[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (ListItem item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000AE5EA File Offset: 0x000AC7EA
		public void Clear()
		{
			this.listItems.Clear();
			if (this.marked)
			{
				this.saveAll = true;
			}
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000AE606 File Offset: 0x000AC806
		public bool Contains(ListItem item)
		{
			return this.listItems.Contains(item);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000AE614 File Offset: 0x000AC814
		bool IList.Contains(object item)
		{
			return this.Contains((ListItem)item);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000AE622 File Offset: 0x000AC822
		public void CopyTo(Array array, int index)
		{
			this.listItems.CopyTo(array, index);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000AE634 File Offset: 0x000AC834
		public ListItem FindByText(string text)
		{
			int num = this.FindByTextInternal(text, true);
			if (num != -1)
			{
				return (ListItem)this.listItems[num];
			}
			return null;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000AE664 File Offset: 0x000AC864
		internal int FindByTextInternal(string text, bool includeDisabled)
		{
			int num = 0;
			foreach (object obj in this.listItems)
			{
				ListItem listItem = (ListItem)obj;
				if (listItem.Text.Equals(text) && (includeDisabled || listItem.Enabled))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000AE6E0 File Offset: 0x000AC8E0
		public ListItem FindByValue(string value)
		{
			int num = this.FindByValueInternal(value, true);
			if (num != -1)
			{
				return (ListItem)this.listItems[num];
			}
			return null;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000AE710 File Offset: 0x000AC910
		internal int FindByValueInternal(string value, bool includeDisabled)
		{
			int num = 0;
			foreach (object obj in this.listItems)
			{
				ListItem listItem = (ListItem)obj;
				if (listItem.Value.Equals(value) && (includeDisabled || listItem.Enabled))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000AE78C File Offset: 0x000AC98C
		public IEnumerator GetEnumerator()
		{
			return this.listItems.GetEnumerator();
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000AE799 File Offset: 0x000AC999
		public int IndexOf(ListItem item)
		{
			return this.listItems.IndexOf(item);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000AE7A7 File Offset: 0x000AC9A7
		int IList.IndexOf(object item)
		{
			return this.IndexOf((ListItem)item);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000AE7B5 File Offset: 0x000AC9B5
		public void Insert(int index, string item)
		{
			this.Insert(index, new ListItem(item));
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000AE7C4 File Offset: 0x000AC9C4
		public void Insert(int index, ListItem item)
		{
			this.listItems.Insert(index, item);
			if (this.marked)
			{
				this.saveAll = true;
			}
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000AE7E2 File Offset: 0x000AC9E2
		void IList.Insert(int index, object item)
		{
			this.Insert(index, (ListItem)item);
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x00007722 File Offset: 0x00005922
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000AE7F1 File Offset: 0x000AC9F1
		public bool IsReadOnly
		{
			get
			{
				return this.listItems.IsReadOnly;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000AE7FE File Offset: 0x000AC9FE
		public bool IsSynchronized
		{
			get
			{
				return this.listItems.IsSynchronized;
			}
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000AE80B File Offset: 0x000ACA0B
		public void RemoveAt(int index)
		{
			this.listItems.RemoveAt(index);
			if (this.marked)
			{
				this.saveAll = true;
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000AE828 File Offset: 0x000ACA28
		public void Remove(string item)
		{
			int num = this.IndexOf(new ListItem(item));
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000AE850 File Offset: 0x000ACA50
		public void Remove(ListItem item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000AE870 File Offset: 0x000ACA70
		void IList.Remove(object item)
		{
			this.Remove((ListItem)item);
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000AE87E File Offset: 0x000ACA7E
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000AE886 File Offset: 0x000ACA86
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000AE890 File Offset: 0x000ACA90
		internal void LoadViewState(object state)
		{
			if (state != null)
			{
				if (state is Pair)
				{
					Pair pair = (Pair)state;
					ArrayList arrayList = (ArrayList)pair.First;
					ArrayList arrayList2 = (ArrayList)pair.Second;
					for (int i = 0; i < arrayList.Count; i++)
					{
						int num = (int)arrayList[i];
						if (num < this.Count)
						{
							this[num].LoadViewState(arrayList2[i]);
						}
						else
						{
							ListItem listItem = new ListItem();
							listItem.LoadViewState(arrayList2[i]);
							this.Add(listItem);
						}
					}
					return;
				}
				Triplet triplet = (Triplet)state;
				this.listItems = new ArrayList();
				this.saveAll = true;
				string[] array = (string[])triplet.First;
				string[] array2 = (string[])triplet.Second;
				bool[] array3 = (bool[])triplet.Third;
				for (int j = 0; j < array.Length; j++)
				{
					this.Add(new ListItem(array[j], array2[j], array3[j]));
				}
			}
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000AE998 File Offset: 0x000ACB98
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000AE9A0 File Offset: 0x000ACBA0
		internal void TrackViewState()
		{
			this.marked = true;
			for (int i = 0; i < this.Count; i++)
			{
				this[i].TrackViewState();
			}
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000AE9D1 File Offset: 0x000ACBD1
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000AE9DC File Offset: 0x000ACBDC
		internal object SaveViewState()
		{
			if (this.saveAll)
			{
				int count = this.Count;
				object[] array = new string[count];
				object[] array2 = array;
				array = new string[count];
				object[] array3 = array;
				bool[] array4 = new bool[count];
				for (int i = 0; i < count; i++)
				{
					array2[i] = this[i].Text;
					array3[i] = this[i].Value;
					array4[i] = this[i].Enabled;
				}
				return new Triplet(array2, array3, array4);
			}
			ArrayList arrayList = new ArrayList(4);
			ArrayList arrayList2 = new ArrayList(4);
			for (int j = 0; j < this.Count; j++)
			{
				object obj = this[j].SaveViewState();
				if (obj != null)
				{
					arrayList.Add(j);
					arrayList2.Add(obj);
				}
			}
			if (arrayList.Count > 0)
			{
				return new Pair(arrayList, arrayList2);
			}
			return null;
		}

		// Token: 0x040021D2 RID: 8658
		private ArrayList listItems;

		// Token: 0x040021D3 RID: 8659
		private bool marked;

		// Token: 0x040021D4 RID: 8660
		private bool saveAll;
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005CB RID: 1483
	[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ListItemCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600485A RID: 18522 RVA: 0x0012727A File Offset: 0x0012627A
		public ListItemCollection()
		{
			this.listItems = new ArrayList();
			this.marked = false;
			this.saveAll = false;
		}

		// Token: 0x170011D5 RID: 4565
		public ListItem this[int index]
		{
			get
			{
				return (ListItem)this.listItems[index];
			}
		}

		// Token: 0x170011D6 RID: 4566
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

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x001272D0 File Offset: 0x001262D0
		// (set) Token: 0x0600485F RID: 18527 RVA: 0x001272DD File Offset: 0x001262DD
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

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x001272EB File Offset: 0x001262EB
		public int Count
		{
			get
			{
				return this.listItems.Count;
			}
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x001272F8 File Offset: 0x001262F8
		public void Add(string item)
		{
			this.Add(new ListItem(item));
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x00127306 File Offset: 0x00126306
		public void Add(ListItem item)
		{
			this.listItems.Add(item);
			if (this.marked)
			{
				item.Dirty = true;
			}
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x00127324 File Offset: 0x00126324
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

		// Token: 0x06004864 RID: 18532 RVA: 0x00127358 File Offset: 0x00126358
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

		// Token: 0x06004865 RID: 18533 RVA: 0x0012738E File Offset: 0x0012638E
		public void Clear()
		{
			this.listItems.Clear();
			if (this.marked)
			{
				this.saveAll = true;
			}
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x001273AA File Offset: 0x001263AA
		public bool Contains(ListItem item)
		{
			return this.listItems.Contains(item);
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x001273B8 File Offset: 0x001263B8
		bool IList.Contains(object item)
		{
			return this.Contains((ListItem)item);
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x001273C6 File Offset: 0x001263C6
		public void CopyTo(Array array, int index)
		{
			this.listItems.CopyTo(array, index);
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x001273D8 File Offset: 0x001263D8
		public ListItem FindByText(string text)
		{
			int num = this.FindByTextInternal(text, true);
			if (num != -1)
			{
				return (ListItem)this.listItems[num];
			}
			return null;
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x00127408 File Offset: 0x00126408
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

		// Token: 0x0600486B RID: 18539 RVA: 0x00127484 File Offset: 0x00126484
		public ListItem FindByValue(string value)
		{
			int num = this.FindByValueInternal(value, true);
			if (num != -1)
			{
				return (ListItem)this.listItems[num];
			}
			return null;
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x001274B4 File Offset: 0x001264B4
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

		// Token: 0x0600486D RID: 18541 RVA: 0x00127530 File Offset: 0x00126530
		public IEnumerator GetEnumerator()
		{
			return this.listItems.GetEnumerator();
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x0012753D File Offset: 0x0012653D
		public int IndexOf(ListItem item)
		{
			return this.listItems.IndexOf(item);
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x0012754B File Offset: 0x0012654B
		int IList.IndexOf(object item)
		{
			return this.IndexOf((ListItem)item);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x00127559 File Offset: 0x00126559
		public void Insert(int index, string item)
		{
			this.Insert(index, new ListItem(item));
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00127568 File Offset: 0x00126568
		public void Insert(int index, ListItem item)
		{
			this.listItems.Insert(index, item);
			if (this.marked)
			{
				this.saveAll = true;
			}
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00127586 File Offset: 0x00126586
		void IList.Insert(int index, object item)
		{
			this.Insert(index, (ListItem)item);
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x00127595 File Offset: 0x00126595
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00127598 File Offset: 0x00126598
		public bool IsReadOnly
		{
			get
			{
				return this.listItems.IsReadOnly;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x001275A5 File Offset: 0x001265A5
		public bool IsSynchronized
		{
			get
			{
				return this.listItems.IsSynchronized;
			}
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x001275B2 File Offset: 0x001265B2
		public void RemoveAt(int index)
		{
			this.listItems.RemoveAt(index);
			if (this.marked)
			{
				this.saveAll = true;
			}
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x001275D0 File Offset: 0x001265D0
		public void Remove(string item)
		{
			int num = this.IndexOf(new ListItem(item));
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x001275F8 File Offset: 0x001265F8
		public void Remove(ListItem item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x00127618 File Offset: 0x00126618
		void IList.Remove(object item)
		{
			this.Remove((ListItem)item);
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x0600487A RID: 18554 RVA: 0x00127626 File Offset: 0x00126626
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x00127629 File Offset: 0x00126629
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x00127631 File Offset: 0x00126631
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x0012763C File Offset: 0x0012663C
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

		// Token: 0x0600487E RID: 18558 RVA: 0x00127744 File Offset: 0x00126744
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x0012774C File Offset: 0x0012674C
		internal void TrackViewState()
		{
			this.marked = true;
			for (int i = 0; i < this.Count; i++)
			{
				this[i].TrackViewState();
			}
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x0012777D File Offset: 0x0012677D
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x00127788 File Offset: 0x00126788
		internal object SaveViewState()
		{
			if (this.saveAll)
			{
				int count = this.Count;
				object[] array = new string[count];
				object[] array2 = new string[count];
				bool[] array3 = new bool[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this[i].Text;
					array2[i] = this[i].Value;
					array3[i] = this[i].Enabled;
				}
				return new Triplet(array, array2, array3);
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

		// Token: 0x04002ACE RID: 10958
		private ArrayList listItems;

		// Token: 0x04002ACF RID: 10959
		private bool marked;

		// Token: 0x04002AD0 RID: 10960
		private bool saveAll;
	}
}

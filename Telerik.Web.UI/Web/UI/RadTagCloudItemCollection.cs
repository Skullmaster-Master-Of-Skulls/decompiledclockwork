using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FA5 RID: 4005
	public class RadTagCloudItemCollection : StateManagedCollection
	{
		// Token: 0x060099CE RID: 39374 RVA: 0x00225020 File Offset: 0x00223220
		public RadTagCloudItemCollection(RadTagCloud parent)
		{
			this._itemContainer = parent;
			foreach (object obj in this)
			{
				RadTagCloudItem radTagCloudItem = (RadTagCloudItem)obj;
				radTagCloudItem.Container = this.ItemContainer;
			}
		}

		// Token: 0x170030AE RID: 12462
		// (get) Token: 0x060099CF RID: 39375 RVA: 0x00225088 File Offset: 0x00223288
		private RadTagCloud ItemContainer
		{
			get
			{
				return this._itemContainer;
			}
		}

		// Token: 0x170030AF RID: 12463
		public RadTagCloudItem this[int index]
		{
			get
			{
				return (RadTagCloudItem)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x170030B0 RID: 12464
		// (get) Token: 0x060099D2 RID: 39378 RVA: 0x002250B2 File Offset: 0x002232B2
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170030B1 RID: 12465
		// (get) Token: 0x060099D3 RID: 39379 RVA: 0x002250B5 File Offset: 0x002232B5
		// (set) Token: 0x060099D4 RID: 39380 RVA: 0x002250C4 File Offset: 0x002232C4
		internal RadTagCloudItemCollection FilteredAndSorted
		{
			get
			{
				this.Sort();
				return this._filteredAndSorted;
			}
			set
			{
				this._filteredAndSorted = value;
			}
		}

		// Token: 0x060099D5 RID: 39381 RVA: 0x002250CD File Offset: 0x002232CD
		public void Add(RadTagCloudItem item)
		{
			this.List.Add(item);
			item.Container = this.ItemContainer;
		}

		// Token: 0x060099D6 RID: 39382 RVA: 0x002250E8 File Offset: 0x002232E8
		public bool Contains(RadTagCloudItem item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x060099D7 RID: 39383 RVA: 0x002250F6 File Offset: 0x002232F6
		public void CopyTo(RadTagCloudItem[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x060099D8 RID: 39384 RVA: 0x00225105 File Offset: 0x00223305
		public int IndexOf(RadTagCloudItem item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x060099D9 RID: 39385 RVA: 0x00225113 File Offset: 0x00223313
		public void Insert(int index, RadTagCloudItem item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x060099DA RID: 39386 RVA: 0x00225124 File Offset: 0x00223324
		protected override void OnInsertComplete(int index, object value)
		{
			RadTagCloudItem radTagCloudItem = (RadTagCloudItem)value;
			radTagCloudItem.Container = this.ItemContainer;
		}

		// Token: 0x060099DB RID: 39387 RVA: 0x00225144 File Offset: 0x00223344
		public void Remove(RadTagCloudItem item)
		{
			this.List.Remove(item);
		}

		// Token: 0x060099DC RID: 39388 RVA: 0x00225152 File Offset: 0x00223352
		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x060099DD RID: 39389 RVA: 0x00225160 File Offset: 0x00223360
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x060099DE RID: 39390 RVA: 0x00225170 File Offset: 0x00223370
		public RadTagCloudItemCollection Sort()
		{
			TagCloudSorting sorting = this.ItemContainer.Sorting;
			this._filteredAndSorted = this.Filter();
			if (this.ItemContainer.ListOfSortedItems.Count > 0)
			{
				bool flag = false;
				switch (sorting)
				{
				case TagCloudSorting.NotSorted:
					flag = true;
					break;
				case TagCloudSorting.WeightedAsc:
					this.SortByWeight(this._filteredAndSorted, true);
					flag = true;
					break;
				case TagCloudSorting.WeightedDsc:
					this.SortByWeight(this._filteredAndSorted, false);
					flag = true;
					break;
				}
				if (flag)
				{
					return this._filteredAndSorted;
				}
			}
			IComparer comparer;
			switch (sorting)
			{
			case TagCloudSorting.AlphabeticAsc:
				comparer = new RadTagCloudItemCollection.AlphabeticComparerAsc();
				break;
			case TagCloudSorting.AlphabeticDsc:
				comparer = new RadTagCloudItemCollection.AlphabeticComparerDsc();
				break;
			case TagCloudSorting.WeightedAsc:
				comparer = new RadTagCloudItemCollection.WeightComparerAsc();
				break;
			case TagCloudSorting.WeightedDsc:
				comparer = new RadTagCloudItemCollection.WeightComparerDsc();
				break;
			default:
				return this._filteredAndSorted;
			}
			ArrayList.Adapter(this._filteredAndSorted.List).Sort(comparer);
			return this._filteredAndSorted;
		}

		// Token: 0x060099DF RID: 39391 RVA: 0x00225258 File Offset: 0x00223458
		public RadTagCloudItemCollection Filter()
		{
			if (base.Count <= 0)
			{
				return this;
			}
			int count = base.Count;
			RadTagCloudItemCollection radTagCloudItemCollection = new RadTagCloudItemCollection(this.ItemContainer);
			this.ItemContainer.ListOfSortedItems.Clear();
			this._min = this[0];
			this._max = this[0];
			for (int i = 0; i < count; i++)
			{
				this._min = ((this._min.Weight >= this[i].Weight) ? this[i] : this._min);
				this._max = ((this._max.Weight <= this[i].Weight) ? this[i] : this._max);
				if (this.ItemContainer.MinimalWeightAllowed == 0.0 || this[i].Weight >= this.ItemContainer.MinimalWeightAllowed)
				{
					radTagCloudItemCollection.Add(this[i]);
					this.ItemContainer.ListOfSortedItems[this[i]] = this[i].Weight;
				}
			}
			if (radTagCloudItemCollection.Count <= this.ItemContainer.MaxNumberOfItems || this.ItemContainer.MaxNumberOfItems == 0)
			{
				return radTagCloudItemCollection;
			}
			this.FilterItems(this.ItemContainer.TakeTopWeightedItems, radTagCloudItemCollection);
			return radTagCloudItemCollection;
		}

		// Token: 0x060099E0 RID: 39392 RVA: 0x002253B3 File Offset: 0x002235B3
		public RadTagCloudItem Min()
		{
			if (this._min == null)
			{
				this.FindMinMax();
			}
			return this._min;
		}

		// Token: 0x060099E1 RID: 39393 RVA: 0x002253C9 File Offset: 0x002235C9
		public RadTagCloudItem Max()
		{
			if (this._max == null)
			{
				this.FindMinMax();
			}
			return this._max;
		}

		// Token: 0x060099E2 RID: 39394 RVA: 0x002253E0 File Offset: 0x002235E0
		private void FindMinMax()
		{
			int count = base.Count;
			if (this.ItemContainer.ListOfSortedItems.Count > 0 && count == this.ItemContainer.ListOfSortedItems.Count)
			{
				this._max = this.ItemContainer.ListOfSortedItems.Keys[0];
				this._min = this.ItemContainer.ListOfSortedItems.Keys[this.ItemContainer.ListOfSortedItems.Count - 1];
				return;
			}
			this._min = this[0];
			this._max = this[0];
			for (int i = 0; i < count; i++)
			{
				this._min = ((this._min.Weight >= this[i].Weight) ? this[i] : this._min);
				this._max = ((this._max.Weight <= this[i].Weight) ? this[i] : this._max);
			}
		}

		// Token: 0x060099E3 RID: 39395 RVA: 0x002254E8 File Offset: 0x002236E8
		private void SortByWeight(RadTagCloudItemCollection filteredItems, bool IsAscending)
		{
			int count = filteredItems.Count;
			filteredItems.Clear();
			if (!IsAscending)
			{
				for (int i = 0; i < count; i++)
				{
					filteredItems.Add(this.ItemContainer.ListOfSortedItems.Keys[i]);
				}
				return;
			}
			if (IsAscending)
			{
				int num;
				int num2;
				if (this.ItemContainer.TakeTopWeightedItems)
				{
					num = count - 1;
					num2 = 0;
				}
				else
				{
					num = this.ItemContainer.ListOfSortedItems.Count - 1;
					num2 = this.ItemContainer.ListOfSortedItems.Count - count;
				}
				for (int j = num; j >= num2; j--)
				{
					filteredItems.Add(this.ItemContainer.ListOfSortedItems.Keys[j]);
				}
			}
		}

		// Token: 0x060099E4 RID: 39396 RVA: 0x0022559C File Offset: 0x0022379C
		private RadTagCloudItemCollection FilterItems(bool takeTopWeightedItems, RadTagCloudItemCollection filteredItems)
		{
			double num = this.ItemContainer.ListOfSortedItems.Values[this.ItemContainer.MaxNumberOfItems - 1];
			if (!takeTopWeightedItems)
			{
				while (filteredItems.Count != this.ItemContainer.MaxNumberOfItems)
				{
					RadTagCloudItem key = filteredItems[this.ItemContainer.MaxNumberOfItems];
					this.ItemContainer.ListOfSortedItems.Remove(key);
					filteredItems.RemoveAt(this.ItemContainer.MaxNumberOfItems);
				}
				return filteredItems;
			}
			int num2 = 0;
			int maxNumberOfItems = this.ItemContainer.MaxNumberOfItems;
			for (int i = 0; i < filteredItems.Count; i++)
			{
				RadTagCloudItem radTagCloudItem = filteredItems[i];
				if (radTagCloudItem.Weight >= num && num2 < maxNumberOfItems && this.ItemContainer.ListOfSortedItems.IndexOfKey(radTagCloudItem) < this.ItemContainer.MaxNumberOfItems)
				{
					num2++;
				}
				else
				{
					filteredItems.Remove(radTagCloudItem);
					i--;
				}
			}
			return filteredItems;
		}

		// Token: 0x04002BAB RID: 11179
		private readonly RadTagCloud _itemContainer;

		// Token: 0x04002BAC RID: 11180
		private RadTagCloudItemCollection _filteredAndSorted;

		// Token: 0x04002BAD RID: 11181
		private RadTagCloudItem _min;

		// Token: 0x04002BAE RID: 11182
		private RadTagCloudItem _max;

		// Token: 0x02000FA6 RID: 4006
		private class WeightComparerAsc : IComparer
		{
			// Token: 0x060099E5 RID: 39397 RVA: 0x0022568C File Offset: 0x0022388C
			public int Compare(object x, object y)
			{
				RadTagCloudItem radTagCloudItem = (RadTagCloudItem)x;
				RadTagCloudItem radTagCloudItem2 = (RadTagCloudItem)y;
				if (radTagCloudItem.Weight == radTagCloudItem2.Weight)
				{
					return 0;
				}
				if (radTagCloudItem.Weight > radTagCloudItem2.Weight)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x02000FA7 RID: 4007
		private class WeightComparerDsc : IComparer
		{
			// Token: 0x060099E7 RID: 39399 RVA: 0x002256D0 File Offset: 0x002238D0
			public int Compare(object x, object y)
			{
				RadTagCloudItem radTagCloudItem = (RadTagCloudItem)x;
				RadTagCloudItem radTagCloudItem2 = (RadTagCloudItem)y;
				if (radTagCloudItem.Weight == radTagCloudItem2.Weight)
				{
					return 0;
				}
				if (radTagCloudItem.Weight > radTagCloudItem2.Weight)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x02000FA8 RID: 4008
		private class AlphabeticComparerAsc : IComparer
		{
			// Token: 0x060099E9 RID: 39401 RVA: 0x00225714 File Offset: 0x00223914
			public int Compare(object x, object y)
			{
				RadTagCloudItem radTagCloudItem = (RadTagCloudItem)x;
				RadTagCloudItem radTagCloudItem2 = (RadTagCloudItem)y;
				return string.Compare(radTagCloudItem.Text, radTagCloudItem2.Text);
			}
		}

		// Token: 0x02000FA9 RID: 4009
		private class AlphabeticComparerDsc : IComparer
		{
			// Token: 0x060099EB RID: 39403 RVA: 0x00225748 File Offset: 0x00223948
			public int Compare(object x, object y)
			{
				RadTagCloudItem radTagCloudItem = (RadTagCloudItem)x;
				RadTagCloudItem radTagCloudItem2 = (RadTagCloudItem)y;
				return string.Compare(radTagCloudItem.Text, radTagCloudItem2.Text) * -1;
			}
		}
	}
}

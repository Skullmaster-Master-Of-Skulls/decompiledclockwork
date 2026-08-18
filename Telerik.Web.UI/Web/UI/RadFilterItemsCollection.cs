using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020018A7 RID: 6311
	public class RadFilterItemsCollection : ICollection<RadFilterExpressionItem>, IEnumerable<RadFilterExpressionItem>, IEnumerable
	{
		// Token: 0x0600F416 RID: 62486 RVA: 0x00378319 File Offset: 0x00376519
		public RadFilterItemsCollection(RadFilterGroupExpressionItem ownerGroup)
		{
			this._ownerGroup = ownerGroup;
			this._items = new List<RadFilterExpressionItem>();
		}

		// Token: 0x1700498F RID: 18831
		public RadFilterExpressionItem this[int index]
		{
			get
			{
				return this._items[index];
			}
		}

		// Token: 0x17004990 RID: 18832
		public RadFilterExpressionItem this[string hierarchicalIndex]
		{
			get
			{
				RadFilterExpressionItem radFilterExpressionItem = this.FindItemByHierarchicalIndex(hierarchicalIndex);
				if (radFilterExpressionItem == null)
				{
					throw new ArgumentOutOfRangeException("HierarchicalIndex");
				}
				return radFilterExpressionItem;
			}
		}

		// Token: 0x0600F419 RID: 62489 RVA: 0x00378368 File Offset: 0x00376568
		internal RadFilterExpressionItem FindItemByHierarchicalIndex(string hierarchicalIndex)
		{
			if (string.Compare(hierarchicalIndex, this._ownerGroup.HierarchicalIndex, true) == 0)
			{
				return this._ownerGroup;
			}
			RadFilterExpressionItem radFilterExpressionItem = null;
			int num = this._ownerGroup.HierarchicalIndex.Split(new char[]
			{
				'_'
			}).Length;
			string[] array = hierarchicalIndex.Split(new char[]
			{
				'_'
			});
			int index = array.Length - num;
			while (index-- > 0)
			{
				RadFilterItemsCollection radFilterItemsCollection;
				if (radFilterExpressionItem == null)
				{
					radFilterItemsCollection = this;
				}
				else
				{
					radFilterItemsCollection = ((RadFilterGroupExpressionItem)radFilterExpressionItem).ChildItems;
				}
				radFilterExpressionItem = radFilterItemsCollection[Convert.ToInt32(array.GetValue(index))];
			}
			return radFilterExpressionItem;
		}

		// Token: 0x0600F41A RID: 62490 RVA: 0x00378406 File Offset: 0x00376606
		public void Add(RadFilterExpressionItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			item.SetOwnerGroup(this._ownerGroup);
			item.SetItemIndex(this.Count);
			this._items.Add(item);
		}

		// Token: 0x0600F41B RID: 62491 RVA: 0x0037843A File Offset: 0x0037663A
		public void Clear()
		{
			this._items.Clear();
		}

		// Token: 0x0600F41C RID: 62492 RVA: 0x00378447 File Offset: 0x00376647
		public bool Contains(RadFilterExpressionItem item)
		{
			return this[item.ItemIndex] != null;
		}

		// Token: 0x0600F41D RID: 62493 RVA: 0x0037845B File Offset: 0x0037665B
		public void CopyTo(RadFilterExpressionItem[] array, int arrayIndex)
		{
			this._items.CopyTo(array, arrayIndex);
		}

		// Token: 0x17004991 RID: 18833
		// (get) Token: 0x0600F41E RID: 62494 RVA: 0x0037846A File Offset: 0x0037666A
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x17004992 RID: 18834
		// (get) Token: 0x0600F41F RID: 62495 RVA: 0x00378477 File Offset: 0x00376677
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600F420 RID: 62496 RVA: 0x0037847A File Offset: 0x0037667A
		public bool Remove(RadFilterExpressionItem item)
		{
			return this._items.Remove(item);
		}

		// Token: 0x0600F421 RID: 62497 RVA: 0x00378488 File Offset: 0x00376688
		public IEnumerator<RadFilterExpressionItem> GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x0600F422 RID: 62498 RVA: 0x00378495 File Offset: 0x00376695
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004606 RID: 17926
		private RadFilterGroupExpressionItem _ownerGroup;

		// Token: 0x04004607 RID: 17927
		private IList<RadFilterExpressionItem> _items;
	}
}

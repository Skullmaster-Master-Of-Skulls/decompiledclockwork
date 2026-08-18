using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001147 RID: 4423
	public class GridItemCollection : ICollection, IEnumerable
	{
		// Token: 0x0600B435 RID: 46133 RVA: 0x0027709A File Offset: 0x0027529A
		public GridItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x0600B436 RID: 46134 RVA: 0x002770A9 File Offset: 0x002752A9
		public GridItemCollection()
		{
			this.items = new ArrayList();
		}

		// Token: 0x0600B437 RID: 46135 RVA: 0x002770BC File Offset: 0x002752BC
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600B438 RID: 46136 RVA: 0x002770CB File Offset: 0x002752CB
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x17003A3E RID: 14910
		// (get) Token: 0x0600B439 RID: 46137 RVA: 0x002770D8 File Offset: 0x002752D8
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17003A3F RID: 14911
		// (get) Token: 0x0600B43A RID: 46138 RVA: 0x002770E5 File Offset: 0x002752E5
		public bool IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x17003A40 RID: 14912
		// (get) Token: 0x0600B43B RID: 46139 RVA: 0x002770F2 File Offset: 0x002752F2
		public bool IsSynchronized
		{
			get
			{
				return this.items.IsSynchronized;
			}
		}

		// Token: 0x17003A41 RID: 14913
		public GridItem this[int index]
		{
			get
			{
				return (GridItem)this.items[index];
			}
		}

		// Token: 0x17003A42 RID: 14914
		public GridItem this[string hierarchicalIndex]
		{
			get
			{
				GridItem gridItem = this.FindByHierarchyIndex(hierarchicalIndex);
				if (gridItem == null)
				{
					throw new ArgumentOutOfRangeException("ItemHierarchicalIndex");
				}
				return gridItem;
			}
		}

		// Token: 0x0600B43E RID: 46142 RVA: 0x00277138 File Offset: 0x00275338
		internal GridItem FindByHierarchyIndex(string hierarchicalIndex)
		{
			GridItem result = null;
			foreach (object obj in this.items)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem.ItemIndexHierarchical == hierarchicalIndex)
				{
					result = gridItem;
					break;
				}
			}
			return result;
		}

		// Token: 0x17003A43 RID: 14915
		// (get) Token: 0x0600B43F RID: 46143 RVA: 0x002771A0 File Offset: 0x002753A0
		public object SyncRoot
		{
			get
			{
				return this.items.SyncRoot;
			}
		}

		// Token: 0x0600B440 RID: 46144 RVA: 0x002771AD File Offset: 0x002753AD
		internal void Add(GridItem item)
		{
			this.items.Add(item);
		}

		// Token: 0x0600B441 RID: 46145 RVA: 0x002771BC File Offset: 0x002753BC
		internal void AddRange(GridItemCollection items)
		{
			this.items.AddRange(items);
		}

		// Token: 0x0600B442 RID: 46146 RVA: 0x002771CA File Offset: 0x002753CA
		internal void AddRange(GridItem[] items)
		{
			this.items.AddRange(items);
		}

		// Token: 0x04002F69 RID: 12137
		private ArrayList items;
	}
}

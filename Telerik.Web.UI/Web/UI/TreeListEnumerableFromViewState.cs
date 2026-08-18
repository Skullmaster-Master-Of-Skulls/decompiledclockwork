using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001291 RID: 4753
	internal class TreeListEnumerableFromViewState : TreeListEnumerableBase
	{
		// Token: 0x0600C638 RID: 50744 RVA: 0x002C3A6E File Offset: 0x002C1C6E
		public TreeListEnumerableFromViewState(TreeListControlStateManager viewState) : base(null, null)
		{
			this._viewState = viewState;
		}

		// Token: 0x0600C639 RID: 50745 RVA: 0x002C3A7F File Offset: 0x002C1C7F
		protected override void TransformEnumerable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C63A RID: 50746 RVA: 0x002C3A86 File Offset: 0x002C1C86
		public override IEnumerable<TreeListSourceItem> RawEnumerable()
		{
			return new TreeListEnumerableFromViewState.TreeListDummyDataSource(this);
		}

		// Token: 0x17004004 RID: 16388
		// (get) Token: 0x0600C63B RID: 50747 RVA: 0x002C3A90 File Offset: 0x002C1C90
		public TreeListItemStateCollection ItemState
		{
			get
			{
				if (this._itemState == null)
				{
					this._itemState = (TreeListItemStateCollection)this._viewState["_!ItemState"];
					if (this._itemState == null)
					{
						this._itemState = new TreeListItemStateCollection();
						this._viewState["_!ItemState"] = this._itemState;
					}
				}
				return this._itemState;
			}
		}

		// Token: 0x17004005 RID: 16389
		// (get) Token: 0x0600C63C RID: 50748 RVA: 0x002C3AF0 File Offset: 0x002C1CF0
		public override int DataSourceCount
		{
			get
			{
				object obj = this._viewState["_!DSIC"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x17004006 RID: 16390
		// (get) Token: 0x0600C63D RID: 50749 RVA: 0x002C3B1C File Offset: 0x002C1D1C
		public override int Count
		{
			get
			{
				object obj = this._viewState["_!ItemCount"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x17004007 RID: 16391
		// (get) Token: 0x0600C63E RID: 50750 RVA: 0x002C3B45 File Offset: 0x002C1D45
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600C63F RID: 50751 RVA: 0x002C3B48 File Offset: 0x002C1D48
		internal override IEnumerable<TreeListSourceItem> GetItemsToDelete(TreeListDeleteContext context)
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C640 RID: 50752 RVA: 0x002C3B54 File Offset: 0x002C1D54
		internal override IEnumerable<TreeListSourceItem> GetItemsToReorder(TreeListReorderContext context)
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C641 RID: 50753 RVA: 0x002C3B60 File Offset: 0x002C1D60
		internal override void AdjustReorderedIndexes(TreeListReorderContext context)
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C642 RID: 50754 RVA: 0x002C3B6C File Offset: 0x002C1D6C
		public override List<TreeListHierarchyIndex> GetExpandedItems()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0400346B RID: 13419
		private readonly TreeListControlStateManager _viewState;

		// Token: 0x0400346C RID: 13420
		private TreeListItemStateCollection _itemState;

		// Token: 0x02001292 RID: 4754
		internal class TreeListDummyDataSource : ICollection, IEnumerable<TreeListSourceItem>, IEnumerable
		{
			// Token: 0x0600C643 RID: 50755 RVA: 0x002C3B78 File Offset: 0x002C1D78
			public TreeListDummyDataSource(TreeListEnumerableFromViewState treeListEnumerable)
			{
				this._treeListEnumerable = treeListEnumerable;
			}

			// Token: 0x0600C644 RID: 50756 RVA: 0x002C3B88 File Offset: 0x002C1D88
			public void CopyTo(Array array, int index)
			{
				foreach (TreeListSourceItem value in this)
				{
					array.SetValue(value, index++);
				}
			}

			// Token: 0x17004008 RID: 16392
			// (get) Token: 0x0600C645 RID: 50757 RVA: 0x002C3BD8 File Offset: 0x002C1DD8
			public int Count
			{
				get
				{
					return this._treeListEnumerable.Count;
				}
			}

			// Token: 0x17004009 RID: 16393
			// (get) Token: 0x0600C646 RID: 50758 RVA: 0x002C3BE5 File Offset: 0x002C1DE5
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700400A RID: 16394
			// (get) Token: 0x0600C647 RID: 50759 RVA: 0x002C3BE8 File Offset: 0x002C1DE8
			public object SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600C648 RID: 50760 RVA: 0x002C3CF8 File Offset: 0x002C1EF8
			public IEnumerator<TreeListSourceItem> GetEnumerator()
			{
				for (int i = 0; i < this._treeListEnumerable.Count; i++)
				{
					KeyValuePair<TreeListHierarchyIndex, TreeListItemState> itemState = this._treeListEnumerable.ItemState[i];
					TreeListSourceItem sourceItem = new TreeListSourceItem
					{
						HierarchyIndex = itemState.Key,
						ItemState = itemState.Value
					};
					yield return sourceItem;
				}
				yield break;
			}

			// Token: 0x0600C649 RID: 50761 RVA: 0x002C3D14 File Offset: 0x002C1F14
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400346D RID: 13421
			private readonly TreeListEnumerableFromViewState _treeListEnumerable;
		}
	}
}

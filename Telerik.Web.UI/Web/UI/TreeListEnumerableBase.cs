using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001288 RID: 4744
	public abstract class TreeListEnumerableBase
	{
		// Token: 0x0600C5DE RID: 50654 RVA: 0x002C332D File Offset: 0x002C152D
		public TreeListEnumerableBase(IList<string> keyNames, IList<string> parentKeyNames)
		{
			this.KeyNames = keyNames;
			this.ParentKeyNames = parentKeyNames;
		}

		// Token: 0x17003FE2 RID: 16354
		// (get) Token: 0x0600C5DF RID: 50655 RVA: 0x002C3343 File Offset: 0x002C1543
		// (set) Token: 0x0600C5E0 RID: 50656 RVA: 0x002C334B File Offset: 0x002C154B
		public TreeListDataColumns Columns { get; internal set; }

		// Token: 0x17003FE3 RID: 16355
		// (get) Token: 0x0600C5E1 RID: 50657 RVA: 0x002C3354 File Offset: 0x002C1554
		// (set) Token: 0x0600C5E2 RID: 50658 RVA: 0x002C335C File Offset: 0x002C155C
		public bool AutogenerateColumns { get; set; }

		// Token: 0x0600C5E3 RID: 50659
		protected abstract void TransformEnumerable();

		// Token: 0x0600C5E4 RID: 50660
		public abstract IEnumerable<TreeListSourceItem> RawEnumerable();

		// Token: 0x0600C5E5 RID: 50661
		public abstract List<TreeListHierarchyIndex> GetExpandedItems();

		// Token: 0x0600C5E6 RID: 50662
		internal abstract IEnumerable<TreeListSourceItem> GetItemsToDelete(TreeListDeleteContext context);

		// Token: 0x0600C5E7 RID: 50663
		internal abstract IEnumerable<TreeListSourceItem> GetItemsToReorder(TreeListReorderContext context);

		// Token: 0x0600C5E8 RID: 50664
		internal abstract void AdjustReorderedIndexes(TreeListReorderContext context);

		// Token: 0x17003FE4 RID: 16356
		// (get) Token: 0x0600C5E9 RID: 50665 RVA: 0x002C3365 File Offset: 0x002C1565
		// (set) Token: 0x0600C5EA RID: 50666 RVA: 0x002C336D File Offset: 0x002C156D
		public virtual RadTreeList OwnerTreeList { get; internal set; }

		// Token: 0x17003FE5 RID: 16357
		// (get) Token: 0x0600C5EB RID: 50667 RVA: 0x002C3376 File Offset: 0x002C1576
		public static TreeListEnumerableBase Null
		{
			get
			{
				return TreeListEnumerableBase._nullEnumerable;
			}
		}

		// Token: 0x17003FE6 RID: 16358
		// (get) Token: 0x0600C5EC RID: 50668
		public abstract int DataSourceCount { get; }

		// Token: 0x17003FE7 RID: 16359
		// (get) Token: 0x0600C5ED RID: 50669 RVA: 0x002C337D File Offset: 0x002C157D
		public virtual int Count
		{
			get
			{
				return this.DataSourceCount;
			}
		}

		// Token: 0x17003FE8 RID: 16360
		// (get) Token: 0x0600C5EE RID: 50670 RVA: 0x002C3385 File Offset: 0x002C1585
		public TreeListEnumerableHelper EnumerableHelper
		{
			get
			{
				if (this._enumerableHelper == null)
				{
					this._enumerableHelper = new TreeListEnumerableHelper(this, this.KeyNames, this.ParentKeyNames);
				}
				return this._enumerableHelper;
			}
		}

		// Token: 0x17003FE9 RID: 16361
		// (get) Token: 0x0600C5EF RID: 50671 RVA: 0x002C33AD File Offset: 0x002C15AD
		public TreeListPagingManager PagingManager
		{
			get
			{
				if (this._pagingManager == null)
				{
					this._pagingManager = new TreeListPagingManager(this);
				}
				return this._pagingManager;
			}
		}

		// Token: 0x17003FEA RID: 16362
		// (get) Token: 0x0600C5F0 RID: 50672 RVA: 0x002C33C9 File Offset: 0x002C15C9
		public virtual bool SupportsPaging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003FEB RID: 16363
		// (get) Token: 0x0600C5F1 RID: 50673 RVA: 0x002C33CC File Offset: 0x002C15CC
		// (set) Token: 0x0600C5F2 RID: 50674 RVA: 0x002C33D4 File Offset: 0x002C15D4
		public IList<string> KeyNames { get; internal set; }

		// Token: 0x17003FEC RID: 16364
		// (get) Token: 0x0600C5F3 RID: 50675 RVA: 0x002C33DD File Offset: 0x002C15DD
		// (set) Token: 0x0600C5F4 RID: 50676 RVA: 0x002C33E5 File Offset: 0x002C15E5
		public IList<string> ParentKeyNames { get; internal set; }

		// Token: 0x17003FED RID: 16365
		// (get) Token: 0x0600C5F5 RID: 50677 RVA: 0x002C33EE File Offset: 0x002C15EE
		public virtual bool SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600C5F6 RID: 50678 RVA: 0x002C33F1 File Offset: 0x002C15F1
		public virtual void SetSortExpressions(TreeListSortExpressionCollection sortExpressions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400344C RID: 13388
		private static readonly TreeListNullEnumerable _nullEnumerable = new TreeListNullEnumerable();

		// Token: 0x0400344D RID: 13389
		private TreeListEnumerableHelper _enumerableHelper;

		// Token: 0x0400344E RID: 13390
		private TreeListPagingManager _pagingManager;
	}
}

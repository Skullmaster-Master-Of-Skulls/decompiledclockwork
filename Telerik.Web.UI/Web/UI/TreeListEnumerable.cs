using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001289 RID: 4745
	public class TreeListEnumerable : TreeListEnumerableBase
	{
		// Token: 0x0600C5F8 RID: 50680 RVA: 0x002C3404 File Offset: 0x002C1604
		public TreeListEnumerable(IEnumerable originalEnumerable) : this(originalEnumerable, null, null, null)
		{
		}

		// Token: 0x0600C5F9 RID: 50681 RVA: 0x002C3410 File Offset: 0x002C1610
		public TreeListEnumerable(IEnumerable originalEnumerable, IList<string> keyNames, IList<string> parentKeyNames, List<TreeListHierarchyIndex> expandedItems) : base(keyNames, parentKeyNames)
		{
			this._originalEnumerable = originalEnumerable;
			this._transformedEnumerable = null;
			this._expandedItems = expandedItems;
		}

		// Token: 0x0600C5FA RID: 50682 RVA: 0x002C3430 File Offset: 0x002C1630
		protected override void TransformEnumerable()
		{
			if (!this._isTransformed)
			{
				if (this._originalEnumerable is List<SiteMapNodeWrapper>)
				{
					base.KeyNames = new string[]
					{
						"Node"
					};
					base.ParentKeyNames = new string[]
					{
						"ParentNode"
					};
				}
				this.PerformTransformation();
				this._isTransformed = true;
			}
		}

		// Token: 0x0600C5FB RID: 50683 RVA: 0x002C348C File Offset: 0x002C168C
		public virtual void PerformTransformation()
		{
			TreeListGroupingContext context = new TreeListGroupingContext(this.GetExpandedItems());
			base.EnumerableHelper.SetSortExpressions(this.SortExpression);
			if (this.OwnerTreeList.AllowLoadOnDemand)
			{
				this._transformedEnumerable = base.EnumerableHelper.GroupEnumerableWhenLoadOnDemand(this._originalEnumerable, context);
				this.OwnerTreeList.ExpandedIndexes.Clear();
				this.OwnerTreeList.ExpandedIndexes.AddHash(this.OwnerTreeList.LoadOnDemandContext.ExpandedItems);
			}
			else
			{
				this._transformedEnumerable = base.EnumerableHelper.GroupEnumerable(this._originalEnumerable, context);
			}
			this.OwnerTreeList.TotalItemCount = base.EnumerableHelper.TotalItemCount;
			this.OwnerTreeList.RootItems = base.EnumerableHelper.RootItems;
			if (this._transformedEnumerable != null)
			{
				if (base.PagingManager.AllowPaging)
				{
					int num = Math.Min(base.PagingManager.CurrentPageIndex, base.PagingManager.PageCount - 1);
					if (num != base.PagingManager.CurrentPageIndex)
					{
						base.PagingManager.CurrentPageIndex = (this.OwnerTreeList.CurrentPageIndex = num);
					}
					int startIndex = base.PagingManager.CurrentPageIndex * base.PagingManager.PageSize;
					this.EnsureDataSourceCount();
					this._transformedEnumerable = base.EnumerableHelper.GetPage(this._transformedEnumerable, startIndex, base.PagingManager.PageSize);
					return;
				}
				this._transformedEnumerable = base.EnumerableHelper.FinalizeItemsState(this._transformedEnumerable);
				if (this.OwnerTreeList.ShowFooter)
				{
					List<TreeListSourceItem> list = new List<TreeListSourceItem>();
					foreach (TreeListSourceItem item in this._transformedEnumerable)
					{
						list.Add(item);
					}
					base.EnumerableHelper.PrepareFooters(list);
				}
			}
		}

		// Token: 0x0600C5FC RID: 50684 RVA: 0x002C3674 File Offset: 0x002C1874
		internal override IEnumerable<TreeListSourceItem> GetItemsToDelete(TreeListDeleteContext context)
		{
			return base.EnumerableHelper.PrepareItemsForDelete(this._originalEnumerable, context);
		}

		// Token: 0x0600C5FD RID: 50685 RVA: 0x002C3688 File Offset: 0x002C1888
		internal override IEnumerable<TreeListSourceItem> GetItemsToReorder(TreeListReorderContext context)
		{
			base.EnumerableHelper.SetReorderContext(context);
			return base.EnumerableHelper.PrepareItemsAfterReorder(this._originalEnumerable);
		}

		// Token: 0x0600C5FE RID: 50686 RVA: 0x002C36A7 File Offset: 0x002C18A7
		internal override void AdjustReorderedIndexes(TreeListReorderContext context)
		{
			base.EnumerableHelper.SetReorderContext(context);
		}

		// Token: 0x0600C5FF RID: 50687 RVA: 0x002C36B5 File Offset: 0x002C18B5
		public override IEnumerable<TreeListSourceItem> RawEnumerable()
		{
			this.TransformEnumerable();
			return this._transformedEnumerable;
		}

		// Token: 0x0600C600 RID: 50688 RVA: 0x002C36C3 File Offset: 0x002C18C3
		protected virtual void EnsureDataSourceCount()
		{
			int dataSourceCount = this.DataSourceCount;
		}

		// Token: 0x17003FEE RID: 16366
		// (get) Token: 0x0600C601 RID: 50689 RVA: 0x002C36CC File Offset: 0x002C18CC
		public override int DataSourceCount
		{
			get
			{
				if (this._dataSourceCount == null)
				{
					this._dataSourceCount = new int?(base.EnumerableHelper.GetCount<TreeListSourceItem>(this._transformedEnumerable));
				}
				return this._dataSourceCount.Value;
			}
		}

		// Token: 0x17003FEF RID: 16367
		// (get) Token: 0x0600C602 RID: 50690 RVA: 0x002C3704 File Offset: 0x002C1904
		public override int Count
		{
			get
			{
				this.TransformEnumerable();
				if (this._count == null)
				{
					if (base.PagingManager.AllowPaging)
					{
						this._count = new int?(base.EnumerableHelper.GetCount<TreeListSourceItem>(this._transformedEnumerable));
					}
					else
					{
						this._count = new int?(this.DataSourceCount);
					}
				}
				return this._count.Value;
			}
		}

		// Token: 0x17003FF0 RID: 16368
		// (get) Token: 0x0600C603 RID: 50691 RVA: 0x002C376B File Offset: 0x002C196B
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003FF1 RID: 16369
		// (get) Token: 0x0600C604 RID: 50692 RVA: 0x002C376E File Offset: 0x002C196E
		public override bool SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600C605 RID: 50693 RVA: 0x002C3771 File Offset: 0x002C1971
		public override void SetSortExpressions(TreeListSortExpressionCollection sortExpressions)
		{
			this.SortExpression = sortExpressions;
		}

		// Token: 0x17003FF2 RID: 16370
		// (get) Token: 0x0600C606 RID: 50694 RVA: 0x002C377A File Offset: 0x002C197A
		// (set) Token: 0x0600C607 RID: 50695 RVA: 0x002C3795 File Offset: 0x002C1995
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual TreeListSortExpressionCollection SortExpression
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new TreeListSortExpressionCollection();
				}
				return this._sortExpressions;
			}
			protected set
			{
				this._sortExpressions = value;
			}
		}

		// Token: 0x0600C608 RID: 50696 RVA: 0x002C379E File Offset: 0x002C199E
		public override List<TreeListHierarchyIndex> GetExpandedItems()
		{
			return this._expandedItems;
		}

		// Token: 0x04003454 RID: 13396
		private readonly IEnumerable _originalEnumerable;

		// Token: 0x04003455 RID: 13397
		private IEnumerable<TreeListSourceItem> _transformedEnumerable;

		// Token: 0x04003456 RID: 13398
		private bool _isTransformed;

		// Token: 0x04003457 RID: 13399
		private int? _dataSourceCount;

		// Token: 0x04003458 RID: 13400
		private int? _count;

		// Token: 0x04003459 RID: 13401
		private List<TreeListHierarchyIndex> _expandedItems;

		// Token: 0x0400345A RID: 13402
		private TreeListSortExpressionCollection _sortExpressions;
	}
}

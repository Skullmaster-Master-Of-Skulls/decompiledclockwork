using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020000C7 RID: 199
	internal class ProxyBoundControlPagableEnumerable : ProxyBoundControlEnumerableBase
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		public ProxyBoundControlPagableEnumerable(RadProxyBoundControl ownerProxyBoundControl, IEnumerable rawEnumerable)
		{
			this.ownerProxyBoundControl = ownerProxyBoundControl;
			this._originalEnumerable = rawEnumerable;
			this._transformedEnumerable = null;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001CB15 File Offset: 0x0001AD15
		private ListViewEnumerableHelper EnumerableHelper
		{
			get
			{
				if (this._enumerableHelper == null)
				{
					this._enumerableHelper = ListViewEnumerableHelper.Instantiate(this._originalEnumerable, false);
					this._enumerableHelper.IsBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
				}
				return this._enumerableHelper;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001CB48 File Offset: 0x0001AD48
		public override IEnumerable RawEnumerable()
		{
			this.TransformEnumerable();
			return this._transformedEnumerable;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001CB56 File Offset: 0x0001AD56
		protected override void TransformEnumerable()
		{
			if (this._isTransformed)
			{
				return;
			}
			this.PerformTransformation();
			this._isTransformed = true;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001CB70 File Offset: 0x0001AD70
		private void PerformTransformation()
		{
			this._transformedEnumerable = this._originalEnumerable;
			if (this.SupportsSorting && this.SortExpression.Count > 0 && this._transformedEnumerable != null && !base.AllowCustomSorting)
			{
				this._transformedEnumerable = this.EnumerableHelper.Sort(this._originalEnumerable, this.SortExpression);
			}
			if (this.SupportsFiltering && this.FilterExpressions.Count > 0 && base.ShouldApplyFiltering && this._transformedEnumerable != null)
			{
				this._transformedEnumerable = this.EnumerableHelper.Filter(this._transformedEnumerable, this.FilterExpressions);
				this.EnsureDataSourceCount();
			}
			if (this.PagingManager.AllowPaging && this._transformedEnumerable != null)
			{
				int startIndex = this.PagingManager.CurrentPageIndex * this.PagingManager.PageSize;
				if (this.PagingManager.AllowCustomPaging)
				{
					startIndex = 0;
				}
				this._transformedEnumerable = this.EnumerableHelper.GetPage(this._transformedEnumerable, startIndex, this.PagingManager.PageSize);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001CC74 File Offset: 0x0001AE74
		private void EnsureDataSourceCount()
		{
			int dataSourceCount = this.DataSourceCount;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001CC80 File Offset: 0x0001AE80
		public override int DataSourceCount
		{
			get
			{
				if (this._dataSourceCount == null)
				{
					if (this.FilterExpressions.Count > 0)
					{
						this._dataSourceCount = new int?(this.EnumerableHelper.GetCount(this._transformedEnumerable));
					}
					else
					{
						this._dataSourceCount = new int?(this.EnumerableHelper.GetCount(this._originalEnumerable));
					}
				}
				return this._dataSourceCount.Value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		public override int Count
		{
			get
			{
				this.TransformEnumerable();
				if (this._count == null)
				{
					this._count = new int?(this.PagingManager.AllowPaging ? this.EnumerableHelper.GetCount(this._transformedEnumerable) : this.DataSourceCount);
				}
				return this._count.Value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001CD4F File Offset: 0x0001AF4F
		public override bool SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001CD52 File Offset: 0x0001AF52
		public override bool SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001CD55 File Offset: 0x0001AF55
		public override void SetSortExpressions(RadListViewSortExpressionCollection sortExpressions)
		{
			this.SortExpression = sortExpressions;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001CD5E File Offset: 0x0001AF5E
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0001CD79 File Offset: 0x0001AF79
		public virtual RadListViewSortExpressionCollection SortExpression
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new RadListViewSortExpressionCollection();
				}
				return this._sortExpressions;
			}
			protected set
			{
				this._sortExpressions = value;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001CD82 File Offset: 0x0001AF82
		public override void SetFilteringExpressions(RadListViewFilterExpressionCollection filterExpressions)
		{
			this.FilterExpressions = filterExpressions;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001CDB1 File Offset: 0x0001AFB1
		public virtual RadListViewFilterExpressionCollection FilterExpressions
		{
			get
			{
				RadListViewFilterExpressionCollection result;
				if ((result = this._filterExpressions) == null)
				{
					result = (this._filterExpressions = new RadListViewFilterExpressionCollection());
				}
				return result;
			}
			protected set
			{
				this._filterExpressions = value;
			}
		}

		// Token: 0x040001C2 RID: 450
		private readonly IEnumerable _originalEnumerable;

		// Token: 0x040001C3 RID: 451
		private IEnumerable _transformedEnumerable;

		// Token: 0x040001C4 RID: 452
		private int? _dataSourceCount;

		// Token: 0x040001C5 RID: 453
		private bool _isTransformed;

		// Token: 0x040001C6 RID: 454
		private int? _count;

		// Token: 0x040001C7 RID: 455
		private RadListViewSortExpressionCollection _sortExpressions;

		// Token: 0x040001C8 RID: 456
		private ListViewEnumerableHelper _enumerableHelper;

		// Token: 0x040001C9 RID: 457
		private RadListViewFilterExpressionCollection _filterExpressions;

		// Token: 0x040001CA RID: 458
		private RadProxyBoundControl ownerProxyBoundControl;
	}
}

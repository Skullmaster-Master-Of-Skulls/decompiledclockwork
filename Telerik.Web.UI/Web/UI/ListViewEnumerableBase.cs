using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020019A1 RID: 6561
	public abstract class ListViewEnumerableBase
	{
		// Token: 0x0600FDBB RID: 64955
		public abstract IEnumerable RawEnumerable();

		// Token: 0x17004C97 RID: 19607
		// (get) Token: 0x0600FDBC RID: 64956 RVA: 0x0038FD5D File Offset: 0x0038DF5D
		// (set) Token: 0x0600FDBD RID: 64957 RVA: 0x0038FD65 File Offset: 0x0038DF65
		internal bool IsBoundUsingDataSourceID { get; set; }

		// Token: 0x17004C98 RID: 19608
		// (get) Token: 0x0600FDBE RID: 64958 RVA: 0x0038FD6E File Offset: 0x0038DF6E
		public static ListViewEnumerableBase Null
		{
			get
			{
				return ListViewEnumerableBase._null;
			}
		}

		// Token: 0x17004C99 RID: 19609
		// (get) Token: 0x0600FDBF RID: 64959 RVA: 0x0038FD75 File Offset: 0x0038DF75
		public virtual bool SupportsPaging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004C9A RID: 19610
		// (get) Token: 0x0600FDC0 RID: 64960 RVA: 0x0038FD78 File Offset: 0x0038DF78
		// (set) Token: 0x0600FDC1 RID: 64961 RVA: 0x0038FD80 File Offset: 0x0038DF80
		public bool AllowCustomSorting { get; internal set; }

		// Token: 0x0600FDC2 RID: 64962
		protected abstract void TransformEnumerable();

		// Token: 0x17004C9B RID: 19611
		// (get) Token: 0x0600FDC3 RID: 64963
		public abstract int DataSourceCount { get; }

		// Token: 0x17004C9C RID: 19612
		// (get) Token: 0x0600FDC4 RID: 64964 RVA: 0x0038FD89 File Offset: 0x0038DF89
		public virtual int Count
		{
			get
			{
				return this.DataSourceCount;
			}
		}

		// Token: 0x17004C9D RID: 19613
		// (get) Token: 0x0600FDC5 RID: 64965 RVA: 0x0038FD91 File Offset: 0x0038DF91
		// (set) Token: 0x0600FDC6 RID: 64966 RVA: 0x0038FD99 File Offset: 0x0038DF99
		internal bool ShouldApplyFiltering { get; set; }

		// Token: 0x17004C9E RID: 19614
		// (get) Token: 0x0600FDC7 RID: 64967 RVA: 0x0038FDA2 File Offset: 0x0038DFA2
		public virtual RadListViewPagingManager PagingManager
		{
			get
			{
				if (this._pagingManager == null)
				{
					this._pagingManager = new RadListViewPagingManager(this);
				}
				return this._pagingManager;
			}
		}

		// Token: 0x17004C9F RID: 19615
		// (get) Token: 0x0600FDC8 RID: 64968 RVA: 0x0038FDBE File Offset: 0x0038DFBE
		public virtual bool SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004CA0 RID: 19616
		// (get) Token: 0x0600FDC9 RID: 64969 RVA: 0x0038FDC1 File Offset: 0x0038DFC1
		public virtual bool SupportsFiltering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600FDCA RID: 64970 RVA: 0x0038FDC4 File Offset: 0x0038DFC4
		public virtual void SetSortExpressions(RadListViewSortExpressionCollection sortExpressions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600FDCB RID: 64971 RVA: 0x0038FDCB File Offset: 0x0038DFCB
		public virtual void SetFilteringExpressions(RadListViewFilterExpressionCollection sortExpressions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600FDCC RID: 64972 RVA: 0x0038FDD2 File Offset: 0x0038DFD2
		public virtual RadListViewInsertionObject GetInsertionObject(IDictionary values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04004803 RID: 18435
		private static ListViewEnumerableBase _null = new ListViewNullEnumerable();

		// Token: 0x04004804 RID: 18436
		private RadListViewPagingManager _pagingManager;
	}
}

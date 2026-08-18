using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020019A4 RID: 6564
	public class ListViewPagableEnumerable : ListViewEnumerableBase
	{
		// Token: 0x0600FDE2 RID: 64994 RVA: 0x0038FF10 File Offset: 0x0038E110
		public ListViewPagableEnumerable(RadListView ownerListView, IEnumerable rawEnumerable)
		{
			this.ownerListView = ownerListView;
			this._originalEnumerable = rawEnumerable;
			this._transformedEnumerable = null;
		}

		// Token: 0x17004CAB RID: 19627
		// (get) Token: 0x0600FDE3 RID: 64995 RVA: 0x0038FF2D File Offset: 0x0038E12D
		private ListViewEnumerableHelper EnumerableHelper
		{
			get
			{
				if (this._enumerableHelper == null)
				{
					this._enumerableHelper = ListViewEnumerableHelper.Instantiate(this._originalEnumerable, this.ownerListView.AllowStableSort);
					this._enumerableHelper.IsBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
				}
				return this._enumerableHelper;
			}
		}

		// Token: 0x0600FDE4 RID: 64996 RVA: 0x0038FF6A File Offset: 0x0038E16A
		public override IEnumerable RawEnumerable()
		{
			this.TransformEnumerable();
			return this._transformedEnumerable;
		}

		// Token: 0x0600FDE5 RID: 64997 RVA: 0x0038FF78 File Offset: 0x0038E178
		protected override void TransformEnumerable()
		{
			if (this._isTransformed)
			{
				return;
			}
			this.PerformTransformation();
			this._isTransformed = true;
		}

		// Token: 0x0600FDE6 RID: 64998 RVA: 0x0038FF90 File Offset: 0x0038E190
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
			if (this._transformedEnumerable != null && this.ownerListView.DataGroups != null && this.ownerListView.DataGroups.Count > 0 && this._transformedEnumerable.GetType().GetInterface("IDataReader") == null)
			{
				ListViewLinqGroupingHelper listViewLinqGroupingHelper = new ListViewLinqGroupingHelper(this.ownerListView, this.PagingManager);
				this._transformedEnumerable = listViewLinqGroupingHelper.GroupDataItems(this._transformedEnumerable);
				return;
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

		// Token: 0x0600FDE7 RID: 64999 RVA: 0x003900FE File Offset: 0x0038E2FE
		private void EnsureDataSourceCount()
		{
			int dataSourceCount = this.DataSourceCount;
		}

		// Token: 0x17004CAC RID: 19628
		// (get) Token: 0x0600FDE8 RID: 65000 RVA: 0x00390108 File Offset: 0x0038E308
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

		// Token: 0x17004CAD RID: 19629
		// (get) Token: 0x0600FDE9 RID: 65001 RVA: 0x00390178 File Offset: 0x0038E378
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

		// Token: 0x17004CAE RID: 19630
		// (get) Token: 0x0600FDEA RID: 65002 RVA: 0x003901D4 File Offset: 0x0038E3D4
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004CAF RID: 19631
		// (get) Token: 0x0600FDEB RID: 65003 RVA: 0x003901D7 File Offset: 0x0038E3D7
		public override bool SupportsSorting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004CB0 RID: 19632
		// (get) Token: 0x0600FDEC RID: 65004 RVA: 0x003901DA File Offset: 0x0038E3DA
		public override bool SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600FDED RID: 65005 RVA: 0x003901DD File Offset: 0x0038E3DD
		public override void SetSortExpressions(RadListViewSortExpressionCollection sortExpressions)
		{
			this.SortExpression = sortExpressions;
		}

		// Token: 0x17004CB1 RID: 19633
		// (get) Token: 0x0600FDEE RID: 65006 RVA: 0x003901E6 File Offset: 0x0038E3E6
		// (set) Token: 0x0600FDEF RID: 65007 RVA: 0x00390201 File Offset: 0x0038E401
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

		// Token: 0x0600FDF0 RID: 65008 RVA: 0x0039020A File Offset: 0x0038E40A
		public override void SetFilteringExpressions(RadListViewFilterExpressionCollection filterExpressions)
		{
			this.FilterExpressions = filterExpressions;
		}

		// Token: 0x17004CB2 RID: 19634
		// (get) Token: 0x0600FDF1 RID: 65009 RVA: 0x00390214 File Offset: 0x0038E414
		// (set) Token: 0x0600FDF2 RID: 65010 RVA: 0x00390239 File Offset: 0x0038E439
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

		// Token: 0x0600FDF3 RID: 65011 RVA: 0x00390244 File Offset: 0x0038E444
		public override RadListViewInsertionObject GetInsertionObject(IDictionary values)
		{
			if (this._transformedEnumerable != null && this._properties == null)
			{
				this._properties = new ItemPropertiesDescriptor(this._transformedEnumerable).Process();
			}
			RadListViewInsertionObject radListViewInsertionObject = new RadListViewInsertionObject(this._properties);
			if (values != null)
			{
				radListViewInsertionObject.SetupValues(values);
			}
			return radListViewInsertionObject;
		}

		// Token: 0x0400480E RID: 18446
		private readonly IEnumerable _originalEnumerable;

		// Token: 0x0400480F RID: 18447
		private IEnumerable _transformedEnumerable;

		// Token: 0x04004810 RID: 18448
		private int? _dataSourceCount;

		// Token: 0x04004811 RID: 18449
		private bool _isTransformed;

		// Token: 0x04004812 RID: 18450
		private int? _count;

		// Token: 0x04004813 RID: 18451
		private RadListViewSortExpressionCollection _sortExpressions;

		// Token: 0x04004814 RID: 18452
		private ListViewEnumerableHelper _enumerableHelper;

		// Token: 0x04004815 RID: 18453
		private RadListViewFilterExpressionCollection _filterExpressions;

		// Token: 0x04004816 RID: 18454
		private PropertyDescriptorCollection _properties;

		// Token: 0x04004817 RID: 18455
		private RadListView ownerListView;
	}
}

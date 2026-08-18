using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020000C4 RID: 196
	public abstract class ProxyBoundControlEnumerableBase
	{
		// Token: 0x06000771 RID: 1905
		public abstract IEnumerable RawEnumerable();

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001C94E File Offset: 0x0001AB4E
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001C956 File Offset: 0x0001AB56
		internal bool IsBoundUsingDataSourceID { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001C95F File Offset: 0x0001AB5F
		public static ProxyBoundControlEnumerableBase Null
		{
			get
			{
				return ProxyBoundControlEnumerableBase._null;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001C966 File Offset: 0x0001AB66
		public virtual bool SupportsPaging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001C969 File Offset: 0x0001AB69
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0001C971 File Offset: 0x0001AB71
		public bool AllowCustomSorting { get; internal set; }

		// Token: 0x06000778 RID: 1912
		protected abstract void TransformEnumerable();

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000779 RID: 1913
		public abstract int DataSourceCount { get; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001C97A File Offset: 0x0001AB7A
		public virtual int Count
		{
			get
			{
				return this.DataSourceCount;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001C982 File Offset: 0x0001AB82
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001C98A File Offset: 0x0001AB8A
		internal bool ShouldApplyFiltering { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001C993 File Offset: 0x0001AB93
		public virtual RadProxyBoundControlPagingManager PagingManager
		{
			get
			{
				if (this._pagingManager == null)
				{
					this._pagingManager = new RadProxyBoundControlPagingManager(this);
				}
				return this._pagingManager;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001C9AF File Offset: 0x0001ABAF
		public virtual bool SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001C9B2 File Offset: 0x0001ABB2
		public virtual bool SupportsFiltering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001C9B5 File Offset: 0x0001ABB5
		public virtual void SetSortExpressions(RadListViewSortExpressionCollection sortExpressions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		public virtual void SetFilteringExpressions(RadListViewFilterExpressionCollection sortExpressions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040001B7 RID: 439
		private static ProxyBoundControlEnumerableBase _null = new ProxyBoundControlNullEnumerable();

		// Token: 0x040001B8 RID: 440
		private RadProxyBoundControlPagingManager _pagingManager;
	}
}

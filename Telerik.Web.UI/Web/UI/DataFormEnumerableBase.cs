using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020001E6 RID: 486
	public abstract class DataFormEnumerableBase
	{
		// Token: 0x0600112B RID: 4395
		public abstract IEnumerable RawEnumerable();

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0003ED21 File Offset: 0x0003CF21
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x0003ED29 File Offset: 0x0003CF29
		internal bool IsBoundUsingDataSourceID { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0003ED32 File Offset: 0x0003CF32
		public static DataFormEnumerableBase Null
		{
			get
			{
				return DataFormEnumerableBase._null;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0003ED39 File Offset: 0x0003CF39
		public virtual bool SupportsPaging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001130 RID: 4400
		protected abstract void TransformEnumerable();

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001131 RID: 4401
		public abstract int DataSourceCount { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0003ED3C File Offset: 0x0003CF3C
		public virtual int Count
		{
			get
			{
				return this.DataSourceCount;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0003ED44 File Offset: 0x0003CF44
		public virtual RadDataFormPagingManager PagingManager
		{
			get
			{
				if (this._pagingManager == null)
				{
					this._pagingManager = new RadDataFormPagingManager(this);
				}
				return this._pagingManager;
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0003ED60 File Offset: 0x0003CF60
		public virtual RadDataFormInsertionObject GetInsertionObject(IDictionary values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040004EB RID: 1259
		private static DataFormEnumerableBase _null = new DataFormNullEnumerable();

		// Token: 0x040004EC RID: 1260
		private RadDataFormPagingManager _pagingManager;
	}
}

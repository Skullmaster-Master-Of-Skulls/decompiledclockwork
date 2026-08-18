using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020001E9 RID: 489
	public class DataFormPagableEnumerable : DataFormEnumerableBase
	{
		// Token: 0x0600114A RID: 4426 RVA: 0x0003EE9C File Offset: 0x0003D09C
		public DataFormPagableEnumerable(RadDataForm ownerDataForm, IEnumerable rawEnumerable)
		{
			this.ownerDataForm = ownerDataForm;
			this._originalEnumerable = rawEnumerable;
			this._transformedEnumerable = null;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0003EEB9 File Offset: 0x0003D0B9
		private DataFormEnumerableHelper EnumerableHelper
		{
			get
			{
				if (this._enumerableHelper == null)
				{
					this._enumerableHelper = DataFormEnumerableHelper.Instantiate(this._originalEnumerable);
					this._enumerableHelper.IsBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
				}
				return this._enumerableHelper;
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003EEEB File Offset: 0x0003D0EB
		public override IEnumerable RawEnumerable()
		{
			this.TransformEnumerable();
			return this._transformedEnumerable;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003EEF9 File Offset: 0x0003D0F9
		protected override void TransformEnumerable()
		{
			if (this._isTransformed)
			{
				return;
			}
			this.PerformTransformation();
			this._isTransformed = true;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003EF14 File Offset: 0x0003D114
		private void PerformTransformation()
		{
			this._transformedEnumerable = this._originalEnumerable;
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

		// Token: 0x0600114F RID: 4431 RVA: 0x0003EF8C File Offset: 0x0003D18C
		private void EnsureDataSourceCount()
		{
			int dataSourceCount = this.DataSourceCount;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0003EF95 File Offset: 0x0003D195
		public override int DataSourceCount
		{
			get
			{
				if (this._dataSourceCount == null)
				{
					this._dataSourceCount = new int?(this.EnumerableHelper.GetCount(this._originalEnumerable));
				}
				return this._dataSourceCount.Value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0003EFCC File Offset: 0x0003D1CC
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

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0003F028 File Offset: 0x0003D228
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003F02C File Offset: 0x0003D22C
		public override RadDataFormInsertionObject GetInsertionObject(IDictionary values)
		{
			if (this._transformedEnumerable != null && this._properties == null)
			{
				this._properties = new ItemPropertiesDescriptor(this._transformedEnumerable).Process();
			}
			RadDataFormInsertionObject radDataFormInsertionObject = new RadDataFormInsertionObject(this._properties);
			if (values != null)
			{
				radDataFormInsertionObject.SetupValues(values);
			}
			return radDataFormInsertionObject;
		}

		// Token: 0x040004F4 RID: 1268
		private readonly IEnumerable _originalEnumerable;

		// Token: 0x040004F5 RID: 1269
		private IEnumerable _transformedEnumerable;

		// Token: 0x040004F6 RID: 1270
		private int? _dataSourceCount;

		// Token: 0x040004F7 RID: 1271
		private int? _count;

		// Token: 0x040004F8 RID: 1272
		private DataFormEnumerableHelper _enumerableHelper;

		// Token: 0x040004F9 RID: 1273
		private PropertyDescriptorCollection _properties;

		// Token: 0x040004FA RID: 1274
		private RadDataForm ownerDataForm;

		// Token: 0x040004FB RID: 1275
		private bool _isTransformed;
	}
}

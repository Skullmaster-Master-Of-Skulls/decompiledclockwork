using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000772 RID: 1906
	internal class DbDataRecordPropertyValues : InternalPropertyValues
	{
		// Token: 0x06005662 RID: 22114 RVA: 0x00176404 File Offset: 0x00174604
		internal DbDataRecordPropertyValues(InternalContext internalContext, Type type, DbUpdatableDataRecord dataRecord, bool isEntity) : base(internalContext, type, isEntity)
		{
			this._dataRecord = dataRecord;
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00176418 File Offset: 0x00174618
		protected override IPropertyValuesItem GetItemImpl(string propertyName)
		{
			int ordinal = this._dataRecord.GetOrdinal(propertyName);
			object obj = this._dataRecord[ordinal];
			DbUpdatableDataRecord dbUpdatableDataRecord = obj as DbUpdatableDataRecord;
			if (dbUpdatableDataRecord != null)
			{
				obj = new DbDataRecordPropertyValues(base.InternalContext, this._dataRecord.GetFieldType(ordinal), dbUpdatableDataRecord, false);
			}
			else if (obj == DBNull.Value)
			{
				obj = null;
			}
			return new DbDataRecordPropertyValuesItem(this._dataRecord, ordinal, obj);
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005664 RID: 22116 RVA: 0x0017647C File Offset: 0x0017467C
		public override ISet<string> PropertyNames
		{
			get
			{
				if (this._names == null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					for (int i = 0; i < this._dataRecord.FieldCount; i++)
					{
						hashSet.Add(this._dataRecord.GetName(i));
					}
					this._names = new ReadOnlySet<string>(hashSet);
				}
				return this._names;
			}
		}

		// Token: 0x040022FB RID: 8955
		private readonly DbUpdatableDataRecord _dataRecord;

		// Token: 0x040022FC RID: 8956
		private ISet<string> _names;
	}
}

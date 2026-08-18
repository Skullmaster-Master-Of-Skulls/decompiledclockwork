using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000773 RID: 1907
	internal class DbDataRecordPropertyValuesItem : IPropertyValuesItem
	{
		// Token: 0x06005665 RID: 22117 RVA: 0x001764D2 File Offset: 0x001746D2
		public DbDataRecordPropertyValuesItem(DbUpdatableDataRecord dataRecord, int ordinal, object value)
		{
			this._dataRecord = dataRecord;
			this._ordinal = ordinal;
			this._value = value;
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06005666 RID: 22118 RVA: 0x001764EF File Offset: 0x001746EF
		// (set) Token: 0x06005667 RID: 22119 RVA: 0x001764F7 File Offset: 0x001746F7
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._dataRecord.SetValue(this._ordinal, value);
				this._value = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06005668 RID: 22120 RVA: 0x00176512 File Offset: 0x00174712
		public string Name
		{
			get
			{
				return this._dataRecord.GetName(this._ordinal);
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06005669 RID: 22121 RVA: 0x00176528 File Offset: 0x00174728
		public bool IsComplex
		{
			get
			{
				return this._dataRecord.DataRecordInfo.FieldMetadata[this._ordinal].FieldType.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600566A RID: 22122 RVA: 0x0017656A File Offset: 0x0017476A
		public Type Type
		{
			get
			{
				return this._dataRecord.GetFieldType(this._ordinal);
			}
		}

		// Token: 0x040022FD RID: 8957
		private readonly DbUpdatableDataRecord _dataRecord;

		// Token: 0x040022FE RID: 8958
		private readonly int _ordinal;

		// Token: 0x040022FF RID: 8959
		private object _value;
	}
}

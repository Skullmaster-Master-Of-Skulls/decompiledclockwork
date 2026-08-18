using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000263 RID: 611
	internal abstract class DataReaderContainer
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x002826F8 File Offset: 0x00281AF8
		internal static DataReaderContainer Create(IDataReader dataReader, bool returnProviderSpecificTypes)
		{
			if (returnProviderSpecificTypes)
			{
				DbDataReader dbDataReader = dataReader as DbDataReader;
				if (dbDataReader != null)
				{
					return new DataReaderContainer.ProviderSpecificDataReader(dataReader, dbDataReader);
				}
			}
			return new DataReaderContainer.CommonLanguageSubsetDataReader(dataReader);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00282728 File Offset: 0x00281B28
		protected DataReaderContainer(IDataReader dataReader)
		{
			this._dataReader = dataReader;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00282748 File Offset: 0x00281B48
		internal int FieldCount
		{
			get
			{
				return this._fieldCount;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060020D2 RID: 8402
		internal abstract bool ReturnProviderSpecificTypes { get; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060020D3 RID: 8403
		protected abstract int VisibleFieldCount { get; }

		// Token: 0x060020D4 RID: 8404
		internal abstract Type GetFieldType(int ordinal);

		// Token: 0x060020D5 RID: 8405
		internal abstract object GetValue(int ordinal);

		// Token: 0x060020D6 RID: 8406
		internal abstract int GetValues(object[] values);

		// Token: 0x060020D7 RID: 8407 RVA: 0x00282768 File Offset: 0x00281B68
		internal string GetName(int ordinal)
		{
			string name = this._dataReader.GetName(ordinal);
			if (name == null)
			{
				return "";
			}
			return name;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00282798 File Offset: 0x00281B98
		internal DataTable GetSchemaTable()
		{
			return this._dataReader.GetSchemaTable();
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x002827B8 File Offset: 0x00281BB8
		internal bool NextResult()
		{
			this._fieldCount = 0;
			if (this._dataReader.NextResult())
			{
				this._fieldCount = this.VisibleFieldCount;
				return true;
			}
			return false;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x002827E8 File Offset: 0x00281BE8
		internal bool Read()
		{
			return this._dataReader.Read();
		}

		// Token: 0x04001552 RID: 5458
		protected readonly IDataReader _dataReader;

		// Token: 0x04001553 RID: 5459
		protected int _fieldCount;

		// Token: 0x02000264 RID: 612
		private sealed class ProviderSpecificDataReader : DataReaderContainer
		{
			// Token: 0x060020DB RID: 8411 RVA: 0x00282808 File Offset: 0x00281C08
			internal ProviderSpecificDataReader(IDataReader dataReader, DbDataReader dbDataReader) : base(dataReader)
			{
				this._providerSpecificDataReader = dbDataReader;
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x060020DC RID: 8412 RVA: 0x00282838 File Offset: 0x00281C38
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x060020DD RID: 8413 RVA: 0x00282848 File Offset: 0x00281C48
			protected override int VisibleFieldCount
			{
				get
				{
					int visibleFieldCount = this._providerSpecificDataReader.VisibleFieldCount;
					if (0 > visibleFieldCount)
					{
						return 0;
					}
					return visibleFieldCount;
				}
			}

			// Token: 0x060020DE RID: 8414 RVA: 0x00282868 File Offset: 0x00281C68
			internal override Type GetFieldType(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificFieldType(ordinal);
			}

			// Token: 0x060020DF RID: 8415 RVA: 0x00282888 File Offset: 0x00281C88
			internal override object GetValue(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValue(ordinal);
			}

			// Token: 0x060020E0 RID: 8416 RVA: 0x002828A8 File Offset: 0x00281CA8
			internal override int GetValues(object[] values)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValues(values);
			}

			// Token: 0x04001554 RID: 5460
			private DbDataReader _providerSpecificDataReader;
		}

		// Token: 0x02000265 RID: 613
		private sealed class CommonLanguageSubsetDataReader : DataReaderContainer
		{
			// Token: 0x060020E1 RID: 8417 RVA: 0x002828C8 File Offset: 0x00281CC8
			internal CommonLanguageSubsetDataReader(IDataReader dataReader) : base(dataReader)
			{
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x060020E2 RID: 8418 RVA: 0x002828E8 File Offset: 0x00281CE8
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x060020E3 RID: 8419 RVA: 0x002828F8 File Offset: 0x00281CF8
			protected override int VisibleFieldCount
			{
				get
				{
					int fieldCount = this._dataReader.FieldCount;
					if (0 > fieldCount)
					{
						return 0;
					}
					return fieldCount;
				}
			}

			// Token: 0x060020E4 RID: 8420 RVA: 0x00282918 File Offset: 0x00281D18
			internal override Type GetFieldType(int ordinal)
			{
				return this._dataReader.GetFieldType(ordinal);
			}

			// Token: 0x060020E5 RID: 8421 RVA: 0x00282938 File Offset: 0x00281D38
			internal override object GetValue(int ordinal)
			{
				return this._dataReader.GetValue(ordinal);
			}

			// Token: 0x060020E6 RID: 8422 RVA: 0x00282958 File Offset: 0x00281D58
			internal override int GetValues(object[] values)
			{
				return this._dataReader.GetValues(values);
			}
		}
	}
}

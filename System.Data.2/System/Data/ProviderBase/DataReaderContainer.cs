using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x020002B6 RID: 694
	internal abstract class DataReaderContainer
	{
		// Token: 0x060029F9 RID: 10745 RVA: 0x00115E74 File Offset: 0x00115274
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

		// Token: 0x060029FA RID: 10746 RVA: 0x00115E9C File Offset: 0x0011529C
		protected DataReaderContainer(IDataReader dataReader)
		{
			this._dataReader = dataReader;
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x00115EB8 File Offset: 0x001152B8
		internal int FieldCount
		{
			get
			{
				return this._fieldCount;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060029FC RID: 10748
		internal abstract bool ReturnProviderSpecificTypes { get; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060029FD RID: 10749
		protected abstract int VisibleFieldCount { get; }

		// Token: 0x060029FE RID: 10750
		internal abstract Type GetFieldType(int ordinal);

		// Token: 0x060029FF RID: 10751
		internal abstract object GetValue(int ordinal);

		// Token: 0x06002A00 RID: 10752
		internal abstract int GetValues(object[] values);

		// Token: 0x06002A01 RID: 10753 RVA: 0x00115ECC File Offset: 0x001152CC
		internal string GetName(int ordinal)
		{
			string name = this._dataReader.GetName(ordinal);
			if (name == null)
			{
				return "";
			}
			return name;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00115EF0 File Offset: 0x001152F0
		internal DataTable GetSchemaTable()
		{
			return this._dataReader.GetSchemaTable();
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00115F08 File Offset: 0x00115308
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

		// Token: 0x06002A04 RID: 10756 RVA: 0x00115F38 File Offset: 0x00115338
		internal bool Read()
		{
			return this._dataReader.Read();
		}

		// Token: 0x04001B11 RID: 6929
		protected readonly IDataReader _dataReader;

		// Token: 0x04001B12 RID: 6930
		protected int _fieldCount;

		// Token: 0x02000425 RID: 1061
		private sealed class ProviderSpecificDataReader : DataReaderContainer
		{
			// Token: 0x060035F8 RID: 13816 RVA: 0x00148208 File Offset: 0x00147608
			internal ProviderSpecificDataReader(IDataReader dataReader, DbDataReader dbDataReader) : base(dataReader)
			{
				this._providerSpecificDataReader = dbDataReader;
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x1700086A RID: 2154
			// (get) Token: 0x060035F9 RID: 13817 RVA: 0x00148230 File Offset: 0x00147630
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700086B RID: 2155
			// (get) Token: 0x060035FA RID: 13818 RVA: 0x00148240 File Offset: 0x00147640
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

			// Token: 0x060035FB RID: 13819 RVA: 0x00148260 File Offset: 0x00147660
			internal override Type GetFieldType(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificFieldType(ordinal);
			}

			// Token: 0x060035FC RID: 13820 RVA: 0x0014827C File Offset: 0x0014767C
			internal override object GetValue(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValue(ordinal);
			}

			// Token: 0x060035FD RID: 13821 RVA: 0x00148298 File Offset: 0x00147698
			internal override int GetValues(object[] values)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValues(values);
			}

			// Token: 0x040022DA RID: 8922
			private DbDataReader _providerSpecificDataReader;
		}

		// Token: 0x02000426 RID: 1062
		private sealed class CommonLanguageSubsetDataReader : DataReaderContainer
		{
			// Token: 0x060035FE RID: 13822 RVA: 0x001482B4 File Offset: 0x001476B4
			internal CommonLanguageSubsetDataReader(IDataReader dataReader) : base(dataReader)
			{
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x1700086C RID: 2156
			// (get) Token: 0x060035FF RID: 13823 RVA: 0x001482D4 File Offset: 0x001476D4
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700086D RID: 2157
			// (get) Token: 0x06003600 RID: 13824 RVA: 0x001482E4 File Offset: 0x001476E4
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

			// Token: 0x06003601 RID: 13825 RVA: 0x00148304 File Offset: 0x00147704
			internal override Type GetFieldType(int ordinal)
			{
				return this._dataReader.GetFieldType(ordinal);
			}

			// Token: 0x06003602 RID: 13826 RVA: 0x00148320 File Offset: 0x00147720
			internal override object GetValue(int ordinal)
			{
				return this._dataReader.GetValue(ordinal);
			}

			// Token: 0x06003603 RID: 13827 RVA: 0x0014833C File Offset: 0x0014773C
			internal override int GetValues(object[] values)
			{
				return this._dataReader.GetValues(values);
			}
		}
	}
}

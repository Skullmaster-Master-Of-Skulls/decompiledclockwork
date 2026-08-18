using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x020000CF RID: 207
	internal abstract class BufferedDataRecord
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0001EAF4 File Offset: 0x0001CCF4
		protected virtual void ReadMetadata(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			Type[] array2 = new Type[fieldCount];
			string[] columnNames = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetDataTypeName(i);
				array2[i] = reader.GetFieldType(i);
				columnNames[i] = reader.GetName(i);
			}
			this._dataTypeNames = array;
			this._fieldTypes = array2;
			this._columnNames = columnNames;
			this._fieldNameLookup = new Lazy<FieldNameLookup>(() => new FieldNameLookup(new ReadOnlyCollection<string>(columnNames)), false);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001EB8E File Offset: 0x0001CD8E
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0001EB96 File Offset: 0x0001CD96
		public bool IsDataReady { get; protected set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0001EB9F File Offset: 0x0001CD9F
		public bool HasRows
		{
			get
			{
				return this._rowCount > 0;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001EBAA File Offset: 0x0001CDAA
		public int FieldCount
		{
			get
			{
				return this._dataTypeNames.Length;
			}
		}

		// Token: 0x060004AB RID: 1195
		public abstract bool GetBoolean(int ordinal);

		// Token: 0x060004AC RID: 1196
		public abstract byte GetByte(int ordinal);

		// Token: 0x060004AD RID: 1197
		public abstract char GetChar(int ordinal);

		// Token: 0x060004AE RID: 1198
		public abstract DateTime GetDateTime(int ordinal);

		// Token: 0x060004AF RID: 1199
		public abstract decimal GetDecimal(int ordinal);

		// Token: 0x060004B0 RID: 1200
		public abstract double GetDouble(int ordinal);

		// Token: 0x060004B1 RID: 1201
		public abstract float GetFloat(int ordinal);

		// Token: 0x060004B2 RID: 1202
		public abstract Guid GetGuid(int ordinal);

		// Token: 0x060004B3 RID: 1203
		public abstract short GetInt16(int ordinal);

		// Token: 0x060004B4 RID: 1204
		public abstract int GetInt32(int ordinal);

		// Token: 0x060004B5 RID: 1205
		public abstract long GetInt64(int ordinal);

		// Token: 0x060004B6 RID: 1206
		public abstract string GetString(int ordinal);

		// Token: 0x060004B7 RID: 1207
		public abstract T GetFieldValue<T>(int ordinal);

		// Token: 0x060004B8 RID: 1208
		public abstract Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken);

		// Token: 0x060004B9 RID: 1209
		public abstract object GetValue(int ordinal);

		// Token: 0x060004BA RID: 1210
		public abstract int GetValues(object[] values);

		// Token: 0x060004BB RID: 1211
		public abstract bool IsDBNull(int ordinal);

		// Token: 0x060004BC RID: 1212
		public abstract Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken);

		// Token: 0x060004BD RID: 1213 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		public string GetDataTypeName(int ordinal)
		{
			return this._dataTypeNames[ordinal];
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001EBBE File Offset: 0x0001CDBE
		public Type GetFieldType(int ordinal)
		{
			return this._fieldTypes[ordinal];
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001EBC8 File Offset: 0x0001CDC8
		public string GetName(int ordinal)
		{
			return this._columnNames[ordinal];
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001EBD2 File Offset: 0x0001CDD2
		public int GetOrdinal(string name)
		{
			return this._fieldNameLookup.Value.GetOrdinal(name);
		}

		// Token: 0x060004C1 RID: 1217
		public abstract bool Read();

		// Token: 0x060004C2 RID: 1218
		public abstract Task<bool> ReadAsync(CancellationToken cancellationToken);

		// Token: 0x0400016E RID: 366
		protected int _currentRowNumber = -1;

		// Token: 0x0400016F RID: 367
		protected int _rowCount;

		// Token: 0x04000170 RID: 368
		private string[] _dataTypeNames;

		// Token: 0x04000171 RID: 369
		private Type[] _fieldTypes;

		// Token: 0x04000172 RID: 370
		private string[] _columnNames;

		// Token: 0x04000173 RID: 371
		private Lazy<FieldNameLookup> _fieldNameLookup;
	}
}

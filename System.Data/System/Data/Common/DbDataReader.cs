using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x020000A2 RID: 162
	public abstract class DbDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000AA4 RID: 2724
		public abstract int Depth { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000AA5 RID: 2725
		public abstract int FieldCount { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000AA6 RID: 2726
		public abstract bool HasRows { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000AA7 RID: 2727
		public abstract bool IsClosed { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000AA8 RID: 2728
		public abstract int RecordsAffected { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00209A88 File Offset: 0x00208E88
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		// Token: 0x17000163 RID: 355
		public abstract object this[int ordinal]
		{
			get;
		}

		// Token: 0x17000164 RID: 356
		public abstract object this[string name]
		{
			get;
		}

		// Token: 0x06000AAC RID: 2732
		public abstract void Close();

		// Token: 0x06000AAD RID: 2733 RVA: 0x00209AA8 File Offset: 0x00208EA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00209AC8 File Offset: 0x00208EC8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000AAF RID: 2735
		public abstract string GetDataTypeName(int ordinal);

		// Token: 0x06000AB0 RID: 2736
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		// Token: 0x06000AB1 RID: 2737
		public abstract Type GetFieldType(int ordinal);

		// Token: 0x06000AB2 RID: 2738
		public abstract string GetName(int ordinal);

		// Token: 0x06000AB3 RID: 2739
		public abstract int GetOrdinal(string name);

		// Token: 0x06000AB4 RID: 2740
		public abstract DataTable GetSchemaTable();

		// Token: 0x06000AB5 RID: 2741
		public abstract bool GetBoolean(int ordinal);

		// Token: 0x06000AB6 RID: 2742
		public abstract byte GetByte(int ordinal);

		// Token: 0x06000AB7 RID: 2743
		public abstract long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000AB8 RID: 2744
		public abstract char GetChar(int ordinal);

		// Token: 0x06000AB9 RID: 2745
		public abstract long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000ABA RID: 2746 RVA: 0x00209AE8 File Offset: 0x00208EE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DbDataReader GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00209B08 File Offset: 0x00208F08
		IDataReader IDataRecord.GetData(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00209B28 File Offset: 0x00208F28
		protected virtual DbDataReader GetDbDataReader(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06000ABD RID: 2749
		public abstract DateTime GetDateTime(int ordinal);

		// Token: 0x06000ABE RID: 2750
		public abstract decimal GetDecimal(int ordinal);

		// Token: 0x06000ABF RID: 2751
		public abstract double GetDouble(int ordinal);

		// Token: 0x06000AC0 RID: 2752
		public abstract float GetFloat(int ordinal);

		// Token: 0x06000AC1 RID: 2753
		public abstract Guid GetGuid(int ordinal);

		// Token: 0x06000AC2 RID: 2754
		public abstract short GetInt16(int ordinal);

		// Token: 0x06000AC3 RID: 2755
		public abstract int GetInt32(int ordinal);

		// Token: 0x06000AC4 RID: 2756
		public abstract long GetInt64(int ordinal);

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00209B48 File Offset: 0x00208F48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Type GetProviderSpecificFieldType(int ordinal)
		{
			return this.GetFieldType(ordinal);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00209B68 File Offset: 0x00208F68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object GetProviderSpecificValue(int ordinal)
		{
			return this.GetValue(ordinal);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00209B88 File Offset: 0x00208F88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		// Token: 0x06000AC8 RID: 2760
		public abstract string GetString(int ordinal);

		// Token: 0x06000AC9 RID: 2761
		public abstract object GetValue(int ordinal);

		// Token: 0x06000ACA RID: 2762
		public abstract int GetValues(object[] values);

		// Token: 0x06000ACB RID: 2763
		public abstract bool IsDBNull(int ordinal);

		// Token: 0x06000ACC RID: 2764
		public abstract bool NextResult();

		// Token: 0x06000ACD RID: 2765
		public abstract bool Read();
	}
}

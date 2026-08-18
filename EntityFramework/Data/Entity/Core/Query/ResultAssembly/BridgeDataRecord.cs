using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Query.ResultAssembly
{
	// Token: 0x020006AE RID: 1710
	internal sealed class BridgeDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x060043D7 RID: 17367 RVA: 0x0014276B File Offset: 0x0014096B
		internal BridgeDataRecord(Shaper<RecordState> shaper, int depth)
		{
			this._shaper = shaper;
			this.Depth = depth;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x00142781 File Offset: 0x00140981
		internal void CloseExplicitly()
		{
			this.Close<object>(BridgeDataRecord.Status.ClosedExplicitly, new Func<object>(this.CloseNestedObjectImplicitly));
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x001427B4 File Offset: 0x001409B4
		internal Task CloseExplicitlyAsync(CancellationToken cancellationToken)
		{
			return this.Close<Task>(BridgeDataRecord.Status.ClosedExplicitly, () => this.CloseNestedObjectImplicitlyAsync(cancellationToken));
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x001427E8 File Offset: 0x001409E8
		internal void CloseImplicitly()
		{
			this.Close<object>(BridgeDataRecord.Status.ClosedImplicitly, new Func<object>(this.CloseNestedObjectImplicitly));
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x0014281C File Offset: 0x00140A1C
		internal Task CloseImplicitlyAsync(CancellationToken cancellationToken)
		{
			return this.Close<Task>(BridgeDataRecord.Status.ClosedImplicitly, () => this.CloseNestedObjectImplicitlyAsync(cancellationToken));
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x00142850 File Offset: 0x00140A50
		private T Close<T>(BridgeDataRecord.Status status, Func<T> close)
		{
			this._status = status;
			this._source = null;
			return close();
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x00142868 File Offset: 0x00140A68
		private object CloseNestedObjectImplicitly()
		{
			BridgeDataRecord currentNestedRecord = this._currentNestedRecord;
			if (currentNestedRecord != null)
			{
				this._currentNestedRecord = null;
				currentNestedRecord.CloseImplicitly();
			}
			BridgeDataReader currentNestedReader = this._currentNestedReader;
			if (currentNestedReader != null)
			{
				this._currentNestedReader = null;
				currentNestedReader.CloseImplicitly();
			}
			return null;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x00142A64 File Offset: 0x00140C64
		private async Task CloseNestedObjectImplicitlyAsync(CancellationToken cancellationToken)
		{
			BridgeDataRecord currentNestedRecord = this._currentNestedRecord;
			if (currentNestedRecord != null)
			{
				this._currentNestedRecord = null;
				await currentNestedRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
			}
			BridgeDataReader currentNestedReader = this._currentNestedReader;
			if (currentNestedReader != null)
			{
				this._currentNestedReader = null;
				await currentNestedReader.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
			}
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x00142AB2 File Offset: 0x00140CB2
		internal void SetRecordSource(RecordState newSource, bool hasData)
		{
			if (hasData)
			{
				this._source = newSource;
			}
			else
			{
				this._source = null;
			}
			this._status = BridgeDataRecord.Status.Open;
			this._lastColumnRead = -1;
			this._lastDataOffsetRead = -1L;
			this._lastOrdinalCheckedForNull = -1;
			this._lastValueCheckedForNull = null;
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x00142AEB File Offset: 0x00140CEB
		private void AssertReaderIsOpen()
		{
			if (this.IsExplicitlyClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
			if (this.IsImplicitlyClosed)
			{
				throw Error.ADP_ImplicitlyClosedDataReaderError();
			}
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x00142B09 File Offset: 0x00140D09
		private void AssertReaderIsOpenWithData()
		{
			this.AssertReaderIsOpen();
			if (!this.HasData)
			{
				throw Error.ADP_NoData();
			}
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x00142B20 File Offset: 0x00140D20
		private void AssertSequentialAccess(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			if (this._lastColumnRead >= ordinal)
			{
				throw new InvalidOperationException(Strings.ADP_NonSequentialColumnAccess(ordinal.ToString(CultureInfo.InvariantCulture), (this._lastColumnRead + 1).ToString(CultureInfo.InvariantCulture)));
			}
			this._lastColumnRead = ordinal;
			this._lastDataOffsetRead = long.MaxValue;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x00142B98 File Offset: 0x00140D98
		private void AssertSequentialAccess(int ordinal, long dataOffset, string methodName)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			if (this._lastColumnRead > ordinal || (this._lastColumnRead == ordinal && this._lastDataOffsetRead == 9223372036854775807L))
			{
				throw new InvalidOperationException(Strings.ADP_NonSequentialColumnAccess(ordinal.ToString(CultureInfo.InvariantCulture), (this._lastColumnRead + 1).ToString(CultureInfo.InvariantCulture)));
			}
			if (this._lastColumnRead == ordinal)
			{
				if (this._lastDataOffsetRead >= dataOffset)
				{
					throw new InvalidOperationException(Strings.ADP_NonSequentialChunkAccess(dataOffset.ToString(CultureInfo.InvariantCulture), (this._lastDataOffsetRead + 1L).ToString(CultureInfo.InvariantCulture), methodName));
				}
			}
			else
			{
				this._lastColumnRead = ordinal;
				this._lastDataOffsetRead = -1L;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x00142C60 File Offset: 0x00140E60
		internal bool HasData
		{
			get
			{
				return this._source != null;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x00142C7B File Offset: 0x00140E7B
		internal bool IsClosed
		{
			get
			{
				return this._status != BridgeDataRecord.Status.Open;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x00142C89 File Offset: 0x00140E89
		internal bool IsExplicitlyClosed
		{
			get
			{
				return this._status == BridgeDataRecord.Status.ClosedExplicitly;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x00142C94 File Offset: 0x00140E94
		internal bool IsImplicitlyClosed
		{
			get
			{
				return this._status == BridgeDataRecord.Status.ClosedImplicitly;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x00142CA0 File Offset: 0x00140EA0
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._source.DataRecordInfo;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x00142CC0 File Offset: 0x00140EC0
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._source.ColumnCount;
			}
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x00142CD4 File Offset: 0x00140ED4
		private TypeUsage GetTypeUsage(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			RecordState recordState = this._source.CurrentColumnValues[ordinal] as RecordState;
			TypeUsage result;
			if (recordState != null)
			{
				result = recordState.DataRecordInfo.RecordType;
			}
			else
			{
				result = this._source.GetTypeUsage(ordinal);
			}
			return result;
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x00142D30 File Offset: 0x00140F30
		public override string GetDataTypeName(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			return this.GetTypeUsage(ordinal).ToString();
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x00142D44 File Offset: 0x00140F44
		public override Type GetFieldType(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			return BridgeDataReader.GetClrTypeFromTypeMetadata(this.GetTypeUsage(ordinal));
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x00142D58 File Offset: 0x00140F58
		public override string GetName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._source.GetName(ordinal);
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x00142D6C File Offset: 0x00140F6C
		public override int GetOrdinal(string name)
		{
			this.AssertReaderIsOpen();
			return this._source.GetOrdinal(name);
		}

		// Token: 0x17000A48 RID: 2632
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000A49 RID: 2633
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00142D98 File Offset: 0x00140F98
		public override object GetValue(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			this.AssertSequentialAccess(ordinal);
			object result;
			if (ordinal == this._lastOrdinalCheckedForNull)
			{
				result = this._lastValueCheckedForNull;
			}
			else
			{
				this._lastOrdinalCheckedForNull = -1;
				this._lastValueCheckedForNull = null;
				this.CloseNestedObjectImplicitly();
				result = this._source.CurrentColumnValues[ordinal];
				if (this._source.IsNestedObject(ordinal))
				{
					result = this.GetNestedObjectValue(result);
				}
			}
			return result;
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x00142E00 File Offset: 0x00141000
		private object GetNestedObjectValue(object result)
		{
			if (result != DBNull.Value)
			{
				RecordState recordState = result as RecordState;
				if (recordState != null)
				{
					if (recordState.IsNull)
					{
						result = DBNull.Value;
					}
					else
					{
						BridgeDataRecord bridgeDataRecord = new BridgeDataRecord(this._shaper, this.Depth + 1);
						bridgeDataRecord.SetRecordSource(recordState, true);
						result = bridgeDataRecord;
						this._currentNestedRecord = bridgeDataRecord;
						this._currentNestedReader = null;
					}
				}
				else
				{
					Coordinator<RecordState> coordinator = result as Coordinator<RecordState>;
					if (coordinator != null)
					{
						BridgeDataReader bridgeDataReader = new BridgeDataReader(this._shaper, coordinator.TypedCoordinatorFactory, this.Depth + 1, null);
						result = bridgeDataReader;
						this._currentNestedRecord = null;
						this._currentNestedReader = bridgeDataReader;
					}
				}
			}
			return result;
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x00142E9C File Offset: 0x0014109C
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x00142EDB File Offset: 0x001410DB
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x00142EE9 File Offset: 0x001410E9
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x00142EF7 File Offset: 0x001410F7
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x00142F05 File Offset: 0x00141105
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x00142F13 File Offset: 0x00141113
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x00142F21 File Offset: 0x00141121
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x00142F2F File Offset: 0x0014112F
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00142F3D File Offset: 0x0014113D
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x00142F4B File Offset: 0x0014114B
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x00142F59 File Offset: 0x00141159
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00142F67 File Offset: 0x00141167
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00142F75 File Offset: 0x00141175
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x00142F84 File Offset: 0x00141184
		public override bool IsDBNull(int ordinal)
		{
			object value = this.GetValue(ordinal);
			this._lastColumnRead--;
			this._lastDataOffsetRead = -1L;
			this._lastValueCheckedForNull = value;
			this._lastOrdinalCheckedForNull = ordinal;
			return DBNull.Value == value;
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x00142FC8 File Offset: 0x001411C8
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.AssertReaderIsOpenWithData();
			this.AssertSequentialAccess(ordinal, dataOffset, "GetBytes");
			long bytes = this._source.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
			if (buffer != null)
			{
				this._lastDataOffsetRead = dataOffset + bytes - 1L;
			}
			return bytes;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0014300C File Offset: 0x0014120C
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			this.AssertReaderIsOpenWithData();
			this.AssertSequentialAccess(ordinal, dataOffset, "GetChars");
			long chars = this._source.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
			if (buffer != null)
			{
				this._lastDataOffsetRead = dataOffset + chars - 1L;
			}
			return chars;
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x0014304F File Offset: 0x0014124F
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return (DbDataReader)this.GetValue(ordinal);
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x0014305D File Offset: 0x0014125D
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x0014306B File Offset: 0x0014126B
		public DbDataReader GetDataReader(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x0400191C RID: 6428
		internal readonly int Depth;

		// Token: 0x0400191D RID: 6429
		private readonly Shaper<RecordState> _shaper;

		// Token: 0x0400191E RID: 6430
		private RecordState _source;

		// Token: 0x0400191F RID: 6431
		private BridgeDataRecord.Status _status;

		// Token: 0x04001920 RID: 6432
		private int _lastColumnRead;

		// Token: 0x04001921 RID: 6433
		private long _lastDataOffsetRead;

		// Token: 0x04001922 RID: 6434
		private int _lastOrdinalCheckedForNull;

		// Token: 0x04001923 RID: 6435
		private object _lastValueCheckedForNull;

		// Token: 0x04001924 RID: 6436
		private BridgeDataReader _currentNestedReader;

		// Token: 0x04001925 RID: 6437
		private BridgeDataRecord _currentNestedRecord;

		// Token: 0x020006AF RID: 1711
		private enum Status
		{
			// Token: 0x04001927 RID: 6439
			Open,
			// Token: 0x04001928 RID: 6440
			ClosedImplicitly,
			// Token: 0x04001929 RID: 6441
			ClosedExplicitly
		}
	}
}

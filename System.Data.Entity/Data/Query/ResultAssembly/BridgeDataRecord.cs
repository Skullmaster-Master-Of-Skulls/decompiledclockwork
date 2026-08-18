using System;
using System.Data.Common;
using System.Data.Common.Internal.Materialization;
using System.Data.Metadata.Edm;

namespace System.Data.Query.ResultAssembly
{
	// Token: 0x02000043 RID: 67
	internal sealed class BridgeDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x000183B1 File Offset: 0x000165B1
		internal BridgeDataRecord(Shaper<RecordState> shaper, int depth)
		{
			this.Shaper = shaper;
			this.Depth = depth;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000183C7 File Offset: 0x000165C7
		internal void CloseExplicitly()
		{
			this._status = BridgeDataRecord.Status.ClosedExplicitly;
			this._source = null;
			this.CloseNestedObjectImplicitly();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000183DD File Offset: 0x000165DD
		internal void CloseImplicitly()
		{
			this._status = BridgeDataRecord.Status.ClosedImplicitly;
			this._source = null;
			this.CloseNestedObjectImplicitly();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000183F4 File Offset: 0x000165F4
		private void CloseNestedObjectImplicitly()
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
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001842F File Offset: 0x0001662F
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

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018468 File Offset: 0x00016668
		private void AssertReaderIsOpen()
		{
			if (this.IsExplicitlyClosed)
			{
				throw EntityUtil.ClosedDataReaderError();
			}
			if (this.IsImplicitlyClosed)
			{
				throw EntityUtil.ImplicitlyClosedDataReaderError();
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018486 File Offset: 0x00016686
		private void AssertReaderIsOpenWithData()
		{
			this.AssertReaderIsOpen();
			if (!this.HasData)
			{
				throw EntityUtil.NoData();
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001849C File Offset: 0x0001669C
		private void AssertSequentialAccess(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
			}
			if (this._lastColumnRead >= ordinal)
			{
				throw EntityUtil.NonSequentialColumnAccess(ordinal, this._lastColumnRead + 1);
			}
			this._lastColumnRead = ordinal;
			this._lastDataOffsetRead = long.MaxValue;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000184F4 File Offset: 0x000166F4
		private void AssertSequentialAccess(int ordinal, long dataOffset, string methodName)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
			}
			if (this._lastColumnRead > ordinal || (this._lastColumnRead == ordinal && this._lastDataOffsetRead == 9223372036854775807L))
			{
				throw EntityUtil.NonSequentialColumnAccess(ordinal, this._lastColumnRead + 1);
			}
			if (this._lastColumnRead == ordinal)
			{
				if (this._lastDataOffsetRead >= dataOffset)
				{
					throw EntityUtil.NonSequentialArrayOffsetAccess(dataOffset, this._lastDataOffsetRead + 1L, methodName);
				}
			}
			else
			{
				this._lastColumnRead = ordinal;
				this._lastDataOffsetRead = -1L;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00018584 File Offset: 0x00016784
		internal bool HasData
		{
			get
			{
				return this._source != null;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001859C File Offset: 0x0001679C
		internal bool IsClosed
		{
			get
			{
				return this._status > BridgeDataRecord.Status.Open;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000185A7 File Offset: 0x000167A7
		internal bool IsExplicitlyClosed
		{
			get
			{
				return this._status == BridgeDataRecord.Status.ClosedExplicitly;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000185B2 File Offset: 0x000167B2
		internal bool IsImplicitlyClosed
		{
			get
			{
				return this._status == BridgeDataRecord.Status.ClosedImplicitly;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000185C0 File Offset: 0x000167C0
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._source.DataRecordInfo;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000185E0 File Offset: 0x000167E0
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._source.ColumnCount;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000185F4 File Offset: 0x000167F4
		private TypeUsage GetTypeUsage(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
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

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018650 File Offset: 0x00016850
		public override string GetDataTypeName(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			return TypeHelpers.GetFullName(this.GetTypeUsage(ordinal));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018664 File Offset: 0x00016864
		public override Type GetFieldType(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			return BridgeDataReader.GetClrTypeFromTypeMetadata(this.GetTypeUsage(ordinal));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018678 File Offset: 0x00016878
		public override string GetName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._source.GetName(ordinal);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001868C File Offset: 0x0001688C
		public override int GetOrdinal(string name)
		{
			this.AssertReaderIsOpen();
			return this._source.GetOrdinal(name);
		}

		// Token: 0x1700007D RID: 125
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x1700007E RID: 126
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000186B8 File Offset: 0x000168B8
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

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018720 File Offset: 0x00016920
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
						BridgeDataRecord bridgeDataRecord = new BridgeDataRecord(this.Shaper, this.Depth + 1);
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
						BridgeDataReader bridgeDataReader = new BridgeDataReader(this.Shaper, coordinator.TypedCoordinatorFactory, this.Depth + 1, null);
						result = bridgeDataReader;
						this._currentNestedRecord = null;
						this._currentNestedReader = bridgeDataReader;
					}
				}
			}
			return result;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000187BC File Offset: 0x000169BC
		public override int GetValues(object[] values)
		{
			EntityUtil.CheckArgumentNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000187FB File Offset: 0x000169FB
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00018809 File Offset: 0x00016A09
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018817 File Offset: 0x00016A17
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018825 File Offset: 0x00016A25
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018833 File Offset: 0x00016A33
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018841 File Offset: 0x00016A41
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001884F File Offset: 0x00016A4F
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001885D File Offset: 0x00016A5D
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001886B File Offset: 0x00016A6B
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018879 File Offset: 0x00016A79
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00018887 File Offset: 0x00016A87
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018895 File Offset: 0x00016A95
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000188A4 File Offset: 0x00016AA4
		public override bool IsDBNull(int ordinal)
		{
			object value = this.GetValue(ordinal);
			this._lastColumnRead--;
			this._lastDataOffsetRead = -1L;
			this._lastValueCheckedForNull = value;
			this._lastOrdinalCheckedForNull = ordinal;
			return DBNull.Value == value;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000188E8 File Offset: 0x00016AE8
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

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001892C File Offset: 0x00016B2C
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

		// Token: 0x060005CA RID: 1482 RVA: 0x0001896F File Offset: 0x00016B6F
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return (DbDataReader)this.GetValue(ordinal);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001897D File Offset: 0x00016B7D
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001898B File Offset: 0x00016B8B
		public DbDataReader GetDataReader(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x0400074F RID: 1871
		internal readonly int Depth;

		// Token: 0x04000750 RID: 1872
		private readonly Shaper<RecordState> Shaper;

		// Token: 0x04000751 RID: 1873
		private RecordState _source;

		// Token: 0x04000752 RID: 1874
		private BridgeDataRecord.Status _status;

		// Token: 0x04000753 RID: 1875
		private int _lastColumnRead;

		// Token: 0x04000754 RID: 1876
		private long _lastDataOffsetRead;

		// Token: 0x04000755 RID: 1877
		private int _lastOrdinalCheckedForNull;

		// Token: 0x04000756 RID: 1878
		private object _lastValueCheckedForNull;

		// Token: 0x04000757 RID: 1879
		private BridgeDataReader _currentNestedReader;

		// Token: 0x04000758 RID: 1880
		private BridgeDataRecord _currentNestedRecord;

		// Token: 0x02000460 RID: 1120
		private enum Status
		{
			// Token: 0x0400194D RID: 6477
			Open,
			// Token: 0x0400194E RID: 6478
			ClosedImplicitly,
			// Token: 0x0400194F RID: 6479
			ClosedExplicitly
		}
	}
}

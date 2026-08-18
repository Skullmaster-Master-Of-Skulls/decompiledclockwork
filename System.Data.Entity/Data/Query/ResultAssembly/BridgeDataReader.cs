using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.Internal.Materialization;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Query.InternalTrees;
using System.Data.Query.PlanCompiler;

namespace System.Data.Query.ResultAssembly
{
	// Token: 0x02000042 RID: 66
	internal sealed class BridgeDataReader : DbDataReader, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x00017BCC File Offset: 0x00015DCC
		internal BridgeDataReader(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth, IEnumerator<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> nextResultShaperInfos)
		{
			this.NextResultShaperInfoEnumerator = ((nextResultShaperInfos != null) ? nextResultShaperInfos : null);
			this.SetShaper(shaper, coordinatorFactory, depth);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00017BEC File Offset: 0x00015DEC
		private void SetShaper(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth)
		{
			this.Shaper = shaper;
			this.CoordinatorFactory = coordinatorFactory;
			this.DataRecord = new BridgeDataRecord(shaper, depth);
			this._hasRows = false;
			if (!this.Shaper.DataWaiting)
			{
				this.Shaper.DataWaiting = this.Shaper.RootEnumerator.MoveNext();
			}
			if (this.Shaper.DataWaiting)
			{
				RecordState recordState = this.Shaper.RootEnumerator.Current;
				if (recordState != null)
				{
					this._hasRows = (recordState.CoordinatorFactory == this.CoordinatorFactory);
				}
			}
			this.DefaultRecordState = coordinatorFactory.GetDefaultRecordState(this.Shaper);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00017C8C File Offset: 0x00015E8C
		internal static DbDataReader Create(DbDataReader storeDataReader, ColumnMap columnMap, MetadataWorkspace workspace, IEnumerable<ColumnMap> nextResultColumnMaps)
		{
			KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> keyValuePair = BridgeDataReader.CreateShaperInfo(storeDataReader, columnMap, workspace);
			return new BridgeDataReader(keyValuePair.Key, keyValuePair.Value, 0, BridgeDataReader.GetNextResultShaperInfo(storeDataReader, workspace, nextResultColumnMaps).GetEnumerator());
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00017CC8 File Offset: 0x00015EC8
		private static KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> CreateShaperInfo(DbDataReader storeDataReader, ColumnMap columnMap, MetadataWorkspace workspace)
		{
			QueryCacheManager queryCacheManager = workspace.GetQueryCacheManager();
			ShaperFactory<RecordState> shaperFactory = Translator.TranslateColumnMap<RecordState>(queryCacheManager, columnMap, workspace, null, MergeOption.NoTracking, true);
			Shaper<RecordState> shaper = shaperFactory.Create(storeDataReader, null, workspace, MergeOption.NoTracking, true);
			return new KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>(shaper, shaper.RootCoordinator.TypedCoordinatorFactory);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00017D05 File Offset: 0x00015F05
		private static IEnumerable<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> GetNextResultShaperInfo(DbDataReader storeDataReader, MetadataWorkspace workspace, IEnumerable<ColumnMap> nextResultColumnMaps)
		{
			foreach (ColumnMap columnMap in nextResultColumnMaps)
			{
				yield return BridgeDataReader.CreateShaperInfo(storeDataReader, columnMap, workspace);
			}
			IEnumerator<ColumnMap> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00017D23 File Offset: 0x00015F23
		internal void CloseImplicitly()
		{
			this.Consume();
			this.DataRecord.CloseImplicitly();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017D36 File Offset: 0x00015F36
		private void Consume()
		{
			while (this.ReadInternal())
			{
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017D40 File Offset: 0x00015F40
		internal static Type GetClrTypeFromTypeMetadata(TypeUsage typeUsage)
		{
			PrimitiveType primitiveType;
			Type result;
			if (TypeHelpers.TryGetEdmType<PrimitiveType>(typeUsage, out primitiveType))
			{
				result = primitiveType.ClrEquivalentType;
			}
			else if (TypeSemantics.IsReferenceType(typeUsage))
			{
				result = typeof(EntityKey);
			}
			else if (TypeUtils.IsStructuredType(typeUsage))
			{
				result = typeof(DbDataRecord);
			}
			else if (TypeUtils.IsCollectionType(typeUsage))
			{
				result = typeof(DbDataReader);
			}
			else if (TypeUtils.IsEnumerationType(typeUsage))
			{
				result = ((EnumType)typeUsage.EdmType).UnderlyingType.ClrEquivalentType;
			}
			else
			{
				result = typeof(object);
			}
			return result;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00017DCB File Offset: 0x00015FCB
		public override int Depth
		{
			get
			{
				this.AssertReaderIsOpen("Depth");
				return this.DataRecord.Depth;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00017DE3 File Offset: 0x00015FE3
		public override bool HasRows
		{
			get
			{
				this.AssertReaderIsOpen("HasRows");
				return this._hasRows;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00017DF6 File Offset: 0x00015FF6
		public override bool IsClosed
		{
			get
			{
				return this._isClosed || this.DataRecord.IsClosed;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00017E10 File Offset: 0x00016010
		public override int RecordsAffected
		{
			get
			{
				int result = -1;
				if (this.DataRecord.Depth == 0)
				{
					result = this.Shaper.Reader.RecordsAffected;
				}
				return result;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017E3E File Offset: 0x0001603E
		private void AssertReaderIsOpen(string methodName)
		{
			if (this.IsClosed)
			{
				if (this.DataRecord.IsImplicitlyClosed)
				{
					throw EntityUtil.ImplicitlyClosedDataReaderError();
				}
				if (this.DataRecord.IsExplicitlyClosed)
				{
					throw EntityUtil.DataReaderClosed(methodName);
				}
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017E70 File Offset: 0x00016070
		public override void Close()
		{
			this.DataRecord.CloseExplicitly();
			if (!this._isClosed)
			{
				this._isClosed = true;
				if (this.DataRecord.Depth == 0)
				{
					this.Shaper.Reader.Close();
				}
				else
				{
					this.Consume();
				}
			}
			if (this.NextResultShaperInfoEnumerator != null)
			{
				this.NextResultShaperInfoEnumerator.Dispose();
				this.NextResultShaperInfoEnumerator = null;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00017ED8 File Offset: 0x000160D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, true);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00017EEE File Offset: 0x000160EE
		public override DataTable GetSchemaTable()
		{
			throw EntityUtil.NotSupported(Strings.ADP_GetSchemaTableIsNotSupported);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00017EFC File Offset: 0x000160FC
		public override bool NextResult()
		{
			this.AssertReaderIsOpen("NextResult");
			if (this.NextResultShaperInfoEnumerator != null && this.Shaper.Reader.NextResult() && this.NextResultShaperInfoEnumerator.MoveNext())
			{
				KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> keyValuePair = this.NextResultShaperInfoEnumerator.Current;
				this.DataRecord.CloseImplicitly();
				this.SetShaper(keyValuePair.Key, keyValuePair.Value, 0);
				return true;
			}
			if (this.DataRecord.Depth == 0)
			{
				CommandHelper.ConsumeReader(this.Shaper.Reader);
			}
			else
			{
				this.Consume();
			}
			this.CloseImplicitly();
			this.DataRecord.SetRecordSource(null, false);
			return false;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00017FA4 File Offset: 0x000161A4
		public override bool Read()
		{
			this.AssertReaderIsOpen("Read");
			this.DataRecord.CloseImplicitly();
			bool flag = this.ReadInternal();
			this.DataRecord.SetRecordSource(this.Shaper.RootEnumerator.Current, flag);
			return flag;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017FEC File Offset: 0x000161EC
		private bool ReadInternal()
		{
			bool result = false;
			if (!this.Shaper.DataWaiting)
			{
				this.Shaper.DataWaiting = this.Shaper.RootEnumerator.MoveNext();
			}
			while (this.Shaper.DataWaiting && this.Shaper.RootEnumerator.Current.CoordinatorFactory != this.CoordinatorFactory && this.Shaper.RootEnumerator.Current.CoordinatorFactory.Depth > this.CoordinatorFactory.Depth)
			{
				this.Shaper.DataWaiting = this.Shaper.RootEnumerator.MoveNext();
			}
			if (this.Shaper.DataWaiting && this.Shaper.RootEnumerator.Current.CoordinatorFactory == this.CoordinatorFactory)
			{
				this.Shaper.DataWaiting = false;
				this.Shaper.RootEnumerator.Current.AcceptPendingValues();
				result = true;
			}
			return result;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000180E0 File Offset: 0x000162E0
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				this.AssertReaderIsOpen("DataRecordInfo");
				DataRecordInfo dataRecordInfo;
				if (this.DataRecord.HasData)
				{
					dataRecordInfo = this.DataRecord.DataRecordInfo;
				}
				else
				{
					dataRecordInfo = this.DefaultRecordState.DataRecordInfo;
				}
				return dataRecordInfo;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00018120 File Offset: 0x00016320
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen("FieldCount");
				return this.DefaultRecordState.ColumnCount;
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00018148 File Offset: 0x00016348
		public override string GetDataTypeName(int ordinal)
		{
			this.AssertReaderIsOpen("GetDataTypeName");
			string result;
			if (this.DataRecord.HasData)
			{
				result = this.DataRecord.GetDataTypeName(ordinal);
			}
			else
			{
				result = TypeHelpers.GetFullName(this.DefaultRecordState.GetTypeUsage(ordinal));
			}
			return result;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00018190 File Offset: 0x00016390
		public override Type GetFieldType(int ordinal)
		{
			this.AssertReaderIsOpen("GetFieldType");
			Type result;
			if (this.DataRecord.HasData)
			{
				result = this.DataRecord.GetFieldType(ordinal);
			}
			else
			{
				result = BridgeDataReader.GetClrTypeFromTypeMetadata(this.DefaultRecordState.GetTypeUsage(ordinal));
			}
			return result;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000181D8 File Offset: 0x000163D8
		public override string GetName(int ordinal)
		{
			this.AssertReaderIsOpen("GetName");
			string name;
			if (this.DataRecord.HasData)
			{
				name = this.DataRecord.GetName(ordinal);
			}
			else
			{
				name = this.DefaultRecordState.GetName(ordinal);
			}
			return name;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001821C File Offset: 0x0001641C
		public override int GetOrdinal(string name)
		{
			this.AssertReaderIsOpen("GetOrdinal");
			int ordinal;
			if (this.DataRecord.HasData)
			{
				ordinal = this.DataRecord.GetOrdinal(name);
			}
			else
			{
				ordinal = this.DefaultRecordState.GetOrdinal(name);
			}
			return ordinal;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013A81 File Offset: 0x00011C81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x17000075 RID: 117
		public override object this[int ordinal]
		{
			get
			{
				return this.DataRecord[ordinal];
			}
		}

		// Token: 0x17000076 RID: 118
		public override object this[string name]
		{
			get
			{
				int ordinal = this.GetOrdinal(name);
				return this.DataRecord[ordinal];
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013A81 File Offset: 0x00011C81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetProviderSpecificValue(int ordinal)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00013A81 File Offset: 0x00011C81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetProviderSpecificValues(object[] values)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001828D File Offset: 0x0001648D
		public override object GetValue(int ordinal)
		{
			return this.DataRecord.GetValue(ordinal);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001829B File Offset: 0x0001649B
		public override int GetValues(object[] values)
		{
			return this.DataRecord.GetValues(values);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000182A9 File Offset: 0x000164A9
		public override bool GetBoolean(int ordinal)
		{
			return this.DataRecord.GetBoolean(ordinal);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000182B7 File Offset: 0x000164B7
		public override byte GetByte(int ordinal)
		{
			return this.DataRecord.GetByte(ordinal);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000182C5 File Offset: 0x000164C5
		public override char GetChar(int ordinal)
		{
			return this.DataRecord.GetChar(ordinal);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000182D3 File Offset: 0x000164D3
		public override DateTime GetDateTime(int ordinal)
		{
			return this.DataRecord.GetDateTime(ordinal);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000182E1 File Offset: 0x000164E1
		public override decimal GetDecimal(int ordinal)
		{
			return this.DataRecord.GetDecimal(ordinal);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000182EF File Offset: 0x000164EF
		public override double GetDouble(int ordinal)
		{
			return this.DataRecord.GetDouble(ordinal);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000182FD File Offset: 0x000164FD
		public override float GetFloat(int ordinal)
		{
			return this.DataRecord.GetFloat(ordinal);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001830B File Offset: 0x0001650B
		public override Guid GetGuid(int ordinal)
		{
			return this.DataRecord.GetGuid(ordinal);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018319 File Offset: 0x00016519
		public override short GetInt16(int ordinal)
		{
			return this.DataRecord.GetInt16(ordinal);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018327 File Offset: 0x00016527
		public override int GetInt32(int ordinal)
		{
			return this.DataRecord.GetInt32(ordinal);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00018335 File Offset: 0x00016535
		public override long GetInt64(int ordinal)
		{
			return this.DataRecord.GetInt64(ordinal);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00018343 File Offset: 0x00016543
		public override string GetString(int ordinal)
		{
			return this.DataRecord.GetString(ordinal);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00018351 File Offset: 0x00016551
		public override bool IsDBNull(int ordinal)
		{
			return this.DataRecord.IsDBNull(ordinal);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001835F File Offset: 0x0001655F
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this.DataRecord.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00018373 File Offset: 0x00016573
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			return this.DataRecord.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00018387 File Offset: 0x00016587
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return (DbDataReader)this.DataRecord.GetData(ordinal);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001839A File Offset: 0x0001659A
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return this.DataRecord.GetDataRecord(ordinal);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000183A8 File Offset: 0x000165A8
		public DbDataReader GetDataReader(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x04000748 RID: 1864
		private Shaper<RecordState> Shaper;

		// Token: 0x04000749 RID: 1865
		private IEnumerator<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> NextResultShaperInfoEnumerator;

		// Token: 0x0400074A RID: 1866
		private CoordinatorFactory<RecordState> CoordinatorFactory;

		// Token: 0x0400074B RID: 1867
		private RecordState DefaultRecordState;

		// Token: 0x0400074C RID: 1868
		private BridgeDataRecord DataRecord;

		// Token: 0x0400074D RID: 1869
		private bool _hasRows;

		// Token: 0x0400074E RID: 1870
		private bool _isClosed;
	}
}

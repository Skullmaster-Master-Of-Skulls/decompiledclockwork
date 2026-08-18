using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Query.ResultAssembly
{
	// Token: 0x020006AD RID: 1709
	internal class BridgeDataReader : DbDataReader, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x0600439D RID: 17309 RVA: 0x00140CE8 File Offset: 0x0013EEE8
		internal BridgeDataReader(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth, IEnumerator<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> nextResultShaperInfos)
		{
			BridgeDataReader <>4__this = this;
			this._nextResultShaperInfoEnumerator = nextResultShaperInfos;
			this._initialize = delegate()
			{
				<>4__this.SetShaper(shaper, coordinatorFactory, depth);
			};
			this._initializeAsync = ((CancellationToken ct) => <>4__this.SetShaperAsync(shaper, coordinatorFactory, depth, ct));
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x00140D57 File Offset: 0x0013EF57
		protected virtual void EnsureInitialized()
		{
			if (Interlocked.CompareExchange(ref this._initialized, 1, 0) == 0)
			{
				this._initialize();
			}
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x00140D73 File Offset: 0x0013EF73
		protected virtual Task EnsureInitializedAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (Interlocked.CompareExchange(ref this._initialized, 1, 0) != 0)
			{
				return Task.FromResult<object>(null);
			}
			return this._initializeAsync(cancellationToken);
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x00140DA0 File Offset: 0x0013EFA0
		private void SetShaper(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth)
		{
			this._shaper = shaper;
			this._coordinatorFactory = coordinatorFactory;
			this._dataRecord = new BridgeDataRecord(shaper, depth);
			if (!this._shaper.DataWaiting)
			{
				this._shaper.DataWaiting = this._shaper.RootEnumerator.MoveNext();
			}
			this.InitializeHasRows();
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x00140F80 File Offset: 0x0013F180
		private async Task SetShaperAsync(Shaper<RecordState> shaper, CoordinatorFactory<RecordState> coordinatorFactory, int depth, CancellationToken cancellationToken)
		{
			this._shaper = shaper;
			this._coordinatorFactory = coordinatorFactory;
			this._dataRecord = new BridgeDataRecord(shaper, depth);
			if (!this._shaper.DataWaiting)
			{
				this._shaper.DataWaiting = await this._shaper.RootEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			this.InitializeHasRows();
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x00140FE8 File Offset: 0x0013F1E8
		private void InitializeHasRows()
		{
			this._hasRows = false;
			if (this._shaper.DataWaiting)
			{
				RecordState recordState = this._shaper.RootEnumerator.Current;
				if (recordState != null)
				{
					this._hasRows = (recordState.CoordinatorFactory == this._coordinatorFactory);
				}
			}
			this._defaultRecordState = this._coordinatorFactory.GetDefaultRecordState(this._shaper);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x00141048 File Offset: 0x0013F248
		private void AssertReaderIsOpen(string methodName)
		{
			if (this.IsClosed)
			{
				if (this._dataRecord.IsImplicitlyClosed)
				{
					throw Error.ADP_ImplicitlyClosedDataReaderError();
				}
				if (this._dataRecord.IsExplicitlyClosed)
				{
					throw Error.ADP_DataReaderClosed(methodName);
				}
			}
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x00141079 File Offset: 0x0013F279
		internal void CloseImplicitly()
		{
			this.EnsureInitialized();
			this.Consume();
			this._dataRecord.CloseImplicitly();
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x0014128C File Offset: 0x0013F48C
		internal async Task CloseImplicitlyAsync(CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			await this.ConsumeAsync(cancellationToken).WithCurrentCulture();
			await this._dataRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x001412DA File Offset: 0x0013F4DA
		private void Consume()
		{
			while (this.ReadInternal())
			{
			}
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x001413C4 File Offset: 0x0013F5C4
		private async Task ConsumeAsync(CancellationToken cancellationToken)
		{
			while (await this.ReadInternalAsync(cancellationToken).WithCurrentCulture<bool>())
			{
			}
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x00141414 File Offset: 0x0013F614
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

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x0014149F File Offset: 0x0013F69F
		public override int Depth
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("Depth");
				return this._dataRecord.Depth;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x001414BD File Offset: 0x0013F6BD
		public override bool HasRows
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("HasRows");
				return this._hasRows;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x001414D6 File Offset: 0x0013F6D6
		public override bool IsClosed
		{
			get
			{
				this.EnsureInitialized();
				return this._isClosed || this._dataRecord.IsClosed;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x001414F4 File Offset: 0x0013F6F4
		public override int RecordsAffected
		{
			get
			{
				this.EnsureInitialized();
				int result = -1;
				if (this._dataRecord.Depth == 0)
				{
					result = this._shaper.Reader.RecordsAffected;
				}
				return result;
			}
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x00141528 File Offset: 0x0013F728
		public override void Close()
		{
			this.EnsureInitialized();
			this._dataRecord.CloseExplicitly();
			if (!this._isClosed)
			{
				this._isClosed = true;
				if (this._dataRecord.Depth == 0)
				{
					this._shaper.Reader.Close();
				}
				else
				{
					this.Consume();
				}
			}
			if (this._nextResultShaperInfoEnumerator != null)
			{
				this._nextResultShaperInfoEnumerator.Dispose();
				this._nextResultShaperInfoEnumerator = null;
			}
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x00141594 File Offset: 0x0013F794
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, true);
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x001415AA File Offset: 0x0013F7AA
		public override DataTable GetSchemaTable()
		{
			throw new NotSupportedException(Strings.ADP_GetSchemaTableIsNotSupported);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x001415B8 File Offset: 0x0013F7B8
		public override bool NextResult()
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("NextResult");
			if (this._nextResultShaperInfoEnumerator != null && this._shaper.Reader.NextResult() && this._nextResultShaperInfoEnumerator.MoveNext())
			{
				KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> keyValuePair = this._nextResultShaperInfoEnumerator.Current;
				this._dataRecord.CloseImplicitly();
				this.SetShaper(keyValuePair.Key, keyValuePair.Value, 0);
				return true;
			}
			if (this._dataRecord.Depth == 0)
			{
				CommandHelper.ConsumeReader(this._shaper.Reader);
			}
			else
			{
				this.Consume();
			}
			this.CloseImplicitly();
			this._dataRecord.SetRecordSource(null, false);
			return false;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x00141A94 File Offset: 0x0013FC94
		public override async Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			this.AssertReaderIsOpen("NextResult");
			bool result;
			if (this._nextResultShaperInfoEnumerator != null && await this._shaper.Reader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>() && this._nextResultShaperInfoEnumerator.MoveNext())
			{
				KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> nextResultShaperInfo = this._nextResultShaperInfoEnumerator.Current;
				await this._dataRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
				this.SetShaper(nextResultShaperInfo.Key, nextResultShaperInfo.Value, 0);
				result = true;
			}
			else
			{
				if (this._dataRecord.Depth == 0)
				{
					await CommandHelper.ConsumeReaderAsync(this._shaper.Reader, cancellationToken).WithCurrentCulture();
				}
				else
				{
					await this.ConsumeAsync(cancellationToken).WithCurrentCulture();
				}
				await this.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
				this._dataRecord.SetRecordSource(null, false);
				result = false;
			}
			return result;
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x00141AE4 File Offset: 0x0013FCE4
		public override bool Read()
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("Read");
			this._dataRecord.CloseImplicitly();
			bool flag = this.ReadInternal();
			this._dataRecord.SetRecordSource(this._shaper.RootEnumerator.Current, flag);
			return flag;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x00141D7C File Offset: 0x0013FF7C
		public override async Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			this.AssertReaderIsOpen("Read");
			await this._dataRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
			bool result = await this.ReadInternalAsync(cancellationToken).WithCurrentCulture<bool>();
			this._dataRecord.SetRecordSource(this._shaper.RootEnumerator.Current, result);
			return result;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x00141DCC File Offset: 0x0013FFCC
		private bool ReadInternal()
		{
			bool result = false;
			if (!this._shaper.DataWaiting)
			{
				this._shaper.DataWaiting = this._shaper.RootEnumerator.MoveNext();
			}
			while (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory != this._coordinatorFactory && this._shaper.RootEnumerator.Current.CoordinatorFactory.Depth > this._coordinatorFactory.Depth)
			{
				this._shaper.DataWaiting = this._shaper.RootEnumerator.MoveNext();
			}
			if (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory == this._coordinatorFactory)
			{
				this._shaper.DataWaiting = false;
				this._shaper.RootEnumerator.Current.AcceptPendingValues();
				result = true;
			}
			return result;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x001421AC File Offset: 0x001403AC
		private async Task<bool> ReadInternalAsync(CancellationToken cancellationToken)
		{
			bool result = false;
			if (!this._shaper.DataWaiting)
			{
				this._shaper.DataWaiting = await this._shaper.RootEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			while (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory != this._coordinatorFactory && this._shaper.RootEnumerator.Current.CoordinatorFactory.Depth > this._coordinatorFactory.Depth)
			{
				this._shaper.DataWaiting = await this._shaper.RootEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
			}
			if (this._shaper.DataWaiting && this._shaper.RootEnumerator.Current.CoordinatorFactory == this._coordinatorFactory)
			{
				this._shaper.DataWaiting = false;
				this._shaper.RootEnumerator.Current.AcceptPendingValues();
				result = true;
			}
			return result;
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060043B6 RID: 17334 RVA: 0x001421FC File Offset: 0x001403FC
		public override int FieldCount
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("FieldCount");
				return this._defaultRecordState.ColumnCount;
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x00142228 File Offset: 0x00140428
		public override string GetDataTypeName(int ordinal)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetDataTypeName");
			string result;
			if (this._dataRecord.HasData)
			{
				result = this._dataRecord.GetDataTypeName(ordinal);
			}
			else
			{
				result = this._defaultRecordState.GetTypeUsage(ordinal).ToString();
			}
			return result;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x00142278 File Offset: 0x00140478
		public override Type GetFieldType(int ordinal)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetFieldType");
			Type result;
			if (this._dataRecord.HasData)
			{
				result = this._dataRecord.GetFieldType(ordinal);
			}
			else
			{
				result = BridgeDataReader.GetClrTypeFromTypeMetadata(this._defaultRecordState.GetTypeUsage(ordinal));
			}
			return result;
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x001422C8 File Offset: 0x001404C8
		public override string GetName(int ordinal)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetName");
			string name;
			if (this._dataRecord.HasData)
			{
				name = this._dataRecord.GetName(ordinal);
			}
			else
			{
				name = this._defaultRecordState.GetName(ordinal);
			}
			return name;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x00142310 File Offset: 0x00140510
		public override int GetOrdinal(string name)
		{
			this.EnsureInitialized();
			this.AssertReaderIsOpen("GetOrdinal");
			int ordinal;
			if (this._dataRecord.HasData)
			{
				ordinal = this._dataRecord.GetOrdinal(name);
			}
			else
			{
				ordinal = this._defaultRecordState.GetOrdinal(name);
			}
			return ordinal;
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x00142358 File Offset: 0x00140558
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000A3F RID: 2623
		public override object this[int ordinal]
		{
			get
			{
				this.EnsureInitialized();
				return this._dataRecord[ordinal];
			}
		}

		// Token: 0x17000A40 RID: 2624
		public override object this[string name]
		{
			get
			{
				this.EnsureInitialized();
				int ordinal = this.GetOrdinal(name);
				return this._dataRecord[ordinal];
			}
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x0014239B File Offset: 0x0014059B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override object GetProviderSpecificValue(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x001423A2 File Offset: 0x001405A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetProviderSpecificValues(object[] values)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x001423A9 File Offset: 0x001405A9
		public override object GetValue(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetValue(ordinal);
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x0014253C File Offset: 0x0014073C
		public override async Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			await this.EnsureInitializedAsync(cancellationToken).WithCurrentCulture();
			return await base.GetFieldValueAsync<T>(ordinal, cancellationToken).WithCurrentCulture<T>();
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x00142592 File Offset: 0x00140792
		public override int GetValues(object[] values)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetValues(values);
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x001425A6 File Offset: 0x001407A6
		public override bool GetBoolean(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetBoolean(ordinal);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x001425BA File Offset: 0x001407BA
		public override byte GetByte(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetByte(ordinal);
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x001425CE File Offset: 0x001407CE
		public override char GetChar(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetChar(ordinal);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x001425E2 File Offset: 0x001407E2
		public override DateTime GetDateTime(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDateTime(ordinal);
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x001425F6 File Offset: 0x001407F6
		public override decimal GetDecimal(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDecimal(ordinal);
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x0014260A File Offset: 0x0014080A
		public override double GetDouble(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDouble(ordinal);
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x0014261E File Offset: 0x0014081E
		public override float GetFloat(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetFloat(ordinal);
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x00142632 File Offset: 0x00140832
		public override Guid GetGuid(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetGuid(ordinal);
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x00142646 File Offset: 0x00140846
		public override short GetInt16(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetInt16(ordinal);
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x0014265A File Offset: 0x0014085A
		public override int GetInt32(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetInt32(ordinal);
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x0014266E File Offset: 0x0014086E
		public override long GetInt64(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetInt64(ordinal);
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x00142682 File Offset: 0x00140882
		public override string GetString(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetString(ordinal);
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x00142696 File Offset: 0x00140896
		public override bool IsDBNull(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.IsDBNull(ordinal);
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x001426AA File Offset: 0x001408AA
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x001426C4 File Offset: 0x001408C4
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x001426DE File Offset: 0x001408DE
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			this.EnsureInitialized();
			return (DbDataReader)this._dataRecord.GetData(ordinal);
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060043D3 RID: 17363 RVA: 0x001426F8 File Offset: 0x001408F8
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				this.EnsureInitialized();
				this.AssertReaderIsOpen("DataRecordInfo");
				DataRecordInfo dataRecordInfo;
				if (this._dataRecord.HasData)
				{
					dataRecordInfo = this._dataRecord.DataRecordInfo;
				}
				else
				{
					dataRecordInfo = this._defaultRecordState.DataRecordInfo;
				}
				return dataRecordInfo;
			}
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x0014273E File Offset: 0x0014093E
		public DbDataRecord GetDataRecord(int ordinal)
		{
			this.EnsureInitialized();
			return this._dataRecord.GetDataRecord(ordinal);
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x00142752 File Offset: 0x00140952
		public DbDataReader GetDataReader(int ordinal)
		{
			this.EnsureInitialized();
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x04001912 RID: 6418
		private Shaper<RecordState> _shaper;

		// Token: 0x04001913 RID: 6419
		private IEnumerator<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> _nextResultShaperInfoEnumerator;

		// Token: 0x04001914 RID: 6420
		private CoordinatorFactory<RecordState> _coordinatorFactory;

		// Token: 0x04001915 RID: 6421
		private RecordState _defaultRecordState;

		// Token: 0x04001916 RID: 6422
		private BridgeDataRecord _dataRecord;

		// Token: 0x04001917 RID: 6423
		private bool _hasRows;

		// Token: 0x04001918 RID: 6424
		private bool _isClosed;

		// Token: 0x04001919 RID: 6425
		private int _initialized;

		// Token: 0x0400191A RID: 6426
		private readonly Action _initialize;

		// Token: 0x0400191B RID: 6427
		private readonly Func<CancellationToken, Task> _initializeAsync;
	}
}

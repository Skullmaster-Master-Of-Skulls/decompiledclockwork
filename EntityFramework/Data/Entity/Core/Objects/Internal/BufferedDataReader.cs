using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000200 RID: 512
	internal class BufferedDataReader : DbDataReader
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x0004CE03 File Offset: 0x0004B003
		public BufferedDataReader(DbDataReader reader)
		{
			this._underlyingReader = reader;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004CE1D File Offset: 0x0004B01D
		public override int RecordsAffected
		{
			get
			{
				return this._recordsAffected;
			}
		}

		// Token: 0x170001D0 RID: 464
		public override object this[string name]
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001D1 RID: 465
		public override object this[int ordinal]
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004CE33 File Offset: 0x0004B033
		public override int Depth
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004CE3A File Offset: 0x0004B03A
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._currentResultSet.FieldCount;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004CE4D File Offset: 0x0004B04D
		public override bool HasRows
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._currentResultSet.HasRows;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004CE60 File Offset: 0x0004B060
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004CE68 File Offset: 0x0004B068
		private void AssertReaderIsOpen()
		{
			if (this._isClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004CE78 File Offset: 0x0004B078
		private void AssertReaderIsOpenWithData()
		{
			if (this._isClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
			if (!this._currentResultSet.IsDataReady)
			{
				throw Error.ADP_NoData();
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004CE9B File Offset: 0x0004B09B
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		private void AssertFieldIsReady(int ordinal)
		{
			if (this._isClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
			if (!this._currentResultSet.IsDataReady)
			{
				throw Error.ADP_NoData();
			}
			if (0 > ordinal || ordinal > this._currentResultSet.FieldCount)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004CED8 File Offset: 0x0004B0D8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "columnTypes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "nullableColumns")]
		internal void Initialize(string providerManifestToken, DbProviderServices providerServices, Type[] columnTypes, bool[] nullableColumns)
		{
			DbDataReader underlyingReader = this._underlyingReader;
			if (underlyingReader == null)
			{
				return;
			}
			this._underlyingReader = null;
			try
			{
				if (columnTypes != null && underlyingReader.GetType().Name != "SqlDataReader")
				{
					this._bufferedDataRecords.Add(ShapedBufferedDataRecord.Initialize(providerManifestToken, providerServices, underlyingReader, columnTypes, nullableColumns));
				}
				else
				{
					this._bufferedDataRecords.Add(ShapelessBufferedDataRecord.Initialize(providerManifestToken, providerServices, underlyingReader));
				}
				while (underlyingReader.NextResult())
				{
					this._bufferedDataRecords.Add(ShapelessBufferedDataRecord.Initialize(providerManifestToken, providerServices, underlyingReader));
				}
				this._recordsAffected = underlyingReader.RecordsAffected;
				this._currentResultSet = this._bufferedDataRecords[this._currentResultSetNumber];
			}
			finally
			{
				underlyingReader.Dispose();
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004D3C4 File Offset: 0x0004B5C4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "columnTypes")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "nullableColumns")]
		internal async Task InitializeAsync(string providerManifestToken, DbProviderServices providerSerivces, Type[] columnTypes, bool[] nullableColumns, CancellationToken cancellationToken)
		{
			if (this._underlyingReader != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				DbDataReader reader = this._underlyingReader;
				this._underlyingReader = null;
				try
				{
					if (columnTypes != null && reader.GetType().Name != "SqlDataReader")
					{
						this._bufferedDataRecords.Add(await ShapedBufferedDataRecord.InitializeAsync(providerManifestToken, providerSerivces, reader, columnTypes, nullableColumns, cancellationToken).WithCurrentCulture<BufferedDataRecord>());
					}
					else
					{
						this._bufferedDataRecords.Add(await ShapelessBufferedDataRecord.InitializeAsync(providerManifestToken, providerSerivces, reader, cancellationToken).WithCurrentCulture<ShapelessBufferedDataRecord>());
					}
					while (await reader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>())
					{
						this._bufferedDataRecords.Add(await ShapelessBufferedDataRecord.InitializeAsync(providerManifestToken, providerSerivces, reader, cancellationToken).WithCurrentCulture<ShapelessBufferedDataRecord>());
					}
					this._recordsAffected = reader.RecordsAffected;
					this._currentResultSet = this._bufferedDataRecords[this._currentResultSetNumber];
				}
				finally
				{
					reader.Dispose();
				}
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004D434 File Offset: 0x0004B634
		public override void Close()
		{
			this._bufferedDataRecords = null;
			this._isClosed = true;
			DbDataReader underlyingReader = this._underlyingReader;
			if (underlyingReader != null)
			{
				this._underlyingReader = null;
				underlyingReader.Dispose();
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004D466 File Offset: 0x0004B666
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing && !this.IsClosed)
			{
				this.Close();
			}
			this._disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004D48F File Offset: 0x0004B68F
		public override bool GetBoolean(int ordinal)
		{
			return this._currentResultSet.GetBoolean(ordinal);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0004D49D File Offset: 0x0004B69D
		public override byte GetByte(int ordinal)
		{
			return this._currentResultSet.GetByte(ordinal);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0004D4AB File Offset: 0x0004B6AB
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004D4B2 File Offset: 0x0004B6B2
		public override char GetChar(int ordinal)
		{
			return this._currentResultSet.GetChar(ordinal);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004D4C0 File Offset: 0x0004B6C0
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004D4C7 File Offset: 0x0004B6C7
		public override DateTime GetDateTime(int ordinal)
		{
			return this._currentResultSet.GetDateTime(ordinal);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004D4D5 File Offset: 0x0004B6D5
		public override decimal GetDecimal(int ordinal)
		{
			return this._currentResultSet.GetDecimal(ordinal);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004D4E3 File Offset: 0x0004B6E3
		public override double GetDouble(int ordinal)
		{
			return this._currentResultSet.GetDouble(ordinal);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004D4F1 File Offset: 0x0004B6F1
		public override float GetFloat(int ordinal)
		{
			return this._currentResultSet.GetFloat(ordinal);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004D4FF File Offset: 0x0004B6FF
		public override Guid GetGuid(int ordinal)
		{
			return this._currentResultSet.GetGuid(ordinal);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004D50D File Offset: 0x0004B70D
		public override short GetInt16(int ordinal)
		{
			return this._currentResultSet.GetInt16(ordinal);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004D51B File Offset: 0x0004B71B
		public override int GetInt32(int ordinal)
		{
			return this._currentResultSet.GetInt32(ordinal);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004D529 File Offset: 0x0004B729
		public override long GetInt64(int ordinal)
		{
			return this._currentResultSet.GetInt64(ordinal);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004D537 File Offset: 0x0004B737
		public override string GetString(int ordinal)
		{
			return this._currentResultSet.GetString(ordinal);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004D545 File Offset: 0x0004B745
		public override T GetFieldValue<T>(int ordinal)
		{
			return this._currentResultSet.GetFieldValue<T>(ordinal);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004D553 File Offset: 0x0004B753
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return this._currentResultSet.GetFieldValueAsync<T>(ordinal, cancellationToken);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004D562 File Offset: 0x0004B762
		public override object GetValue(int ordinal)
		{
			return this._currentResultSet.GetValue(ordinal);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004D570 File Offset: 0x0004B770
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			this.AssertReaderIsOpenWithData();
			return this._currentResultSet.GetValues(values);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004D590 File Offset: 0x0004B790
		public override string GetDataTypeName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetDataTypeName(ordinal);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004D5A4 File Offset: 0x0004B7A4
		public override Type GetFieldType(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetFieldType(ordinal);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004D5B8 File Offset: 0x0004B7B8
		public override string GetName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetName(ordinal);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004D5CC File Offset: 0x0004B7CC
		public override int GetOrdinal(string name)
		{
			Check.NotNull<string>(name, "name");
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetOrdinal(name);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004D5EC File Offset: 0x0004B7EC
		public override bool IsDBNull(int ordinal)
		{
			return this._currentResultSet.IsDBNull(ordinal);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004D5FA File Offset: 0x0004B7FA
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return this._currentResultSet.IsDBNullAsync(ordinal, cancellationToken);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004D609 File Offset: 0x0004B809
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004D611 File Offset: 0x0004B811
		public override DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004D618 File Offset: 0x0004B818
		public override bool NextResult()
		{
			this.AssertReaderIsOpen();
			if (++this._currentResultSetNumber < this._bufferedDataRecords.Count)
			{
				this._currentResultSet = this._bufferedDataRecords[this._currentResultSetNumber];
				return true;
			}
			this._currentResultSet = null;
			return false;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004D66A File Offset: 0x0004B86A
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return Task.FromResult<bool>(this.NextResult());
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004D67E File Offset: 0x0004B87E
		public override bool Read()
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.Read();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004D691 File Offset: 0x0004B891
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.AssertReaderIsOpen();
			return this._currentResultSet.ReadAsync(cancellationToken);
		}

		// Token: 0x0400055C RID: 1372
		private DbDataReader _underlyingReader;

		// Token: 0x0400055D RID: 1373
		private List<BufferedDataRecord> _bufferedDataRecords = new List<BufferedDataRecord>();

		// Token: 0x0400055E RID: 1374
		private BufferedDataRecord _currentResultSet;

		// Token: 0x0400055F RID: 1375
		private int _currentResultSetNumber;

		// Token: 0x04000560 RID: 1376
		private int _recordsAffected;

		// Token: 0x04000561 RID: 1377
		private bool _disposed;

		// Token: 0x04000562 RID: 1378
		private bool _isClosed;
	}
}

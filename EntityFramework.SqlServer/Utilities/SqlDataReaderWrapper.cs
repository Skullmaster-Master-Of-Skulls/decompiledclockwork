using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002A RID: 42
	internal class SqlDataReaderWrapper : MarshalByRefObject
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000B1E4 File Offset: 0x000093E4
		protected SqlDataReaderWrapper()
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B1EC File Offset: 0x000093EC
		public SqlDataReaderWrapper(SqlDataReader sqlDataReader)
		{
			this._sqlDataReader = sqlDataReader;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B1FB File Offset: 0x000093FB
		public virtual IDataReader GetData(int i)
		{
			return ((IDataRecord)this._sqlDataReader).GetData(i);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B209 File Offset: 0x00009409
		public virtual void Dispose()
		{
			this._sqlDataReader.Dispose();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B216 File Offset: 0x00009416
		public virtual Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			return this._sqlDataReader.GetFieldValueAsync<T>(ordinal);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B224 File Offset: 0x00009424
		public virtual Task<bool> IsDBNullAsync(int ordinal)
		{
			return this._sqlDataReader.IsDBNullAsync(ordinal);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B232 File Offset: 0x00009432
		public virtual Task<bool> ReadAsync()
		{
			return this._sqlDataReader.ReadAsync();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B23F File Offset: 0x0000943F
		public virtual Task<bool> NextResultAsync()
		{
			return this._sqlDataReader.NextResultAsync();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B24C File Offset: 0x0000944C
		public virtual void Close()
		{
			this._sqlDataReader.Close();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B259 File Offset: 0x00009459
		public virtual string GetDataTypeName(int i)
		{
			return this._sqlDataReader.GetDataTypeName(i);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B267 File Offset: 0x00009467
		public virtual IEnumerator GetEnumerator()
		{
			return this._sqlDataReader.GetEnumerator();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B274 File Offset: 0x00009474
		public virtual Type GetFieldType(int i)
		{
			return this._sqlDataReader.GetFieldType(i);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B282 File Offset: 0x00009482
		public virtual string GetName(int i)
		{
			return this._sqlDataReader.GetName(i);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B290 File Offset: 0x00009490
		public virtual Type GetProviderSpecificFieldType(int i)
		{
			return this._sqlDataReader.GetProviderSpecificFieldType(i);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B29E File Offset: 0x0000949E
		public virtual int GetOrdinal(string name)
		{
			return this._sqlDataReader.GetOrdinal(name);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B2AC File Offset: 0x000094AC
		public virtual object GetProviderSpecificValue(int i)
		{
			return this._sqlDataReader.GetProviderSpecificValue(i);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B2BA File Offset: 0x000094BA
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this._sqlDataReader.GetProviderSpecificValues(values);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public virtual DataTable GetSchemaTable()
		{
			return this._sqlDataReader.GetSchemaTable();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B2D5 File Offset: 0x000094D5
		public virtual bool GetBoolean(int i)
		{
			return this._sqlDataReader.GetBoolean(i);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B2E3 File Offset: 0x000094E3
		public virtual XmlReader GetXmlReader(int i)
		{
			return this._sqlDataReader.GetXmlReader(i);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B2F1 File Offset: 0x000094F1
		public virtual Stream GetStream(int i)
		{
			return this._sqlDataReader.GetStream(i);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B2FF File Offset: 0x000094FF
		public virtual byte GetByte(int i)
		{
			return this._sqlDataReader.GetByte(i);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000B30D File Offset: 0x0000950D
		public virtual long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this._sqlDataReader.GetBytes(i, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B321 File Offset: 0x00009521
		public virtual TextReader GetTextReader(int i)
		{
			return this._sqlDataReader.GetTextReader(i);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B32F File Offset: 0x0000952F
		public virtual char GetChar(int i)
		{
			return this._sqlDataReader.GetChar(i);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B33D File Offset: 0x0000953D
		public virtual long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this._sqlDataReader.GetChars(i, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000B351 File Offset: 0x00009551
		public virtual DateTime GetDateTime(int i)
		{
			return this._sqlDataReader.GetDateTime(i);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000B35F File Offset: 0x0000955F
		public virtual decimal GetDecimal(int i)
		{
			return this._sqlDataReader.GetDecimal(i);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000B36D File Offset: 0x0000956D
		public virtual double GetDouble(int i)
		{
			return this._sqlDataReader.GetDouble(i);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B37B File Offset: 0x0000957B
		public virtual float GetFloat(int i)
		{
			return this._sqlDataReader.GetFloat(i);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B389 File Offset: 0x00009589
		public virtual Guid GetGuid(int i)
		{
			return this._sqlDataReader.GetGuid(i);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B397 File Offset: 0x00009597
		public virtual short GetInt16(int i)
		{
			return this._sqlDataReader.GetInt16(i);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B3A5 File Offset: 0x000095A5
		public virtual int GetInt32(int i)
		{
			return this._sqlDataReader.GetInt32(i);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B3B3 File Offset: 0x000095B3
		public virtual long GetInt64(int i)
		{
			return this._sqlDataReader.GetInt64(i);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000B3C1 File Offset: 0x000095C1
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			return this._sqlDataReader.GetSqlBoolean(i);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B3CF File Offset: 0x000095CF
		public virtual SqlBinary GetSqlBinary(int i)
		{
			return this._sqlDataReader.GetSqlBinary(i);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B3DD File Offset: 0x000095DD
		public virtual SqlByte GetSqlByte(int i)
		{
			return this._sqlDataReader.GetSqlByte(i);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B3EB File Offset: 0x000095EB
		public virtual SqlBytes GetSqlBytes(int i)
		{
			return this._sqlDataReader.GetSqlBytes(i);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B3F9 File Offset: 0x000095F9
		public virtual SqlChars GetSqlChars(int i)
		{
			return this._sqlDataReader.GetSqlChars(i);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B407 File Offset: 0x00009607
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			return this._sqlDataReader.GetSqlDateTime(i);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B415 File Offset: 0x00009615
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			return this._sqlDataReader.GetSqlDecimal(i);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B423 File Offset: 0x00009623
		public virtual SqlGuid GetSqlGuid(int i)
		{
			return this._sqlDataReader.GetSqlGuid(i);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B431 File Offset: 0x00009631
		public virtual SqlDouble GetSqlDouble(int i)
		{
			return this._sqlDataReader.GetSqlDouble(i);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B43F File Offset: 0x0000963F
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			return this._sqlDataReader.GetSqlInt16(i);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B44D File Offset: 0x0000964D
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			return this._sqlDataReader.GetSqlInt32(i);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B45B File Offset: 0x0000965B
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			return this._sqlDataReader.GetSqlInt64(i);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B469 File Offset: 0x00009669
		public virtual SqlMoney GetSqlMoney(int i)
		{
			return this._sqlDataReader.GetSqlMoney(i);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B477 File Offset: 0x00009677
		public virtual SqlSingle GetSqlSingle(int i)
		{
			return this._sqlDataReader.GetSqlSingle(i);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B485 File Offset: 0x00009685
		public virtual SqlString GetSqlString(int i)
		{
			return this._sqlDataReader.GetSqlString(i);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B493 File Offset: 0x00009693
		public virtual SqlXml GetSqlXml(int i)
		{
			return this._sqlDataReader.GetSqlXml(i);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B4A1 File Offset: 0x000096A1
		public virtual object GetSqlValue(int i)
		{
			return this._sqlDataReader.GetSqlValue(i);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B4AF File Offset: 0x000096AF
		public virtual int GetSqlValues(object[] values)
		{
			return this._sqlDataReader.GetSqlValues(values);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B4BD File Offset: 0x000096BD
		public virtual string GetString(int i)
		{
			return this._sqlDataReader.GetString(i);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B4CB File Offset: 0x000096CB
		public virtual T GetFieldValue<T>(int i)
		{
			return this._sqlDataReader.GetFieldValue<T>(i);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B4D9 File Offset: 0x000096D9
		public virtual object GetValue(int i)
		{
			return this._sqlDataReader.GetValue(i);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B4E7 File Offset: 0x000096E7
		public virtual TimeSpan GetTimeSpan(int i)
		{
			return this._sqlDataReader.GetTimeSpan(i);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B4F5 File Offset: 0x000096F5
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			return this._sqlDataReader.GetDateTimeOffset(i);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B503 File Offset: 0x00009703
		public virtual int GetValues(object[] values)
		{
			return this._sqlDataReader.GetValues(values);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B511 File Offset: 0x00009711
		public virtual bool IsDBNull(int i)
		{
			return this._sqlDataReader.IsDBNull(i);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B51F File Offset: 0x0000971F
		public virtual bool NextResult()
		{
			return this._sqlDataReader.NextResult();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B52C File Offset: 0x0000972C
		public virtual bool Read()
		{
			return this._sqlDataReader.Read();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B539 File Offset: 0x00009739
		public virtual Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return this._sqlDataReader.NextResultAsync(cancellationToken);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B54E File Offset: 0x0000974E
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return this._sqlDataReader.ReadAsync(cancellationToken);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B563 File Offset: 0x00009763
		public virtual Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
		{
			return this._sqlDataReader.IsDBNullAsync(i, cancellationToken);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B572 File Offset: 0x00009772
		public virtual Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
		{
			return this._sqlDataReader.GetFieldValueAsync<T>(i, cancellationToken);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B581 File Offset: 0x00009781
		public virtual int Depth
		{
			get
			{
				return this._sqlDataReader.Depth;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B58E File Offset: 0x0000978E
		public virtual int FieldCount
		{
			get
			{
				return this._sqlDataReader.FieldCount;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B59B File Offset: 0x0000979B
		public virtual bool HasRows
		{
			get
			{
				return this._sqlDataReader.HasRows;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B5A8 File Offset: 0x000097A8
		public virtual bool IsClosed
		{
			get
			{
				return this._sqlDataReader.IsClosed;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B5B5 File Offset: 0x000097B5
		public virtual int RecordsAffected
		{
			get
			{
				return this._sqlDataReader.RecordsAffected;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B5C2 File Offset: 0x000097C2
		public virtual int VisibleFieldCount
		{
			get
			{
				return this._sqlDataReader.VisibleFieldCount;
			}
		}

		// Token: 0x17000044 RID: 68
		public virtual object this[int i]
		{
			get
			{
				return this._sqlDataReader[i];
			}
		}

		// Token: 0x17000045 RID: 69
		public virtual object this[string name]
		{
			get
			{
				return this._sqlDataReader[name];
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly SqlDataReader _sqlDataReader;
	}
}

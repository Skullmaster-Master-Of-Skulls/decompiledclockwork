using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000030 RID: 48
	internal sealed class MemoryRecordBuffer : SmiRecordBuffer
	{
		// Token: 0x06000174 RID: 372 RVA: 0x001DAE38 File Offset: 0x001DA238
		internal MemoryRecordBuffer(SmiMetaData[] metaData)
		{
			this._buffer = new SqlRecordBuffer[metaData.Length];
			for (int i = 0; i < this._buffer.Length; i++)
			{
				this._buffer[i] = new SqlRecordBuffer(metaData[i]);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x001DAE88 File Offset: 0x001DA288
		public override bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].IsNull;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x001DAEA8 File Offset: 0x001DA2A8
		public override SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].VariantType;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x001DAEC8 File Offset: 0x001DA2C8
		public override bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Boolean;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x001DAEE8 File Offset: 0x001DA2E8
		public override byte GetByte(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Byte;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x001DAF08 File Offset: 0x001DA308
		public override long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].BytesLength;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x001DAF28 File Offset: 0x001DA328
		public override int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x001DAF58 File Offset: 0x001DA358
		public override long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].CharsLength;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x001DAF78 File Offset: 0x001DA378
		public override int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x001DAFA8 File Offset: 0x001DA3A8
		public override string GetString(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].String;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x001DAFC8 File Offset: 0x001DA3C8
		public override short GetInt16(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int16;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x001DAFE8 File Offset: 0x001DA3E8
		public override int GetInt32(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int32;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x001DB008 File Offset: 0x001DA408
		public override long GetInt64(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int64;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x001DB028 File Offset: 0x001DA428
		public override float GetSingle(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Single;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x001DB048 File Offset: 0x001DA448
		public override double GetDouble(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Double;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x001DB068 File Offset: 0x001DA468
		public override SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].SqlDecimal;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x001DB088 File Offset: 0x001DA488
		public override DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTime;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x001DB0A8 File Offset: 0x001DA4A8
		public override Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Guid;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x001DB0C8 File Offset: 0x001DA4C8
		public override TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].TimeSpan;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x001DB0E8 File Offset: 0x001DA4E8
		public override DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTimeOffset;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x001DB108 File Offset: 0x001DA508
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._buffer[ordinal].SetNull();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x001DB128 File Offset: 0x001DA528
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._buffer[ordinal].Boolean = value;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x001DB148 File Offset: 0x001DA548
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._buffer[ordinal].Byte = value;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x001DB168 File Offset: 0x001DA568
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x001DB198 File Offset: 0x001DA598
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].BytesLength = length;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x001DB1B8 File Offset: 0x001DA5B8
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x001DB1E8 File Offset: 0x001DA5E8
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].CharsLength = length;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x001DB208 File Offset: 0x001DA608
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._buffer[ordinal].String = value.Substring(offset, length);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x001DB238 File Offset: 0x001DA638
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._buffer[ordinal].Int16 = value;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x001DB258 File Offset: 0x001DA658
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._buffer[ordinal].Int32 = value;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x001DB278 File Offset: 0x001DA678
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._buffer[ordinal].Int64 = value;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x001DB298 File Offset: 0x001DA698
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._buffer[ordinal].Single = value;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x001DB2B8 File Offset: 0x001DA6B8
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._buffer[ordinal].Double = value;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x001DB2D8 File Offset: 0x001DA6D8
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._buffer[ordinal].SqlDecimal = value;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x001DB2F8 File Offset: 0x001DA6F8
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._buffer[ordinal].DateTime = value;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x001DB318 File Offset: 0x001DA718
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._buffer[ordinal].Guid = value;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x001DB338 File Offset: 0x001DA738
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._buffer[ordinal].TimeSpan = value;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x001DB358 File Offset: 0x001DA758
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._buffer[ordinal].DateTimeOffset = value;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x001DB378 File Offset: 0x001DA778
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._buffer[ordinal].VariantType = metaData;
		}

		// Token: 0x04000571 RID: 1393
		private SqlRecordBuffer[] _buffer;
	}
}

using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000076 RID: 118
	internal sealed class MemoryRecordBuffer : SmiRecordBuffer
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00047B24 File Offset: 0x00046F24
		internal MemoryRecordBuffer(SmiMetaData[] metaData)
		{
			this._buffer = new SqlRecordBuffer[metaData.Length];
			for (int i = 0; i < this._buffer.Length; i++)
			{
				this._buffer[i] = new SqlRecordBuffer(metaData[i]);
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00047B68 File Offset: 0x00046F68
		public override bool IsDBNull(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].IsNull;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00047B84 File Offset: 0x00046F84
		public override SmiMetaData GetVariantType(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].VariantType;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00047BA0 File Offset: 0x00046FA0
		public override bool GetBoolean(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Boolean;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00047BBC File Offset: 0x00046FBC
		public override byte GetByte(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Byte;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00047BD8 File Offset: 0x00046FD8
		public override long GetBytesLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].BytesLength;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00047BF4 File Offset: 0x00046FF4
		public override int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00047C18 File Offset: 0x00047018
		public override long GetCharsLength(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].CharsLength;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00047C34 File Offset: 0x00047034
		public override int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].GetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00047C58 File Offset: 0x00047058
		public override string GetString(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].String;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00047C74 File Offset: 0x00047074
		public override short GetInt16(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int16;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00047C90 File Offset: 0x00047090
		public override int GetInt32(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int32;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00047CAC File Offset: 0x000470AC
		public override long GetInt64(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Int64;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00047CC8 File Offset: 0x000470C8
		public override float GetSingle(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Single;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00047CE4 File Offset: 0x000470E4
		public override double GetDouble(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Double;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00047D00 File Offset: 0x00047100
		public override SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].SqlDecimal;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00047D1C File Offset: 0x0004711C
		public override DateTime GetDateTime(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTime;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00047D38 File Offset: 0x00047138
		public override Guid GetGuid(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].Guid;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00047D54 File Offset: 0x00047154
		public override TimeSpan GetTimeSpan(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].TimeSpan;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00047D70 File Offset: 0x00047170
		public override DateTimeOffset GetDateTimeOffset(SmiEventSink sink, int ordinal)
		{
			return this._buffer[ordinal].DateTimeOffset;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00047D8C File Offset: 0x0004718C
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._buffer[ordinal].SetNull();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00047DA8 File Offset: 0x000471A8
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._buffer[ordinal].Boolean = value;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00047DC4 File Offset: 0x000471C4
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._buffer[ordinal].Byte = value;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00047DE0 File Offset: 0x000471E0
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00047E04 File Offset: 0x00047204
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].BytesLength = length;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00047E20 File Offset: 0x00047220
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._buffer[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00047E44 File Offset: 0x00047244
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._buffer[ordinal].CharsLength = length;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00047E60 File Offset: 0x00047260
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._buffer[ordinal].String = value.Substring(offset, length);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00047E84 File Offset: 0x00047284
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._buffer[ordinal].Int16 = value;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00047EA0 File Offset: 0x000472A0
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._buffer[ordinal].Int32 = value;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00047EBC File Offset: 0x000472BC
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._buffer[ordinal].Int64 = value;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00047ED8 File Offset: 0x000472D8
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._buffer[ordinal].Single = value;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00047EF4 File Offset: 0x000472F4
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._buffer[ordinal].Double = value;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00047F10 File Offset: 0x00047310
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._buffer[ordinal].SqlDecimal = value;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00047F2C File Offset: 0x0004732C
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._buffer[ordinal].DateTime = value;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00047F48 File Offset: 0x00047348
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._buffer[ordinal].Guid = value;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00047F64 File Offset: 0x00047364
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._buffer[ordinal].TimeSpan = value;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00047F80 File Offset: 0x00047380
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._buffer[ordinal].DateTimeOffset = value;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00047F9C File Offset: 0x0004739C
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._buffer[ordinal].VariantType = metaData;
		}

		// Token: 0x04000251 RID: 593
		private SqlRecordBuffer[] _buffer;
	}
}

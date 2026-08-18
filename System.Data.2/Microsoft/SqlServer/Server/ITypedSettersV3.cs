using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000037 RID: 55
	internal interface ITypedSettersV3
	{
		// Token: 0x06000186 RID: 390
		void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData);

		// Token: 0x06000187 RID: 391
		void SetDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06000188 RID: 392
		void SetBoolean(SmiEventSink sink, int ordinal, bool value);

		// Token: 0x06000189 RID: 393
		void SetByte(SmiEventSink sink, int ordinal, byte value);

		// Token: 0x0600018A RID: 394
		int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x0600018B RID: 395
		void SetBytesLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x0600018C RID: 396
		int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x0600018D RID: 397
		void SetCharsLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x0600018E RID: 398
		void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length);

		// Token: 0x0600018F RID: 399
		void SetInt16(SmiEventSink sink, int ordinal, short value);

		// Token: 0x06000190 RID: 400
		void SetInt32(SmiEventSink sink, int ordinal, int value);

		// Token: 0x06000191 RID: 401
		void SetInt64(SmiEventSink sink, int ordinal, long value);

		// Token: 0x06000192 RID: 402
		void SetSingle(SmiEventSink sink, int ordinal, float value);

		// Token: 0x06000193 RID: 403
		void SetDouble(SmiEventSink sink, int ordinal, double value);

		// Token: 0x06000194 RID: 404
		void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value);

		// Token: 0x06000195 RID: 405
		void SetDateTime(SmiEventSink sink, int ordinal, DateTime value);

		// Token: 0x06000196 RID: 406
		void SetGuid(SmiEventSink sink, int ordinal, Guid value);
	}
}

using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000035 RID: 53
	internal interface ITypedGettersV3
	{
		// Token: 0x06000151 RID: 337
		bool IsDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x06000152 RID: 338
		SmiMetaData GetVariantType(SmiEventSink sink, int ordinal);

		// Token: 0x06000153 RID: 339
		bool GetBoolean(SmiEventSink sink, int ordinal);

		// Token: 0x06000154 RID: 340
		byte GetByte(SmiEventSink sink, int ordinal);

		// Token: 0x06000155 RID: 341
		long GetBytesLength(SmiEventSink sink, int ordinal);

		// Token: 0x06000156 RID: 342
		int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000157 RID: 343
		long GetCharsLength(SmiEventSink sink, int ordinal);

		// Token: 0x06000158 RID: 344
		int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000159 RID: 345
		string GetString(SmiEventSink sink, int ordinal);

		// Token: 0x0600015A RID: 346
		short GetInt16(SmiEventSink sink, int ordinal);

		// Token: 0x0600015B RID: 347
		int GetInt32(SmiEventSink sink, int ordinal);

		// Token: 0x0600015C RID: 348
		long GetInt64(SmiEventSink sink, int ordinal);

		// Token: 0x0600015D RID: 349
		float GetSingle(SmiEventSink sink, int ordinal);

		// Token: 0x0600015E RID: 350
		double GetDouble(SmiEventSink sink, int ordinal);

		// Token: 0x0600015F RID: 351
		SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal);

		// Token: 0x06000160 RID: 352
		DateTime GetDateTime(SmiEventSink sink, int ordinal);

		// Token: 0x06000161 RID: 353
		Guid GetGuid(SmiEventSink sink, int ordinal);
	}
}

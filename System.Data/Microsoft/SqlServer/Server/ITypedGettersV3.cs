using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002B RID: 43
	internal interface ITypedGettersV3
	{
		// Token: 0x060000B5 RID: 181
		bool IsDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x060000B6 RID: 182
		SmiMetaData GetVariantType(SmiEventSink sink, int ordinal);

		// Token: 0x060000B7 RID: 183
		bool GetBoolean(SmiEventSink sink, int ordinal);

		// Token: 0x060000B8 RID: 184
		byte GetByte(SmiEventSink sink, int ordinal);

		// Token: 0x060000B9 RID: 185
		long GetBytesLength(SmiEventSink sink, int ordinal);

		// Token: 0x060000BA RID: 186
		int GetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x060000BB RID: 187
		long GetCharsLength(SmiEventSink sink, int ordinal);

		// Token: 0x060000BC RID: 188
		int GetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x060000BD RID: 189
		string GetString(SmiEventSink sink, int ordinal);

		// Token: 0x060000BE RID: 190
		short GetInt16(SmiEventSink sink, int ordinal);

		// Token: 0x060000BF RID: 191
		int GetInt32(SmiEventSink sink, int ordinal);

		// Token: 0x060000C0 RID: 192
		long GetInt64(SmiEventSink sink, int ordinal);

		// Token: 0x060000C1 RID: 193
		float GetSingle(SmiEventSink sink, int ordinal);

		// Token: 0x060000C2 RID: 194
		double GetDouble(SmiEventSink sink, int ordinal);

		// Token: 0x060000C3 RID: 195
		SqlDecimal GetSqlDecimal(SmiEventSink sink, int ordinal);

		// Token: 0x060000C4 RID: 196
		DateTime GetDateTime(SmiEventSink sink, int ordinal);

		// Token: 0x060000C5 RID: 197
		Guid GetGuid(SmiEventSink sink, int ordinal);
	}
}

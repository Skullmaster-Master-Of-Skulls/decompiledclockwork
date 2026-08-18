using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002D RID: 45
	internal interface ITypedSettersV3
	{
		// Token: 0x060000EA RID: 234
		void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData);

		// Token: 0x060000EB RID: 235
		void SetDBNull(SmiEventSink sink, int ordinal);

		// Token: 0x060000EC RID: 236
		void SetBoolean(SmiEventSink sink, int ordinal, bool value);

		// Token: 0x060000ED RID: 237
		void SetByte(SmiEventSink sink, int ordinal, byte value);

		// Token: 0x060000EE RID: 238
		int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x060000EF RID: 239
		void SetBytesLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x060000F0 RID: 240
		int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x060000F1 RID: 241
		void SetCharsLength(SmiEventSink sink, int ordinal, long length);

		// Token: 0x060000F2 RID: 242
		void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length);

		// Token: 0x060000F3 RID: 243
		void SetInt16(SmiEventSink sink, int ordinal, short value);

		// Token: 0x060000F4 RID: 244
		void SetInt32(SmiEventSink sink, int ordinal, int value);

		// Token: 0x060000F5 RID: 245
		void SetInt64(SmiEventSink sink, int ordinal, long value);

		// Token: 0x060000F6 RID: 246
		void SetSingle(SmiEventSink sink, int ordinal, float value);

		// Token: 0x060000F7 RID: 247
		void SetDouble(SmiEventSink sink, int ordinal, double value);

		// Token: 0x060000F8 RID: 248
		void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value);

		// Token: 0x060000F9 RID: 249
		void SetDateTime(SmiEventSink sink, int ordinal, DateTime value);

		// Token: 0x060000FA RID: 250
		void SetGuid(SmiEventSink sink, int ordinal, Guid value);
	}
}

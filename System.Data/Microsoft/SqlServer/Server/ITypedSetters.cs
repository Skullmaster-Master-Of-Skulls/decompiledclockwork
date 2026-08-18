using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002C RID: 44
	internal interface ITypedSetters
	{
		// Token: 0x060000C6 RID: 198
		void SetDBNull(int ordinal);

		// Token: 0x060000C7 RID: 199
		void SetBoolean(int ordinal, bool value);

		// Token: 0x060000C8 RID: 200
		void SetByte(int ordinal, byte value);

		// Token: 0x060000C9 RID: 201
		void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x060000CA RID: 202
		void SetChar(int ordinal, char value);

		// Token: 0x060000CB RID: 203
		void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x060000CC RID: 204
		void SetInt16(int ordinal, short value);

		// Token: 0x060000CD RID: 205
		void SetInt32(int ordinal, int value);

		// Token: 0x060000CE RID: 206
		void SetInt64(int ordinal, long value);

		// Token: 0x060000CF RID: 207
		void SetFloat(int ordinal, float value);

		// Token: 0x060000D0 RID: 208
		void SetDouble(int ordinal, double value);

		// Token: 0x060000D1 RID: 209
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetString(int ordinal, string value);

		// Token: 0x060000D2 RID: 210
		void SetString(int ordinal, string value, int offset);

		// Token: 0x060000D3 RID: 211
		void SetDecimal(int ordinal, decimal value);

		// Token: 0x060000D4 RID: 212
		void SetDateTime(int ordinal, DateTime value);

		// Token: 0x060000D5 RID: 213
		void SetGuid(int ordinal, Guid value);

		// Token: 0x060000D6 RID: 214
		void SetSqlBoolean(int ordinal, SqlBoolean value);

		// Token: 0x060000D7 RID: 215
		void SetSqlByte(int ordinal, SqlByte value);

		// Token: 0x060000D8 RID: 216
		void SetSqlInt16(int ordinal, SqlInt16 value);

		// Token: 0x060000D9 RID: 217
		void SetSqlInt32(int ordinal, SqlInt32 value);

		// Token: 0x060000DA RID: 218
		void SetSqlInt64(int ordinal, SqlInt64 value);

		// Token: 0x060000DB RID: 219
		void SetSqlSingle(int ordinal, SqlSingle value);

		// Token: 0x060000DC RID: 220
		void SetSqlDouble(int ordinal, SqlDouble value);

		// Token: 0x060000DD RID: 221
		void SetSqlMoney(int ordinal, SqlMoney value);

		// Token: 0x060000DE RID: 222
		void SetSqlDateTime(int ordinal, SqlDateTime value);

		// Token: 0x060000DF RID: 223
		void SetSqlDecimal(int ordinal, SqlDecimal value);

		// Token: 0x060000E0 RID: 224
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlString(int ordinal, SqlString value);

		// Token: 0x060000E1 RID: 225
		void SetSqlString(int ordinal, SqlString value, int offset);

		// Token: 0x060000E2 RID: 226
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBinary(int ordinal, SqlBinary value);

		// Token: 0x060000E3 RID: 227
		void SetSqlBinary(int ordinal, SqlBinary value, int offset);

		// Token: 0x060000E4 RID: 228
		void SetSqlGuid(int ordinal, SqlGuid value);

		// Token: 0x060000E5 RID: 229
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlChars(int ordinal, SqlChars value);

		// Token: 0x060000E6 RID: 230
		void SetSqlChars(int ordinal, SqlChars value, int offset);

		// Token: 0x060000E7 RID: 231
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBytes(int ordinal, SqlBytes value);

		// Token: 0x060000E8 RID: 232
		void SetSqlBytes(int ordinal, SqlBytes value, int offset);

		// Token: 0x060000E9 RID: 233
		void SetSqlXml(int ordinal, SqlXml value);
	}
}

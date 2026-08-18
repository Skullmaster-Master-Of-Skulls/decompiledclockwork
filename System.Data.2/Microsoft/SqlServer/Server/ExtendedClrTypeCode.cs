using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000033 RID: 51
	internal enum ExtendedClrTypeCode
	{
		// Token: 0x040000C5 RID: 197
		Invalid = -1,
		// Token: 0x040000C6 RID: 198
		Boolean,
		// Token: 0x040000C7 RID: 199
		Byte,
		// Token: 0x040000C8 RID: 200
		Char,
		// Token: 0x040000C9 RID: 201
		DateTime,
		// Token: 0x040000CA RID: 202
		DBNull,
		// Token: 0x040000CB RID: 203
		Decimal,
		// Token: 0x040000CC RID: 204
		Double,
		// Token: 0x040000CD RID: 205
		Empty,
		// Token: 0x040000CE RID: 206
		Int16,
		// Token: 0x040000CF RID: 207
		Int32,
		// Token: 0x040000D0 RID: 208
		Int64,
		// Token: 0x040000D1 RID: 209
		SByte,
		// Token: 0x040000D2 RID: 210
		Single,
		// Token: 0x040000D3 RID: 211
		String,
		// Token: 0x040000D4 RID: 212
		UInt16,
		// Token: 0x040000D5 RID: 213
		UInt32,
		// Token: 0x040000D6 RID: 214
		UInt64,
		// Token: 0x040000D7 RID: 215
		Object,
		// Token: 0x040000D8 RID: 216
		ByteArray,
		// Token: 0x040000D9 RID: 217
		CharArray,
		// Token: 0x040000DA RID: 218
		Guid,
		// Token: 0x040000DB RID: 219
		SqlBinary,
		// Token: 0x040000DC RID: 220
		SqlBoolean,
		// Token: 0x040000DD RID: 221
		SqlByte,
		// Token: 0x040000DE RID: 222
		SqlDateTime,
		// Token: 0x040000DF RID: 223
		SqlDouble,
		// Token: 0x040000E0 RID: 224
		SqlGuid,
		// Token: 0x040000E1 RID: 225
		SqlInt16,
		// Token: 0x040000E2 RID: 226
		SqlInt32,
		// Token: 0x040000E3 RID: 227
		SqlInt64,
		// Token: 0x040000E4 RID: 228
		SqlMoney,
		// Token: 0x040000E5 RID: 229
		SqlDecimal,
		// Token: 0x040000E6 RID: 230
		SqlSingle,
		// Token: 0x040000E7 RID: 231
		SqlString,
		// Token: 0x040000E8 RID: 232
		SqlChars,
		// Token: 0x040000E9 RID: 233
		SqlBytes,
		// Token: 0x040000EA RID: 234
		SqlXml,
		// Token: 0x040000EB RID: 235
		DataTable,
		// Token: 0x040000EC RID: 236
		DbDataReader,
		// Token: 0x040000ED RID: 237
		IEnumerableOfSqlDataRecord,
		// Token: 0x040000EE RID: 238
		TimeSpan,
		// Token: 0x040000EF RID: 239
		DateTimeOffset,
		// Token: 0x040000F0 RID: 240
		Stream,
		// Token: 0x040000F1 RID: 241
		TextReader,
		// Token: 0x040000F2 RID: 242
		XmlReader,
		// Token: 0x040000F3 RID: 243
		Last = 44,
		// Token: 0x040000F4 RID: 244
		First = 0
	}
}

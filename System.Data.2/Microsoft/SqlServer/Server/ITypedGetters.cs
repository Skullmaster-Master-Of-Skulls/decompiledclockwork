using System;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000034 RID: 52
	internal interface ITypedGetters
	{
		// Token: 0x0600012E RID: 302
		bool IsDBNull(int ordinal);

		// Token: 0x0600012F RID: 303
		SqlDbType GetVariantType(int ordinal);

		// Token: 0x06000130 RID: 304
		bool GetBoolean(int ordinal);

		// Token: 0x06000131 RID: 305
		byte GetByte(int ordinal);

		// Token: 0x06000132 RID: 306
		long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000133 RID: 307
		char GetChar(int ordinal);

		// Token: 0x06000134 RID: 308
		long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000135 RID: 309
		short GetInt16(int ordinal);

		// Token: 0x06000136 RID: 310
		int GetInt32(int ordinal);

		// Token: 0x06000137 RID: 311
		long GetInt64(int ordinal);

		// Token: 0x06000138 RID: 312
		float GetFloat(int ordinal);

		// Token: 0x06000139 RID: 313
		double GetDouble(int ordinal);

		// Token: 0x0600013A RID: 314
		string GetString(int ordinal);

		// Token: 0x0600013B RID: 315
		decimal GetDecimal(int ordinal);

		// Token: 0x0600013C RID: 316
		DateTime GetDateTime(int ordinal);

		// Token: 0x0600013D RID: 317
		Guid GetGuid(int ordinal);

		// Token: 0x0600013E RID: 318
		SqlBoolean GetSqlBoolean(int ordinal);

		// Token: 0x0600013F RID: 319
		SqlByte GetSqlByte(int ordinal);

		// Token: 0x06000140 RID: 320
		SqlInt16 GetSqlInt16(int ordinal);

		// Token: 0x06000141 RID: 321
		SqlInt32 GetSqlInt32(int ordinal);

		// Token: 0x06000142 RID: 322
		SqlInt64 GetSqlInt64(int ordinal);

		// Token: 0x06000143 RID: 323
		SqlSingle GetSqlSingle(int ordinal);

		// Token: 0x06000144 RID: 324
		SqlDouble GetSqlDouble(int ordinal);

		// Token: 0x06000145 RID: 325
		SqlMoney GetSqlMoney(int ordinal);

		// Token: 0x06000146 RID: 326
		SqlDateTime GetSqlDateTime(int ordinal);

		// Token: 0x06000147 RID: 327
		SqlDecimal GetSqlDecimal(int ordinal);

		// Token: 0x06000148 RID: 328
		SqlString GetSqlString(int ordinal);

		// Token: 0x06000149 RID: 329
		SqlBinary GetSqlBinary(int ordinal);

		// Token: 0x0600014A RID: 330
		SqlGuid GetSqlGuid(int ordinal);

		// Token: 0x0600014B RID: 331
		SqlChars GetSqlChars(int ordinal);

		// Token: 0x0600014C RID: 332
		SqlBytes GetSqlBytes(int ordinal);

		// Token: 0x0600014D RID: 333
		SqlXml GetSqlXml(int ordinal);

		// Token: 0x0600014E RID: 334
		SqlBytes GetSqlBytesRef(int ordinal);

		// Token: 0x0600014F RID: 335
		SqlChars GetSqlCharsRef(int ordinal);

		// Token: 0x06000150 RID: 336
		SqlXml GetSqlXmlRef(int ordinal);
	}
}

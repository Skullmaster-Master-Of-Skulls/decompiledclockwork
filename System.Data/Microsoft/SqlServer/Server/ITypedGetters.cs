using System;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002A RID: 42
	internal interface ITypedGetters
	{
		// Token: 0x06000092 RID: 146
		bool IsDBNull(int ordinal);

		// Token: 0x06000093 RID: 147
		SqlDbType GetVariantType(int ordinal);

		// Token: 0x06000094 RID: 148
		bool GetBoolean(int ordinal);

		// Token: 0x06000095 RID: 149
		byte GetByte(int ordinal);

		// Token: 0x06000096 RID: 150
		long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000097 RID: 151
		char GetChar(int ordinal);

		// Token: 0x06000098 RID: 152
		long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000099 RID: 153
		short GetInt16(int ordinal);

		// Token: 0x0600009A RID: 154
		int GetInt32(int ordinal);

		// Token: 0x0600009B RID: 155
		long GetInt64(int ordinal);

		// Token: 0x0600009C RID: 156
		float GetFloat(int ordinal);

		// Token: 0x0600009D RID: 157
		double GetDouble(int ordinal);

		// Token: 0x0600009E RID: 158
		string GetString(int ordinal);

		// Token: 0x0600009F RID: 159
		decimal GetDecimal(int ordinal);

		// Token: 0x060000A0 RID: 160
		DateTime GetDateTime(int ordinal);

		// Token: 0x060000A1 RID: 161
		Guid GetGuid(int ordinal);

		// Token: 0x060000A2 RID: 162
		SqlBoolean GetSqlBoolean(int ordinal);

		// Token: 0x060000A3 RID: 163
		SqlByte GetSqlByte(int ordinal);

		// Token: 0x060000A4 RID: 164
		SqlInt16 GetSqlInt16(int ordinal);

		// Token: 0x060000A5 RID: 165
		SqlInt32 GetSqlInt32(int ordinal);

		// Token: 0x060000A6 RID: 166
		SqlInt64 GetSqlInt64(int ordinal);

		// Token: 0x060000A7 RID: 167
		SqlSingle GetSqlSingle(int ordinal);

		// Token: 0x060000A8 RID: 168
		SqlDouble GetSqlDouble(int ordinal);

		// Token: 0x060000A9 RID: 169
		SqlMoney GetSqlMoney(int ordinal);

		// Token: 0x060000AA RID: 170
		SqlDateTime GetSqlDateTime(int ordinal);

		// Token: 0x060000AB RID: 171
		SqlDecimal GetSqlDecimal(int ordinal);

		// Token: 0x060000AC RID: 172
		SqlString GetSqlString(int ordinal);

		// Token: 0x060000AD RID: 173
		SqlBinary GetSqlBinary(int ordinal);

		// Token: 0x060000AE RID: 174
		SqlGuid GetSqlGuid(int ordinal);

		// Token: 0x060000AF RID: 175
		SqlChars GetSqlChars(int ordinal);

		// Token: 0x060000B0 RID: 176
		SqlBytes GetSqlBytes(int ordinal);

		// Token: 0x060000B1 RID: 177
		SqlXml GetSqlXml(int ordinal);

		// Token: 0x060000B2 RID: 178
		SqlBytes GetSqlBytesRef(int ordinal);

		// Token: 0x060000B3 RID: 179
		SqlChars GetSqlCharsRef(int ordinal);

		// Token: 0x060000B4 RID: 180
		SqlXml GetSqlXmlRef(int ordinal);
	}
}

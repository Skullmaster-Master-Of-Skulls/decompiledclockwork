using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000036 RID: 54
	internal interface ITypedSetters
	{
		// Token: 0x06000162 RID: 354
		void SetDBNull(int ordinal);

		// Token: 0x06000163 RID: 355
		void SetBoolean(int ordinal, bool value);

		// Token: 0x06000164 RID: 356
		void SetByte(int ordinal, byte value);

		// Token: 0x06000165 RID: 357
		void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		// Token: 0x06000166 RID: 358
		void SetChar(int ordinal, char value);

		// Token: 0x06000167 RID: 359
		void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length);

		// Token: 0x06000168 RID: 360
		void SetInt16(int ordinal, short value);

		// Token: 0x06000169 RID: 361
		void SetInt32(int ordinal, int value);

		// Token: 0x0600016A RID: 362
		void SetInt64(int ordinal, long value);

		// Token: 0x0600016B RID: 363
		void SetFloat(int ordinal, float value);

		// Token: 0x0600016C RID: 364
		void SetDouble(int ordinal, double value);

		// Token: 0x0600016D RID: 365
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetString(int ordinal, string value);

		// Token: 0x0600016E RID: 366
		void SetString(int ordinal, string value, int offset);

		// Token: 0x0600016F RID: 367
		void SetDecimal(int ordinal, decimal value);

		// Token: 0x06000170 RID: 368
		void SetDateTime(int ordinal, DateTime value);

		// Token: 0x06000171 RID: 369
		void SetGuid(int ordinal, Guid value);

		// Token: 0x06000172 RID: 370
		void SetSqlBoolean(int ordinal, SqlBoolean value);

		// Token: 0x06000173 RID: 371
		void SetSqlByte(int ordinal, SqlByte value);

		// Token: 0x06000174 RID: 372
		void SetSqlInt16(int ordinal, SqlInt16 value);

		// Token: 0x06000175 RID: 373
		void SetSqlInt32(int ordinal, SqlInt32 value);

		// Token: 0x06000176 RID: 374
		void SetSqlInt64(int ordinal, SqlInt64 value);

		// Token: 0x06000177 RID: 375
		void SetSqlSingle(int ordinal, SqlSingle value);

		// Token: 0x06000178 RID: 376
		void SetSqlDouble(int ordinal, SqlDouble value);

		// Token: 0x06000179 RID: 377
		void SetSqlMoney(int ordinal, SqlMoney value);

		// Token: 0x0600017A RID: 378
		void SetSqlDateTime(int ordinal, SqlDateTime value);

		// Token: 0x0600017B RID: 379
		void SetSqlDecimal(int ordinal, SqlDecimal value);

		// Token: 0x0600017C RID: 380
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlString(int ordinal, SqlString value);

		// Token: 0x0600017D RID: 381
		void SetSqlString(int ordinal, SqlString value, int offset);

		// Token: 0x0600017E RID: 382
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBinary(int ordinal, SqlBinary value);

		// Token: 0x0600017F RID: 383
		void SetSqlBinary(int ordinal, SqlBinary value, int offset);

		// Token: 0x06000180 RID: 384
		void SetSqlGuid(int ordinal, SqlGuid value);

		// Token: 0x06000181 RID: 385
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlChars(int ordinal, SqlChars value);

		// Token: 0x06000182 RID: 386
		void SetSqlChars(int ordinal, SqlChars value, int offset);

		// Token: 0x06000183 RID: 387
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped.  Use setter with offset.")]
		void SetSqlBytes(int ordinal, SqlBytes value);

		// Token: 0x06000184 RID: 388
		void SetSqlBytes(int ordinal, SqlBytes value, int offset);

		// Token: 0x06000185 RID: 389
		void SetSqlXml(int ordinal, SqlXml value);
	}
}

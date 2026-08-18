using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200002F RID: 47
	internal abstract class SmiRecordBuffer : SmiTypedGetterSetter, ITypedGettersV3, ITypedSettersV3, ITypedGetters, ITypedSetters, IDisposable
	{
		// Token: 0x06000128 RID: 296 RVA: 0x001DA4D8 File Offset: 0x001D98D8
		public virtual void Close(SmiEventSink eventSink)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000129 RID: 297 RVA: 0x001DA4F8 File Offset: 0x001D98F8
		internal override bool CanGet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600012A RID: 298 RVA: 0x001DA508 File Offset: 0x001D9908
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x001DA518 File Offset: 0x001D9918
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x001DA538 File Offset: 0x001D9938
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x001DA558 File Offset: 0x001D9958
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x001DA578 File Offset: 0x001D9978
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x001DA598 File Offset: 0x001D9998
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x001DA5B8 File Offset: 0x001D99B8
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x001DA5D8 File Offset: 0x001D99D8
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x001DA5F8 File Offset: 0x001D99F8
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x001DA618 File Offset: 0x001D9A18
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x001DA638 File Offset: 0x001D9A38
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x001DA658 File Offset: 0x001D9A58
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x001DA678 File Offset: 0x001D9A78
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x001DA698 File Offset: 0x001D9A98
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x001DA6B8 File Offset: 0x001D9AB8
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x001DA6D8 File Offset: 0x001D9AD8
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x001DA6F8 File Offset: 0x001D9AF8
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x001DA718 File Offset: 0x001D9B18
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x001DA738 File Offset: 0x001D9B38
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x001DA758 File Offset: 0x001D9B58
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x001DA778 File Offset: 0x001D9B78
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x001DA798 File Offset: 0x001D9B98
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x001DA7B8 File Offset: 0x001D9BB8
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x001DA7D8 File Offset: 0x001D9BD8
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x001DA7F8 File Offset: 0x001D9BF8
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x001DA818 File Offset: 0x001D9C18
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x001DA838 File Offset: 0x001D9C38
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x001DA858 File Offset: 0x001D9C58
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x001DA878 File Offset: 0x001D9C78
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x001DA898 File Offset: 0x001D9C98
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x001DA8B8 File Offset: 0x001D9CB8
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x001DA8D8 File Offset: 0x001D9CD8
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x001DA8F8 File Offset: 0x001D9CF8
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x001DA918 File Offset: 0x001D9D18
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x001DA938 File Offset: 0x001D9D38
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x001DA958 File Offset: 0x001D9D58
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x001DA978 File Offset: 0x001D9D78
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x001DA998 File Offset: 0x001D9D98
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x001DA9B8 File Offset: 0x001D9DB8
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x001DA9D8 File Offset: 0x001D9DD8
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x001DA9F8 File Offset: 0x001D9DF8
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x001DAA18 File Offset: 0x001D9E18
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x001DAA38 File Offset: 0x001D9E38
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x001DAA58 File Offset: 0x001D9E58
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x001DAA78 File Offset: 0x001D9E78
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x001DAA98 File Offset: 0x001D9E98
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x001DAAB8 File Offset: 0x001D9EB8
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x001DAAD8 File Offset: 0x001D9ED8
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x001DAAF8 File Offset: 0x001D9EF8
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x001DAB18 File Offset: 0x001D9F18
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x001DAB38 File Offset: 0x001D9F38
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x001DAB58 File Offset: 0x001D9F58
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x001DAB78 File Offset: 0x001D9F78
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x001DAB98 File Offset: 0x001D9F98
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x001DABB8 File Offset: 0x001D9FB8
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x001DABD8 File Offset: 0x001D9FD8
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x001DABF8 File Offset: 0x001D9FF8
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x001DAC18 File Offset: 0x001DA018
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x001DAC38 File Offset: 0x001DA038
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x001DAC58 File Offset: 0x001DA058
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x001DAC78 File Offset: 0x001DA078
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x001DAC98 File Offset: 0x001DA098
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x001DACB8 File Offset: 0x001DA0B8
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x001DACD8 File Offset: 0x001DA0D8
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x001DACF8 File Offset: 0x001DA0F8
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x001DAD18 File Offset: 0x001DA118
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x001DAD38 File Offset: 0x001DA138
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x001DAD58 File Offset: 0x001DA158
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x001DAD78 File Offset: 0x001DA178
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x001DAD98 File Offset: 0x001DA198
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x001DADB8 File Offset: 0x001DA1B8
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x001DADD8 File Offset: 0x001DA1D8
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x001DADF8 File Offset: 0x001DA1F8
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}

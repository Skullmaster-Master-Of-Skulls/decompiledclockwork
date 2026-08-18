using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000048 RID: 72
	internal abstract class SmiRecordBuffer : SmiTypedGetterSetter, ITypedGettersV3, ITypedSettersV3, ITypedGetters, ITypedSetters, IDisposable
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0003BCC4 File Offset: 0x0003B0C4
		public virtual void Close(SmiEventSink eventSink)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0003BCD8 File Offset: 0x0003B0D8
		internal override bool CanGet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0003BCE8 File Offset: 0x0003B0E8
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0003BCF8 File Offset: 0x0003B0F8
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0003BD0C File Offset: 0x0003B10C
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0003BD20 File Offset: 0x0003B120
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0003BD34 File Offset: 0x0003B134
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0003BD48 File Offset: 0x0003B148
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0003BD5C File Offset: 0x0003B15C
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0003BD70 File Offset: 0x0003B170
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0003BD84 File Offset: 0x0003B184
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0003BD98 File Offset: 0x0003B198
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0003BDAC File Offset: 0x0003B1AC
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0003BDC0 File Offset: 0x0003B1C0
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0003BDD4 File Offset: 0x0003B1D4
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0003BDE8 File Offset: 0x0003B1E8
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0003BDFC File Offset: 0x0003B1FC
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0003BE10 File Offset: 0x0003B210
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0003BE24 File Offset: 0x0003B224
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0003BE38 File Offset: 0x0003B238
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0003BE4C File Offset: 0x0003B24C
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0003BE60 File Offset: 0x0003B260
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0003BE74 File Offset: 0x0003B274
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0003BE88 File Offset: 0x0003B288
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0003BE9C File Offset: 0x0003B29C
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0003BEB0 File Offset: 0x0003B2B0
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0003BEC4 File Offset: 0x0003B2C4
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0003BED8 File Offset: 0x0003B2D8
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0003BEEC File Offset: 0x0003B2EC
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0003BF00 File Offset: 0x0003B300
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0003BF14 File Offset: 0x0003B314
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0003BF28 File Offset: 0x0003B328
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0003BF3C File Offset: 0x0003B33C
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0003BF50 File Offset: 0x0003B350
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0003BF64 File Offset: 0x0003B364
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0003BF78 File Offset: 0x0003B378
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0003BF8C File Offset: 0x0003B38C
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0003BFA0 File Offset: 0x0003B3A0
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0003BFB4 File Offset: 0x0003B3B4
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0003BFC8 File Offset: 0x0003B3C8
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0003BFDC File Offset: 0x0003B3DC
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0003BFF0 File Offset: 0x0003B3F0
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0003C004 File Offset: 0x0003B404
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0003C018 File Offset: 0x0003B418
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0003C02C File Offset: 0x0003B42C
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0003C040 File Offset: 0x0003B440
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0003C054 File Offset: 0x0003B454
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0003C068 File Offset: 0x0003B468
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0003C07C File Offset: 0x0003B47C
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0003C090 File Offset: 0x0003B490
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0003C0A4 File Offset: 0x0003B4A4
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0003C0B8 File Offset: 0x0003B4B8
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0003C0CC File Offset: 0x0003B4CC
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0003C0E0 File Offset: 0x0003B4E0
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0003C0F4 File Offset: 0x0003B4F4
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0003C108 File Offset: 0x0003B508
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0003C11C File Offset: 0x0003B51C
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0003C130 File Offset: 0x0003B530
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0003C144 File Offset: 0x0003B544
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0003C158 File Offset: 0x0003B558
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0003C16C File Offset: 0x0003B56C
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0003C180 File Offset: 0x0003B580
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0003C194 File Offset: 0x0003B594
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0003C1A8 File Offset: 0x0003B5A8
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0003C1BC File Offset: 0x0003B5BC
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0003C1D0 File Offset: 0x0003B5D0
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0003C1E4 File Offset: 0x0003B5E4
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0003C1F8 File Offset: 0x0003B5F8
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0003C20C File Offset: 0x0003B60C
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0003C220 File Offset: 0x0003B620
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0003C234 File Offset: 0x0003B634
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0003C248 File Offset: 0x0003B648
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0003C25C File Offset: 0x0003B65C
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0003C270 File Offset: 0x0003B670
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0003C284 File Offset: 0x0003B684
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}

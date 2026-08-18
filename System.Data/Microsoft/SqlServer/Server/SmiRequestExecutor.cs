using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Transactions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000049 RID: 73
	internal abstract class SmiRequestExecutor : SmiTypedGetterSetter, ITypedSettersV3, ITypedSetters, ITypedGetters, IDisposable
	{
		// Token: 0x06000272 RID: 626 RVA: 0x001DF318 File Offset: 0x001DE718
		public virtual void Close(SmiEventSink eventSink)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x001DF338 File Offset: 0x001DE738
		internal virtual SmiEventStream Execute(SmiConnection connection, long transactionId, Transaction associatedTransaction, CommandBehavior behavior, SmiExecuteType executeType)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000274 RID: 628 RVA: 0x001DF358 File Offset: 0x001DE758
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000275 RID: 629 RVA: 0x001DF368 File Offset: 0x001DE768
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000276 RID: 630
		internal abstract void SetDefault(int ordinal);

		// Token: 0x06000277 RID: 631 RVA: 0x001DF378 File Offset: 0x001DE778
		internal virtual SmiEventStream Execute(SmiConnection connection, long transactionId, CommandBehavior behavior, SmiExecuteType executeType)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x001DF398 File Offset: 0x001DE798
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x001DF3B8 File Offset: 0x001DE7B8
		internal virtual bool IsSetAsDefault(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600027A RID: 634 RVA: 0x001DF3D8 File Offset: 0x001DE7D8
		public virtual int Count
		{
			get
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x001DF3F8 File Offset: 0x001DE7F8
		public virtual SmiParameterMetaData GetMetaData(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x001DF418 File Offset: 0x001DE818
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x001DF438 File Offset: 0x001DE838
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x001DF458 File Offset: 0x001DE858
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x001DF478 File Offset: 0x001DE878
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x001DF498 File Offset: 0x001DE898
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x001DF4B8 File Offset: 0x001DE8B8
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x001DF4D8 File Offset: 0x001DE8D8
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x001DF4F8 File Offset: 0x001DE8F8
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x001DF518 File Offset: 0x001DE918
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x001DF538 File Offset: 0x001DE938
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x001DF558 File Offset: 0x001DE958
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x001DF578 File Offset: 0x001DE978
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x001DF598 File Offset: 0x001DE998
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x001DF5B8 File Offset: 0x001DE9B8
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x001DF5D8 File Offset: 0x001DE9D8
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x001DF5F8 File Offset: 0x001DE9F8
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x001DF618 File Offset: 0x001DEA18
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x001DF638 File Offset: 0x001DEA38
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x001DF658 File Offset: 0x001DEA58
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x001DF678 File Offset: 0x001DEA78
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x001DF698 File Offset: 0x001DEA98
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x001DF6B8 File Offset: 0x001DEAB8
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x001DF6D8 File Offset: 0x001DEAD8
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x001DF6F8 File Offset: 0x001DEAF8
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x001DF718 File Offset: 0x001DEB18
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x001DF738 File Offset: 0x001DEB38
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x001DF758 File Offset: 0x001DEB58
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x001DF778 File Offset: 0x001DEB78
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x001DF798 File Offset: 0x001DEB98
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x001DF7B8 File Offset: 0x001DEBB8
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x001DF7D8 File Offset: 0x001DEBD8
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x001DF7F8 File Offset: 0x001DEBF8
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x001DF818 File Offset: 0x001DEC18
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x001DF838 File Offset: 0x001DEC38
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x001DF858 File Offset: 0x001DEC58
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x001DF878 File Offset: 0x001DEC78
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x001DF898 File Offset: 0x001DEC98
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x001DF8B8 File Offset: 0x001DECB8
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x001DF8D8 File Offset: 0x001DECD8
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x001DF8F8 File Offset: 0x001DECF8
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x001DF918 File Offset: 0x001DED18
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x001DF938 File Offset: 0x001DED38
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x001DF958 File Offset: 0x001DED58
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x001DF978 File Offset: 0x001DED78
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x001DF998 File Offset: 0x001DED98
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x001DF9B8 File Offset: 0x001DEDB8
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x001DF9D8 File Offset: 0x001DEDD8
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x001DF9F8 File Offset: 0x001DEDF8
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x001DFA18 File Offset: 0x001DEE18
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x001DFA38 File Offset: 0x001DEE38
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x001DFA58 File Offset: 0x001DEE58
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x001DFA78 File Offset: 0x001DEE78
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x001DFA98 File Offset: 0x001DEE98
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x001DFAB8 File Offset: 0x001DEEB8
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x001DFAD8 File Offset: 0x001DEED8
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x001DFAF8 File Offset: 0x001DEEF8
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x001DFB18 File Offset: 0x001DEF18
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x001DFB38 File Offset: 0x001DEF38
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x001DFB58 File Offset: 0x001DEF58
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x001DFB78 File Offset: 0x001DEF78
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x001DFB98 File Offset: 0x001DEF98
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x001DFBB8 File Offset: 0x001DEFB8
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x001DFBD8 File Offset: 0x001DEFD8
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x001DFBF8 File Offset: 0x001DEFF8
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x001DFC18 File Offset: 0x001DF018
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x001DFC38 File Offset: 0x001DF038
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x001DFC58 File Offset: 0x001DF058
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x001DFC78 File Offset: 0x001DF078
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x001DFC98 File Offset: 0x001DF098
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x001DFCB8 File Offset: 0x001DF0B8
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x001DFCD8 File Offset: 0x001DF0D8
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}

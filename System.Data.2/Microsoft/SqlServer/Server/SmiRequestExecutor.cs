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
		// Token: 0x060002A4 RID: 676 RVA: 0x0003C2AC File Offset: 0x0003B6AC
		public virtual void Close(SmiEventSink eventSink)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0003C2C0 File Offset: 0x0003B6C0
		internal virtual SmiEventStream Execute(SmiConnection connection, long transactionId, Transaction associatedTransaction, CommandBehavior behavior, SmiExecuteType executeType)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0003C2D4 File Offset: 0x0003B6D4
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0003C2E4 File Offset: 0x0003B6E4
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002A8 RID: 680
		internal abstract void SetDefault(int ordinal);

		// Token: 0x060002A9 RID: 681 RVA: 0x0003C2F4 File Offset: 0x0003B6F4
		internal virtual SmiEventStream Execute(SmiConnection connection, long transactionId, CommandBehavior behavior, SmiExecuteType executeType)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0003C308 File Offset: 0x0003B708
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0003C31C File Offset: 0x0003B71C
		internal virtual bool IsSetAsDefault(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0003C330 File Offset: 0x0003B730
		public virtual int Count
		{
			get
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0003C344 File Offset: 0x0003B744
		public virtual SmiParameterMetaData GetMetaData(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0003C358 File Offset: 0x0003B758
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0003C36C File Offset: 0x0003B76C
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0003C380 File Offset: 0x0003B780
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0003C394 File Offset: 0x0003B794
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0003C3A8 File Offset: 0x0003B7A8
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0003C3BC File Offset: 0x0003B7BC
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0003C3D0 File Offset: 0x0003B7D0
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0003C3E4 File Offset: 0x0003B7E4
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0003C3F8 File Offset: 0x0003B7F8
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0003C40C File Offset: 0x0003B80C
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0003C420 File Offset: 0x0003B820
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0003C434 File Offset: 0x0003B834
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0003C448 File Offset: 0x0003B848
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0003C45C File Offset: 0x0003B85C
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0003C470 File Offset: 0x0003B870
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0003C484 File Offset: 0x0003B884
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0003C498 File Offset: 0x0003B898
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0003C4AC File Offset: 0x0003B8AC
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0003C4C0 File Offset: 0x0003B8C0
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0003C4D4 File Offset: 0x0003B8D4
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0003C4E8 File Offset: 0x0003B8E8
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0003C4FC File Offset: 0x0003B8FC
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0003C510 File Offset: 0x0003B910
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0003C524 File Offset: 0x0003B924
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0003C538 File Offset: 0x0003B938
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0003C54C File Offset: 0x0003B94C
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0003C560 File Offset: 0x0003B960
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0003C574 File Offset: 0x0003B974
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0003C588 File Offset: 0x0003B988
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0003C59C File Offset: 0x0003B99C
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0003C5B0 File Offset: 0x0003B9B0
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0003C5C4 File Offset: 0x0003B9C4
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0003C5D8 File Offset: 0x0003B9D8
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0003C5EC File Offset: 0x0003B9EC
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0003C600 File Offset: 0x0003BA00
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0003C614 File Offset: 0x0003BA14
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0003C628 File Offset: 0x0003BA28
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0003C63C File Offset: 0x0003BA3C
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0003C650 File Offset: 0x0003BA50
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0003C664 File Offset: 0x0003BA64
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0003C678 File Offset: 0x0003BA78
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0003C68C File Offset: 0x0003BA8C
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0003C6A0 File Offset: 0x0003BAA0
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0003C6B4 File Offset: 0x0003BAB4
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0003C6C8 File Offset: 0x0003BAC8
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0003C6DC File Offset: 0x0003BADC
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0003C6F0 File Offset: 0x0003BAF0
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0003C704 File Offset: 0x0003BB04
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0003C718 File Offset: 0x0003BB18
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0003C72C File Offset: 0x0003BB2C
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0003C740 File Offset: 0x0003BB40
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0003C754 File Offset: 0x0003BB54
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0003C768 File Offset: 0x0003BB68
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0003C77C File Offset: 0x0003BB7C
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0003C790 File Offset: 0x0003BB90
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0003C7A4 File Offset: 0x0003BBA4
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0003C7B8 File Offset: 0x0003BBB8
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0003C7CC File Offset: 0x0003BBCC
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0003C7E0 File Offset: 0x0003BBE0
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0003C7F4 File Offset: 0x0003BBF4
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0003C808 File Offset: 0x0003BC08
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0003C81C File Offset: 0x0003BC1C
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0003C830 File Offset: 0x0003BC30
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0003C844 File Offset: 0x0003BC44
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0003C858 File Offset: 0x0003BC58
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0003C86C File Offset: 0x0003BC6C
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0003C880 File Offset: 0x0003BC80
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0003C894 File Offset: 0x0003BC94
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0003C8A8 File Offset: 0x0003BCA8
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0003C8BC File Offset: 0x0003BCBC
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0003C8D0 File Offset: 0x0003BCD0
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}

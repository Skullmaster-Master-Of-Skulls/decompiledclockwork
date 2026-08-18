using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000EA RID: 234
	internal abstract class ExpressionNode
	{
		// Token: 0x06000F6A RID: 3946 RVA: 0x0007C80C File Offset: 0x0007BC0C
		protected ExpressionNode(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0007C828 File Offset: 0x0007BC28
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.FormatProvider;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0007C850 File Offset: 0x0007BC50
		internal virtual bool IsSqlColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0007C860 File Offset: 0x0007BC60
		protected DataTable table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0007C874 File Offset: 0x0007BC74
		protected void BindTable(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000F6F RID: 3951
		internal abstract void Bind(DataTable table, List<DataColumn> list);

		// Token: 0x06000F70 RID: 3952
		internal abstract object Eval();

		// Token: 0x06000F71 RID: 3953
		internal abstract object Eval(DataRow row, DataRowVersion version);

		// Token: 0x06000F72 RID: 3954
		internal abstract object Eval(int[] recordNos);

		// Token: 0x06000F73 RID: 3955
		internal abstract bool IsConstant();

		// Token: 0x06000F74 RID: 3956
		internal abstract bool IsTableConstant();

		// Token: 0x06000F75 RID: 3957
		internal abstract bool HasLocalAggregate();

		// Token: 0x06000F76 RID: 3958
		internal abstract bool HasRemoteAggregate();

		// Token: 0x06000F77 RID: 3959
		internal abstract ExpressionNode Optimize();

		// Token: 0x06000F78 RID: 3960 RVA: 0x0007C888 File Offset: 0x0007BC88
		internal virtual bool DependsOn(DataColumn column)
		{
			return false;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0007C898 File Offset: 0x0007BC98
		internal static bool IsInteger(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0007C8CC File Offset: 0x0007BCCC
		internal static bool IsIntegerSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || type == StorageType.SqlByte;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0007C914 File Offset: 0x0007BD14
		internal static bool IsSigned(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || ExpressionNode.IsFloat(type);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0007C93C File Offset: 0x0007BD3C
		internal static bool IsSignedSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || ExpressionNode.IsFloatSql(type);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0007C974 File Offset: 0x0007BD74
		internal static bool IsUnsigned(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.Byte;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0007C998 File Offset: 0x0007BD98
		internal static bool IsUnsignedSql(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SqlByte || type == StorageType.Byte;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0007C9C0 File Offset: 0x0007BDC0
		internal static bool IsNumeric(StorageType type)
		{
			return ExpressionNode.IsFloat(type) || ExpressionNode.IsInteger(type);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0007C9E0 File Offset: 0x0007BDE0
		internal static bool IsNumericSql(StorageType type)
		{
			return ExpressionNode.IsFloatSql(type) || ExpressionNode.IsIntegerSql(type);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0007CA00 File Offset: 0x0007BE00
		internal static bool IsFloat(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0007CA20 File Offset: 0x0007BE20
		internal static bool IsFloatSql(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal || type == StorageType.SqlDouble || type == StorageType.SqlDecimal || type == StorageType.SqlMoney || type == StorageType.SqlSingle;
		}

		// Token: 0x0400049C RID: 1180
		private DataTable _table;
	}
}

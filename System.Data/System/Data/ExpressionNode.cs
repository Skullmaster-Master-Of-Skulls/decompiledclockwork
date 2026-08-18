using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020001A1 RID: 417
	internal abstract class ExpressionNode
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x00250B48 File Offset: 0x0024FF48
		protected ExpressionNode(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00250B68 File Offset: 0x0024FF68
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00250B98 File Offset: 0x0024FF98
		internal virtual bool IsSqlColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00250BA8 File Offset: 0x0024FFA8
		protected DataTable table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00250BC8 File Offset: 0x0024FFC8
		protected void BindTable(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x0600183E RID: 6206
		internal abstract void Bind(DataTable table, List<DataColumn> list);

		// Token: 0x0600183F RID: 6207
		internal abstract object Eval();

		// Token: 0x06001840 RID: 6208
		internal abstract object Eval(DataRow row, DataRowVersion version);

		// Token: 0x06001841 RID: 6209
		internal abstract object Eval(int[] recordNos);

		// Token: 0x06001842 RID: 6210
		internal abstract bool IsConstant();

		// Token: 0x06001843 RID: 6211
		internal abstract bool IsTableConstant();

		// Token: 0x06001844 RID: 6212
		internal abstract bool HasLocalAggregate();

		// Token: 0x06001845 RID: 6213
		internal abstract bool HasRemoteAggregate();

		// Token: 0x06001846 RID: 6214
		internal abstract ExpressionNode Optimize();

		// Token: 0x06001847 RID: 6215 RVA: 0x00250BE8 File Offset: 0x0024FFE8
		internal virtual bool DependsOn(DataColumn column)
		{
			return false;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00250BF8 File Offset: 0x0024FFF8
		internal static bool IsInteger(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00250C38 File Offset: 0x00250038
		internal static bool IsIntegerSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || type == StorageType.SqlByte;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00250C88 File Offset: 0x00250088
		internal static bool IsSigned(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || ExpressionNode.IsFloat(type);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00250CB8 File Offset: 0x002500B8
		internal static bool IsSignedSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || ExpressionNode.IsFloatSql(type);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00250CF8 File Offset: 0x002500F8
		internal static bool IsUnsigned(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.Byte;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00250D28 File Offset: 0x00250128
		internal static bool IsUnsignedSql(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SqlByte || type == StorageType.Byte;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00250D58 File Offset: 0x00250158
		internal static bool IsNumeric(StorageType type)
		{
			return ExpressionNode.IsFloat(type) || ExpressionNode.IsInteger(type);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00250D78 File Offset: 0x00250178
		internal static bool IsNumericSql(StorageType type)
		{
			return ExpressionNode.IsFloatSql(type) || ExpressionNode.IsIntegerSql(type);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00250D98 File Offset: 0x00250198
		internal static bool IsFloat(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00250DB8 File Offset: 0x002501B8
		internal static bool IsFloatSql(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal || type == StorageType.SqlDouble || type == StorageType.SqlDecimal || type == StorageType.SqlMoney || type == StorageType.SqlSingle;
		}

		// Token: 0x04000D2C RID: 3372
		private DataTable _table;
	}
}

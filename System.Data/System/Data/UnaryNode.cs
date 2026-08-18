using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020001B8 RID: 440
	internal sealed class UnaryNode : ExpressionNode
	{
		// Token: 0x0600192A RID: 6442 RVA: 0x002587D8 File Offset: 0x00257BD8
		internal UnaryNode(DataTable table, int op, ExpressionNode right) : base(table)
		{
			this.op = op;
			this.right = right;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00258808 File Offset: 0x00257C08
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this.right.Bind(table, list);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00258838 File Offset: 0x00257C38
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00258858 File Offset: 0x00257C58
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalUnaryOp(this.op, this.right.Eval(row, version));
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00258888 File Offset: 0x00257C88
		internal override object Eval(int[] recordNos)
		{
			return this.right.Eval(recordNos);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x002588A8 File Offset: 0x00257CA8
		private object EvalUnaryOp(int op, object vl)
		{
			object value = DBNull.Value;
			if (DataExpression.IsUnknown(vl))
			{
				return DBNull.Value;
			}
			switch (op)
			{
			case 0:
				return vl;
			case 1:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					StorageType storageType2 = storageType;
					switch (storageType2)
					{
					case StorageType.Byte:
						return (int)(-(int)((byte)vl));
					case StorageType.Int16:
						return (int)(-(int)((short)vl));
					case StorageType.UInt16:
					case StorageType.UInt32:
					case StorageType.UInt64:
						break;
					case StorageType.Int32:
						return -(int)vl;
					case StorageType.Int64:
						return -(long)vl;
					case StorageType.Single:
						return -(float)vl;
					case StorageType.Double:
						return -(double)vl;
					case StorageType.Decimal:
						return -(decimal)vl;
					default:
						switch (storageType2)
						{
						case StorageType.SqlDecimal:
							return -(SqlDecimal)vl;
						case StorageType.SqlDouble:
							return -(SqlDouble)vl;
						case StorageType.SqlInt16:
							return -(SqlInt16)vl;
						case StorageType.SqlInt32:
							return -(SqlInt32)vl;
						case StorageType.SqlInt64:
							return -(SqlInt64)vl;
						case StorageType.SqlMoney:
							return -(SqlMoney)vl;
						case StorageType.SqlSingle:
							return -(SqlSingle)vl;
						}
						break;
					}
					return DBNull.Value;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 2:
			{
				StorageType storageType = DataStorage.GetStorageType(vl.GetType());
				if (ExpressionNode.IsNumericSql(storageType))
				{
					return vl;
				}
				throw ExprException.TypeMismatch(this.ToString());
			}
			case 3:
				if (vl is SqlBoolean)
				{
					if (((SqlBoolean)vl).IsFalse)
					{
						return SqlBoolean.True;
					}
					if (((SqlBoolean)vl).IsTrue)
					{
						return SqlBoolean.False;
					}
					throw ExprException.UnsupportedOperator(op);
				}
				else
				{
					if (DataExpression.ToBoolean(vl))
					{
						return false;
					}
					return true;
				}
				break;
			default:
				throw ExprException.UnsupportedOperator(op);
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00258B08 File Offset: 0x00257F08
		internal override bool IsConstant()
		{
			return this.right.IsConstant();
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00258B28 File Offset: 0x00257F28
		internal override bool IsTableConstant()
		{
			return this.right.IsTableConstant();
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00258B48 File Offset: 0x00257F48
		internal override bool HasLocalAggregate()
		{
			return this.right.HasLocalAggregate();
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00258B68 File Offset: 0x00257F68
		internal override bool HasRemoteAggregate()
		{
			return this.right.HasRemoteAggregate();
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00258B88 File Offset: 0x00257F88
		internal override bool DependsOn(DataColumn column)
		{
			return this.right.DependsOn(column);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00258BA8 File Offset: 0x00257FA8
		internal override ExpressionNode Optimize()
		{
			this.right = this.right.Optimize();
			if (this.IsConstant())
			{
				object constant = this.Eval();
				return new ConstNode(base.table, ValueType.Object, constant, false);
			}
			return this;
		}

		// Token: 0x04000E33 RID: 3635
		internal readonly int op;

		// Token: 0x04000E34 RID: 3636
		internal ExpressionNode right;
	}
}

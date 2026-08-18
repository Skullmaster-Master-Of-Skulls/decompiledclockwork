using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000FB RID: 251
	internal sealed class UnaryNode : ExpressionNode
	{
		// Token: 0x06001015 RID: 4117 RVA: 0x00080788 File Offset: 0x0007FB88
		internal UnaryNode(DataTable table, int op, ExpressionNode right) : base(table)
		{
			this.op = op;
			this.right = right;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000807AC File Offset: 0x0007FBAC
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this.right.Bind(table, list);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000807D0 File Offset: 0x0007FBD0
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000807EC File Offset: 0x0007FBEC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalUnaryOp(this.op, this.right.Eval(row, version));
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00080814 File Offset: 0x0007FC14
		internal override object Eval(int[] recordNos)
		{
			return this.right.Eval(recordNos);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00080830 File Offset: 0x0007FC30
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
					switch (storageType)
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
						switch (storageType)
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

		// Token: 0x0600101B RID: 4123 RVA: 0x00080A84 File Offset: 0x0007FE84
		internal override bool IsConstant()
		{
			return this.right.IsConstant();
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00080A9C File Offset: 0x0007FE9C
		internal override bool IsTableConstant()
		{
			return this.right.IsTableConstant();
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00080AB4 File Offset: 0x0007FEB4
		internal override bool HasLocalAggregate()
		{
			return this.right.HasLocalAggregate();
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00080ACC File Offset: 0x0007FECC
		internal override bool HasRemoteAggregate()
		{
			return this.right.HasRemoteAggregate();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00080AE4 File Offset: 0x0007FEE4
		internal override bool DependsOn(DataColumn column)
		{
			return this.right.DependsOn(column);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00080B00 File Offset: 0x0007FF00
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

		// Token: 0x04000562 RID: 1378
		internal readonly int op;

		// Token: 0x04000563 RID: 1379
		internal ExpressionNode right;
	}
}

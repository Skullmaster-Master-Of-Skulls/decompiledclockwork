using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000E6 RID: 230
	internal class BinaryNode : ExpressionNode
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x00079564 File Offset: 0x00078964
		internal BinaryNode(DataTable table, int op, ExpressionNode left, ExpressionNode right) : base(table)
		{
			this.op = op;
			this.left = left;
			this.right = right;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00079590 File Offset: 0x00078990
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this.left.Bind(table, list);
			this.right.Bind(table, list);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000795C0 File Offset: 0x000789C0
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000795DC File Offset: 0x000789DC
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.EvalBinaryOp(this.op, this.left, this.right, row, version, null);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00079604 File Offset: 0x00078A04
		internal override object Eval(int[] recordNos)
		{
			return this.EvalBinaryOp(this.op, this.left, this.right, null, DataRowVersion.Default, recordNos);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00079630 File Offset: 0x00078A30
		internal override bool IsConstant()
		{
			return this.left.IsConstant() && this.right.IsConstant();
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00079658 File Offset: 0x00078A58
		internal override bool IsTableConstant()
		{
			return this.left.IsTableConstant() && this.right.IsTableConstant();
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00079680 File Offset: 0x00078A80
		internal override bool HasLocalAggregate()
		{
			return this.left.HasLocalAggregate() || this.right.HasLocalAggregate();
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000796A8 File Offset: 0x00078AA8
		internal override bool HasRemoteAggregate()
		{
			return this.left.HasRemoteAggregate() || this.right.HasRemoteAggregate();
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000796D0 File Offset: 0x00078AD0
		internal override bool DependsOn(DataColumn column)
		{
			return this.left.DependsOn(column) || this.right.DependsOn(column);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000796FC File Offset: 0x00078AFC
		internal override ExpressionNode Optimize()
		{
			this.left = this.left.Optimize();
			if (this.op == 13)
			{
				if (this.right is UnaryNode)
				{
					UnaryNode unaryNode = (UnaryNode)this.right;
					if (unaryNode.op != 3)
					{
						throw ExprException.InvalidIsSyntax();
					}
					this.op = 39;
					this.right = unaryNode.right;
				}
				if (!(this.right is ZeroOpNode))
				{
					throw ExprException.InvalidIsSyntax();
				}
				if (((ZeroOpNode)this.right).op != 32)
				{
					throw ExprException.InvalidIsSyntax();
				}
			}
			else
			{
				this.right = this.right.Optimize();
			}
			if (!this.IsConstant())
			{
				return this;
			}
			object obj = this.Eval();
			if (obj == DBNull.Value)
			{
				return new ZeroOpNode(32);
			}
			if (!(obj is bool))
			{
				return new ConstNode(base.table, ValueType.Object, obj, false);
			}
			if ((bool)obj)
			{
				return new ZeroOpNode(33);
			}
			return new ZeroOpNode(34);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000797F0 File Offset: 0x00078BF0
		internal void SetTypeMismatchError(int op, Type left, Type right)
		{
			throw ExprException.TypeMismatchInBinop(op, left, right);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00079808 File Offset: 0x00078C08
		private static object Eval(ExpressionNode expr, DataRow row, DataRowVersion version, int[] recordNos)
		{
			if (recordNos == null)
			{
				return expr.Eval(row, version);
			}
			return expr.Eval(recordNos);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00079828 File Offset: 0x00078C28
		internal int BinaryCompare(object vLeft, object vRight, StorageType resultType, int op)
		{
			return this.BinaryCompare(vLeft, vRight, resultType, op, null);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00079844 File Offset: 0x00078C44
		internal int BinaryCompare(object vLeft, object vRight, StorageType resultType, int op, CompareInfo comparer)
		{
			int result = 0;
			try
			{
				if (!DataStorage.IsSqlType(resultType))
				{
					switch (resultType)
					{
					case StorageType.Boolean:
						if (op == 7 || op == 12)
						{
							return Convert.ToInt32(DataExpression.ToBoolean(vLeft), base.FormatProvider) - Convert.ToInt32(DataExpression.ToBoolean(vRight), base.FormatProvider);
						}
						break;
					case StorageType.Char:
						return Convert.ToInt32(vLeft, base.FormatProvider).CompareTo(Convert.ToInt32(vRight, base.FormatProvider));
					case StorageType.SByte:
					case StorageType.Byte:
					case StorageType.Int16:
					case StorageType.UInt16:
					case StorageType.Int32:
						return Convert.ToInt32(vLeft, base.FormatProvider).CompareTo(Convert.ToInt32(vRight, base.FormatProvider));
					case StorageType.UInt32:
					case StorageType.Int64:
					case StorageType.UInt64:
					case StorageType.Decimal:
						return decimal.Compare(Convert.ToDecimal(vLeft, base.FormatProvider), Convert.ToDecimal(vRight, base.FormatProvider));
					case StorageType.Single:
						return Convert.ToSingle(vLeft, base.FormatProvider).CompareTo(Convert.ToSingle(vRight, base.FormatProvider));
					case StorageType.Double:
						return Convert.ToDouble(vLeft, base.FormatProvider).CompareTo(Convert.ToDouble(vRight, base.FormatProvider));
					case StorageType.DateTime:
						return DateTime.Compare(Convert.ToDateTime(vLeft, base.FormatProvider), Convert.ToDateTime(vRight, base.FormatProvider));
					case StorageType.String:
						return base.table.Compare(Convert.ToString(vLeft, base.FormatProvider), Convert.ToString(vRight, base.FormatProvider), comparer);
					case StorageType.Guid:
						return ((Guid)vLeft).CompareTo((Guid)vRight);
					case StorageType.DateTimeOffset:
						return DateTimeOffset.Compare((DateTimeOffset)vLeft, (DateTimeOffset)vRight);
					}
				}
				else
				{
					switch (resultType)
					{
					case StorageType.SByte:
					case StorageType.Byte:
					case StorageType.Int16:
					case StorageType.UInt16:
					case StorageType.Int32:
					case StorageType.SqlByte:
					case StorageType.SqlInt16:
					case StorageType.SqlInt32:
						return SqlConvert.ConvertToSqlInt32(vLeft).CompareTo(SqlConvert.ConvertToSqlInt32(vRight));
					case StorageType.UInt32:
					case StorageType.Int64:
					case StorageType.SqlInt64:
						return SqlConvert.ConvertToSqlInt64(vLeft).CompareTo(SqlConvert.ConvertToSqlInt64(vRight));
					case StorageType.UInt64:
					case StorageType.SqlDecimal:
						return SqlConvert.ConvertToSqlDecimal(vLeft).CompareTo(SqlConvert.ConvertToSqlDecimal(vRight));
					case StorageType.SqlBinary:
						return SqlConvert.ConvertToSqlBinary(vLeft).CompareTo(SqlConvert.ConvertToSqlBinary(vRight));
					case StorageType.SqlBoolean:
						if (op == 7 || op == 12)
						{
							result = 1;
							if ((vLeft.GetType() == typeof(SqlBoolean) && (vRight.GetType() == typeof(SqlBoolean) || vRight.GetType() == typeof(bool))) || (vRight.GetType() == typeof(SqlBoolean) && (vLeft.GetType() == typeof(SqlBoolean) || vLeft.GetType() == typeof(bool))))
							{
								return SqlConvert.ConvertToSqlBoolean(vLeft).CompareTo(SqlConvert.ConvertToSqlBoolean(vRight));
							}
						}
						break;
					case StorageType.SqlDateTime:
						return SqlConvert.ConvertToSqlDateTime(vLeft).CompareTo(SqlConvert.ConvertToSqlDateTime(vRight));
					case StorageType.SqlDouble:
						return SqlConvert.ConvertToSqlDouble(vLeft).CompareTo(SqlConvert.ConvertToSqlDouble(vRight));
					case StorageType.SqlGuid:
						return ((SqlGuid)vLeft).CompareTo(vRight);
					case StorageType.SqlMoney:
						return SqlConvert.ConvertToSqlMoney(vLeft).CompareTo(SqlConvert.ConvertToSqlMoney(vRight));
					case StorageType.SqlSingle:
						return SqlConvert.ConvertToSqlSingle(vLeft).CompareTo(SqlConvert.ConvertToSqlSingle(vRight));
					case StorageType.SqlString:
						return base.table.Compare(vLeft.ToString(), vRight.ToString());
					}
				}
			}
			catch (ArgumentException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
			}
			catch (FormatException e2)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
			}
			catch (InvalidCastException e3)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
			}
			catch (OverflowException e4)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e4);
			}
			catch (EvaluateException e5)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e5);
			}
			this.SetTypeMismatchError(op, vLeft.GetType(), vRight.GetType());
			return result;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00079D60 File Offset: 0x00079160
		private object EvalBinaryOp(int op, ExpressionNode left, ExpressionNode right, DataRow row, DataRowVersion version, int[] recordNos)
		{
			object obj;
			object obj2;
			StorageType storageType3;
			if (op != 27 && op != 26 && op != 5 && op != 13 && op != 39)
			{
				obj = BinaryNode.Eval(left, row, version, recordNos);
				obj2 = BinaryNode.Eval(right, row, version, recordNos);
				Type type = obj.GetType();
				Type type2 = obj2.GetType();
				StorageType storageType = DataStorage.GetStorageType(type);
				StorageType storageType2 = DataStorage.GetStorageType(type2);
				bool flag = DataStorage.IsSqlType(storageType);
				bool flag2 = DataStorage.IsSqlType(storageType2);
				if (flag && DataStorage.IsObjectSqlNull(obj))
				{
					return obj;
				}
				if (flag2 && DataStorage.IsObjectSqlNull(obj2))
				{
					return obj2;
				}
				if (obj == DBNull.Value || obj2 == DBNull.Value)
				{
					return DBNull.Value;
				}
				if (flag || flag2)
				{
					storageType3 = this.ResultSqlType(storageType, storageType2, left is ConstNode, right is ConstNode, op);
				}
				else
				{
					storageType3 = this.ResultType(storageType, storageType2, left is ConstNode, right is ConstNode, op);
				}
				if (storageType3 == StorageType.Empty)
				{
					this.SetTypeMismatchError(op, type, type2);
				}
			}
			else
			{
				obj2 = (obj = DBNull.Value);
				storageType3 = StorageType.Empty;
			}
			object obj3 = DBNull.Value;
			bool flag3 = false;
			try
			{
				switch (op)
				{
				case 5:
				{
					if (!(right is FunctionNode))
					{
						throw ExprException.InWithoutParentheses();
					}
					obj = BinaryNode.Eval(left, row, version, recordNos);
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
					{
						return DBNull.Value;
					}
					obj3 = false;
					FunctionNode functionNode = (FunctionNode)right;
					for (int i = 0; i < functionNode.argumentCount; i++)
					{
						obj2 = functionNode.arguments[i].Eval();
						if (obj2 != DBNull.Value && (!right.IsSqlColumn || !DataStorage.IsObjectSqlNull(obj2)))
						{
							storageType3 = DataStorage.GetStorageType(obj.GetType());
							if (this.BinaryCompare(obj, obj2, storageType3, 7) == 0)
							{
								obj3 = true;
								break;
							}
						}
					}
					goto IL_16EB;
				}
				case 6:
				case 14:
				case 19:
				case 21:
				case 22:
				case 23:
				case 24:
				case 25:
					break;
				case 7:
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)) || obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					return this.BinaryCompare(obj, obj2, storageType3, 7) == 0;
				case 8:
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)) || obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					return 0 < this.BinaryCompare(obj, obj2, storageType3, op);
				case 9:
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)) || obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					return 0 > this.BinaryCompare(obj, obj2, storageType3, op);
				case 10:
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)) || obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					return 0 <= this.BinaryCompare(obj, obj2, storageType3, op);
				case 11:
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)) || obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					return 0 >= this.BinaryCompare(obj, obj2, storageType3, op);
				case 12:
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)) || obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					return this.BinaryCompare(obj, obj2, storageType3, op) != 0;
				case 13:
					obj = BinaryNode.Eval(left, row, version, recordNos);
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
					{
						return true;
					}
					return false;
				case 15:
					switch (storageType3)
					{
					case StorageType.Char:
					case StorageType.String:
						obj3 = Convert.ToString(obj, base.FormatProvider) + Convert.ToString(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.SByte:
						obj3 = Convert.ToSByte((int)(Convert.ToSByte(obj, base.FormatProvider) + Convert.ToSByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Byte:
						obj3 = Convert.ToByte((int)(Convert.ToByte(obj, base.FormatProvider) + Convert.ToByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int16:
						obj3 = Convert.ToInt16((int)(Convert.ToInt16(obj, base.FormatProvider) + Convert.ToInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.UInt16:
						obj3 = Convert.ToUInt16((int)(Convert.ToUInt16(obj, base.FormatProvider) + Convert.ToUInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int32:
						obj3 = checked(Convert.ToInt32(obj, base.FormatProvider) + Convert.ToInt32(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.UInt32:
						obj3 = checked(Convert.ToUInt32(obj, base.FormatProvider) + Convert.ToUInt32(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.Int64:
						obj3 = checked(Convert.ToInt64(obj, base.FormatProvider) + Convert.ToInt64(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.UInt64:
						obj3 = checked(Convert.ToUInt64(obj, base.FormatProvider) + Convert.ToUInt64(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.Single:
						obj3 = Convert.ToSingle(obj, base.FormatProvider) + Convert.ToSingle(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Double:
						obj3 = Convert.ToDouble(obj, base.FormatProvider) + Convert.ToDouble(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Decimal:
						obj3 = Convert.ToDecimal(obj, base.FormatProvider) + Convert.ToDecimal(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.DateTime:
						if (obj is TimeSpan && obj2 is DateTime)
						{
							obj3 = (DateTime)obj2 + (TimeSpan)obj;
							goto IL_16EB;
						}
						if (obj is DateTime && obj2 is TimeSpan)
						{
							obj3 = (DateTime)obj + (TimeSpan)obj2;
							goto IL_16EB;
						}
						flag3 = true;
						goto IL_16EB;
					case StorageType.TimeSpan:
						obj3 = (TimeSpan)obj + (TimeSpan)obj2;
						goto IL_16EB;
					case StorageType.SqlByte:
						obj3 = SqlConvert.ConvertToSqlByte(obj) + SqlConvert.ConvertToSqlByte(obj2);
						goto IL_16EB;
					case StorageType.SqlDateTime:
						if (obj is TimeSpan && obj2 is SqlDateTime)
						{
							obj3 = SqlConvert.ConvertToSqlDateTime(SqlConvert.ConvertToSqlDateTime(obj2).Value + (TimeSpan)obj);
							goto IL_16EB;
						}
						if (obj is SqlDateTime && obj2 is TimeSpan)
						{
							obj3 = SqlConvert.ConvertToSqlDateTime(SqlConvert.ConvertToSqlDateTime(obj).Value + (TimeSpan)obj2);
							goto IL_16EB;
						}
						flag3 = true;
						goto IL_16EB;
					case StorageType.SqlDecimal:
						obj3 = SqlConvert.ConvertToSqlDecimal(obj) + SqlConvert.ConvertToSqlDecimal(obj2);
						goto IL_16EB;
					case StorageType.SqlDouble:
						obj3 = SqlConvert.ConvertToSqlDouble(obj) + SqlConvert.ConvertToSqlDouble(obj2);
						goto IL_16EB;
					case StorageType.SqlInt16:
						obj3 = SqlConvert.ConvertToSqlInt16(obj) + SqlConvert.ConvertToSqlInt16(obj2);
						goto IL_16EB;
					case StorageType.SqlInt32:
						obj3 = SqlConvert.ConvertToSqlInt32(obj) + SqlConvert.ConvertToSqlInt32(obj2);
						goto IL_16EB;
					case StorageType.SqlInt64:
						obj3 = SqlConvert.ConvertToSqlInt64(obj) + SqlConvert.ConvertToSqlInt64(obj2);
						goto IL_16EB;
					case StorageType.SqlMoney:
						obj3 = SqlConvert.ConvertToSqlMoney(obj) + SqlConvert.ConvertToSqlMoney(obj2);
						goto IL_16EB;
					case StorageType.SqlSingle:
						obj3 = SqlConvert.ConvertToSqlSingle(obj) + SqlConvert.ConvertToSqlSingle(obj2);
						goto IL_16EB;
					case StorageType.SqlString:
						obj3 = SqlConvert.ConvertToSqlString(obj) + SqlConvert.ConvertToSqlString(obj2);
						goto IL_16EB;
					}
					flag3 = true;
					goto IL_16EB;
				case 16:
					switch (storageType3)
					{
					case StorageType.SByte:
						obj3 = Convert.ToSByte((int)(Convert.ToSByte(obj, base.FormatProvider) - Convert.ToSByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Byte:
						obj3 = Convert.ToByte((int)(Convert.ToByte(obj, base.FormatProvider) - Convert.ToByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int16:
						obj3 = Convert.ToInt16((int)(Convert.ToInt16(obj, base.FormatProvider) - Convert.ToInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.UInt16:
						obj3 = Convert.ToUInt16((int)(Convert.ToUInt16(obj, base.FormatProvider) - Convert.ToUInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int32:
						obj3 = checked(Convert.ToInt32(obj, base.FormatProvider) - Convert.ToInt32(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.UInt32:
						obj3 = checked(Convert.ToUInt32(obj, base.FormatProvider) - Convert.ToUInt32(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.Int64:
						obj3 = checked(Convert.ToInt64(obj, base.FormatProvider) - Convert.ToInt64(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.UInt64:
						obj3 = checked(Convert.ToUInt64(obj, base.FormatProvider) - Convert.ToUInt64(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.Single:
						obj3 = Convert.ToSingle(obj, base.FormatProvider) - Convert.ToSingle(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Double:
						obj3 = Convert.ToDouble(obj, base.FormatProvider) - Convert.ToDouble(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Decimal:
						obj3 = Convert.ToDecimal(obj, base.FormatProvider) - Convert.ToDecimal(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.DateTime:
						obj3 = (DateTime)obj - (TimeSpan)obj2;
						goto IL_16EB;
					case StorageType.TimeSpan:
						if (obj is DateTime)
						{
							obj3 = (DateTime)obj - (DateTime)obj2;
							goto IL_16EB;
						}
						obj3 = (TimeSpan)obj - (TimeSpan)obj2;
						goto IL_16EB;
					case StorageType.SqlByte:
						obj3 = SqlConvert.ConvertToSqlByte(obj) - SqlConvert.ConvertToSqlByte(obj2);
						goto IL_16EB;
					case StorageType.SqlDateTime:
						if (obj is TimeSpan && obj2 is SqlDateTime)
						{
							obj3 = SqlConvert.ConvertToSqlDateTime(SqlConvert.ConvertToSqlDateTime(obj2).Value - (TimeSpan)obj);
							goto IL_16EB;
						}
						if (obj is SqlDateTime && obj2 is TimeSpan)
						{
							obj3 = SqlConvert.ConvertToSqlDateTime(SqlConvert.ConvertToSqlDateTime(obj).Value - (TimeSpan)obj2);
							goto IL_16EB;
						}
						flag3 = true;
						goto IL_16EB;
					case StorageType.SqlDecimal:
						obj3 = SqlConvert.ConvertToSqlDecimal(obj) - SqlConvert.ConvertToSqlDecimal(obj2);
						goto IL_16EB;
					case StorageType.SqlDouble:
						obj3 = SqlConvert.ConvertToSqlDouble(obj) - SqlConvert.ConvertToSqlDouble(obj2);
						goto IL_16EB;
					case StorageType.SqlInt16:
						obj3 = SqlConvert.ConvertToSqlInt16(obj) - SqlConvert.ConvertToSqlInt16(obj2);
						goto IL_16EB;
					case StorageType.SqlInt32:
						obj3 = SqlConvert.ConvertToSqlInt32(obj) - SqlConvert.ConvertToSqlInt32(obj2);
						goto IL_16EB;
					case StorageType.SqlInt64:
						obj3 = SqlConvert.ConvertToSqlInt64(obj) - SqlConvert.ConvertToSqlInt64(obj2);
						goto IL_16EB;
					case StorageType.SqlMoney:
						obj3 = SqlConvert.ConvertToSqlMoney(obj) - SqlConvert.ConvertToSqlMoney(obj2);
						goto IL_16EB;
					case StorageType.SqlSingle:
						obj3 = SqlConvert.ConvertToSqlSingle(obj) - SqlConvert.ConvertToSqlSingle(obj2);
						goto IL_16EB;
					}
					flag3 = true;
					goto IL_16EB;
				case 17:
					switch (storageType3)
					{
					case StorageType.SByte:
						obj3 = Convert.ToSByte((int)(Convert.ToSByte(obj, base.FormatProvider) * Convert.ToSByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Byte:
						obj3 = Convert.ToByte((int)(Convert.ToByte(obj, base.FormatProvider) * Convert.ToByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int16:
						obj3 = Convert.ToInt16((int)(Convert.ToInt16(obj, base.FormatProvider) * Convert.ToInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.UInt16:
						obj3 = Convert.ToUInt16((int)(Convert.ToUInt16(obj, base.FormatProvider) * Convert.ToUInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int32:
						obj3 = checked(Convert.ToInt32(obj, base.FormatProvider) * Convert.ToInt32(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.UInt32:
						obj3 = checked(Convert.ToUInt32(obj, base.FormatProvider) * Convert.ToUInt32(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.Int64:
						obj3 = checked(Convert.ToInt64(obj, base.FormatProvider) * Convert.ToInt64(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.UInt64:
						obj3 = checked(Convert.ToUInt64(obj, base.FormatProvider) * Convert.ToUInt64(obj2, base.FormatProvider));
						goto IL_16EB;
					case StorageType.Single:
						obj3 = Convert.ToSingle(obj, base.FormatProvider) * Convert.ToSingle(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Double:
						obj3 = Convert.ToDouble(obj, base.FormatProvider) * Convert.ToDouble(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Decimal:
						obj3 = Convert.ToDecimal(obj, base.FormatProvider) * Convert.ToDecimal(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.SqlByte:
						obj3 = SqlConvert.ConvertToSqlByte(obj) * SqlConvert.ConvertToSqlByte(obj2);
						goto IL_16EB;
					case StorageType.SqlDecimal:
						obj3 = SqlConvert.ConvertToSqlDecimal(obj) * SqlConvert.ConvertToSqlDecimal(obj2);
						goto IL_16EB;
					case StorageType.SqlDouble:
						obj3 = SqlConvert.ConvertToSqlDouble(obj) * SqlConvert.ConvertToSqlDouble(obj2);
						goto IL_16EB;
					case StorageType.SqlInt16:
						obj3 = SqlConvert.ConvertToSqlInt16(obj) * SqlConvert.ConvertToSqlInt16(obj2);
						goto IL_16EB;
					case StorageType.SqlInt32:
						obj3 = SqlConvert.ConvertToSqlInt32(obj) * SqlConvert.ConvertToSqlInt32(obj2);
						goto IL_16EB;
					case StorageType.SqlInt64:
						obj3 = SqlConvert.ConvertToSqlInt64(obj) * SqlConvert.ConvertToSqlInt64(obj2);
						goto IL_16EB;
					case StorageType.SqlMoney:
						obj3 = SqlConvert.ConvertToSqlMoney(obj) * SqlConvert.ConvertToSqlMoney(obj2);
						goto IL_16EB;
					case StorageType.SqlSingle:
						obj3 = SqlConvert.ConvertToSqlSingle(obj) * SqlConvert.ConvertToSqlSingle(obj2);
						goto IL_16EB;
					}
					flag3 = true;
					goto IL_16EB;
				case 18:
					switch (storageType3)
					{
					case StorageType.SByte:
						obj3 = Convert.ToSByte((int)(Convert.ToSByte(obj, base.FormatProvider) / Convert.ToSByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Byte:
						obj3 = Convert.ToByte((int)(Convert.ToByte(obj, base.FormatProvider) / Convert.ToByte(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int16:
						obj3 = Convert.ToInt16((int)(Convert.ToInt16(obj, base.FormatProvider) / Convert.ToInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.UInt16:
						obj3 = Convert.ToUInt16((int)(Convert.ToUInt16(obj, base.FormatProvider) / Convert.ToUInt16(obj2, base.FormatProvider)), base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int32:
						obj3 = Convert.ToInt32(obj, base.FormatProvider) / Convert.ToInt32(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.UInt32:
						obj3 = Convert.ToUInt32(obj, base.FormatProvider) / Convert.ToUInt32(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Int64:
						obj3 = Convert.ToInt64(obj, base.FormatProvider) / Convert.ToInt64(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.UInt64:
						obj3 = Convert.ToUInt64(obj, base.FormatProvider) / Convert.ToUInt64(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Single:
						obj3 = Convert.ToSingle(obj, base.FormatProvider) / Convert.ToSingle(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.Double:
					{
						double num = Convert.ToDouble(obj2, base.FormatProvider);
						obj3 = Convert.ToDouble(obj, base.FormatProvider) / num;
						goto IL_16EB;
					}
					case StorageType.Decimal:
						obj3 = Convert.ToDecimal(obj, base.FormatProvider) / Convert.ToDecimal(obj2, base.FormatProvider);
						goto IL_16EB;
					case StorageType.SqlByte:
						obj3 = SqlConvert.ConvertToSqlByte(obj) / SqlConvert.ConvertToSqlByte(obj2);
						goto IL_16EB;
					case StorageType.SqlDecimal:
						obj3 = SqlConvert.ConvertToSqlDecimal(obj) / SqlConvert.ConvertToSqlDecimal(obj2);
						goto IL_16EB;
					case StorageType.SqlDouble:
						obj3 = SqlConvert.ConvertToSqlDouble(obj) / SqlConvert.ConvertToSqlDouble(obj2);
						goto IL_16EB;
					case StorageType.SqlInt16:
						obj3 = SqlConvert.ConvertToSqlInt16(obj) / SqlConvert.ConvertToSqlInt16(obj2);
						goto IL_16EB;
					case StorageType.SqlInt32:
						obj3 = SqlConvert.ConvertToSqlInt32(obj) / SqlConvert.ConvertToSqlInt32(obj2);
						goto IL_16EB;
					case StorageType.SqlInt64:
						obj3 = SqlConvert.ConvertToSqlInt64(obj) / SqlConvert.ConvertToSqlInt64(obj2);
						goto IL_16EB;
					case StorageType.SqlMoney:
						obj3 = SqlConvert.ConvertToSqlMoney(obj) / SqlConvert.ConvertToSqlMoney(obj2);
						goto IL_16EB;
					case StorageType.SqlSingle:
						obj3 = SqlConvert.ConvertToSqlSingle(obj) / SqlConvert.ConvertToSqlSingle(obj2);
						goto IL_16EB;
					}
					flag3 = true;
					goto IL_16EB;
				case 20:
				{
					if (!ExpressionNode.IsIntegerSql(storageType3))
					{
						flag3 = true;
						goto IL_16EB;
					}
					if (storageType3 == StorageType.UInt64)
					{
						obj3 = Convert.ToUInt64(obj, base.FormatProvider) % Convert.ToUInt64(obj2, base.FormatProvider);
						goto IL_16EB;
					}
					if (!DataStorage.IsSqlType(storageType3))
					{
						obj3 = Convert.ToInt64(obj, base.FormatProvider) % Convert.ToInt64(obj2, base.FormatProvider);
						obj3 = Convert.ChangeType(obj3, DataStorage.GetTypeStorage(storageType3), base.FormatProvider);
						goto IL_16EB;
					}
					SqlInt64 sqlInt = SqlConvert.ConvertToSqlInt64(obj) % SqlConvert.ConvertToSqlInt64(obj2);
					if (storageType3 == StorageType.SqlInt32)
					{
						obj3 = sqlInt.ToSqlInt32();
						goto IL_16EB;
					}
					if (storageType3 == StorageType.SqlInt16)
					{
						obj3 = sqlInt.ToSqlInt16();
						goto IL_16EB;
					}
					if (storageType3 == StorageType.SqlByte)
					{
						obj3 = sqlInt.ToSqlByte();
						goto IL_16EB;
					}
					obj3 = sqlInt;
					goto IL_16EB;
				}
				case 26:
					obj = BinaryNode.Eval(left, row, version, recordNos);
					if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
					{
						return DBNull.Value;
					}
					if (!(obj is bool) && !(obj is SqlBoolean))
					{
						obj2 = BinaryNode.Eval(right, row, version, recordNos);
						flag3 = true;
						goto IL_16EB;
					}
					if (obj is bool)
					{
						if (!(bool)obj)
						{
							obj3 = false;
							goto IL_16EB;
						}
					}
					else if (((SqlBoolean)obj).IsFalse)
					{
						obj3 = false;
						goto IL_16EB;
					}
					obj2 = BinaryNode.Eval(right, row, version, recordNos);
					if (obj2 == DBNull.Value || (right.IsSqlColumn && DataStorage.IsObjectSqlNull(obj2)))
					{
						return DBNull.Value;
					}
					if (!(obj2 is bool) && !(obj2 is SqlBoolean))
					{
						flag3 = true;
						goto IL_16EB;
					}
					if (obj2 is bool)
					{
						obj3 = (bool)obj2;
						goto IL_16EB;
					}
					obj3 = ((SqlBoolean)obj2).IsTrue;
					goto IL_16EB;
				case 27:
					obj = BinaryNode.Eval(left, row, version, recordNos);
					if (obj != DBNull.Value && !DataStorage.IsObjectSqlNull(obj))
					{
						if (!(obj is bool) && !(obj is SqlBoolean))
						{
							obj2 = BinaryNode.Eval(right, row, version, recordNos);
							flag3 = true;
							goto IL_16EB;
						}
						if ((bool)obj)
						{
							obj3 = true;
							goto IL_16EB;
						}
					}
					obj2 = BinaryNode.Eval(right, row, version, recordNos);
					if (obj2 == DBNull.Value || DataStorage.IsObjectSqlNull(obj2))
					{
						return obj;
					}
					if (obj == DBNull.Value || DataStorage.IsObjectSqlNull(obj))
					{
						return obj2;
					}
					if (!(obj2 is bool) && !(obj2 is SqlBoolean))
					{
						flag3 = true;
						goto IL_16EB;
					}
					obj3 = ((obj2 is bool) ? ((bool)obj2) : ((SqlBoolean)obj2).IsTrue);
					goto IL_16EB;
				default:
					if (op == 39)
					{
						obj = BinaryNode.Eval(left, row, version, recordNos);
						if (obj == DBNull.Value || (left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
						{
							return false;
						}
						return true;
					}
					break;
				}
				throw ExprException.UnsupportedOperator(op);
				IL_16EB:;
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(DataStorage.GetTypeStorage(storageType3));
			}
			if (flag3)
			{
				this.SetTypeMismatchError(op, obj.GetType(), obj2.GetType());
			}
			return obj3;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0007B4A0 File Offset: 0x0007A8A0
		private BinaryNode.DataTypePrecedence GetPrecedence(StorageType storageType)
		{
			switch (storageType)
			{
			case StorageType.Boolean:
				return BinaryNode.DataTypePrecedence.Boolean;
			case StorageType.Char:
				return BinaryNode.DataTypePrecedence.Char;
			case StorageType.SByte:
				return BinaryNode.DataTypePrecedence.SByte;
			case StorageType.Byte:
				return BinaryNode.DataTypePrecedence.Byte;
			case StorageType.Int16:
				return BinaryNode.DataTypePrecedence.Int16;
			case StorageType.UInt16:
				return BinaryNode.DataTypePrecedence.UInt16;
			case StorageType.Int32:
				return BinaryNode.DataTypePrecedence.Int32;
			case StorageType.UInt32:
				return BinaryNode.DataTypePrecedence.UInt32;
			case StorageType.Int64:
				return BinaryNode.DataTypePrecedence.Int64;
			case StorageType.UInt64:
				return BinaryNode.DataTypePrecedence.UInt64;
			case StorageType.Single:
				return BinaryNode.DataTypePrecedence.Single;
			case StorageType.Double:
				return BinaryNode.DataTypePrecedence.Double;
			case StorageType.Decimal:
				return BinaryNode.DataTypePrecedence.Decimal;
			case StorageType.DateTime:
				return BinaryNode.DataTypePrecedence.DateTime;
			case StorageType.TimeSpan:
				return BinaryNode.DataTypePrecedence.TimeSpan;
			case StorageType.String:
				return BinaryNode.DataTypePrecedence.String;
			case StorageType.DateTimeOffset:
				return BinaryNode.DataTypePrecedence.DateTimeOffset;
			case StorageType.SqlBinary:
				return BinaryNode.DataTypePrecedence.SqlBinary;
			case StorageType.SqlBoolean:
				return BinaryNode.DataTypePrecedence.SqlBoolean;
			case StorageType.SqlByte:
				return BinaryNode.DataTypePrecedence.SqlByte;
			case StorageType.SqlBytes:
				return BinaryNode.DataTypePrecedence.SqlBytes;
			case StorageType.SqlChars:
				return BinaryNode.DataTypePrecedence.SqlChars;
			case StorageType.SqlDateTime:
				return BinaryNode.DataTypePrecedence.SqlDateTime;
			case StorageType.SqlDecimal:
				return BinaryNode.DataTypePrecedence.SqlDecimal;
			case StorageType.SqlDouble:
				return BinaryNode.DataTypePrecedence.SqlDouble;
			case StorageType.SqlGuid:
				return BinaryNode.DataTypePrecedence.SqlGuid;
			case StorageType.SqlInt16:
				return BinaryNode.DataTypePrecedence.SqlInt16;
			case StorageType.SqlInt32:
				return BinaryNode.DataTypePrecedence.SqlInt32;
			case StorageType.SqlInt64:
				return BinaryNode.DataTypePrecedence.SqlInt64;
			case StorageType.SqlMoney:
				return BinaryNode.DataTypePrecedence.SqlMoney;
			case StorageType.SqlSingle:
				return BinaryNode.DataTypePrecedence.SqlSingle;
			case StorageType.SqlString:
				return BinaryNode.DataTypePrecedence.SqlString;
			}
			return BinaryNode.DataTypePrecedence.Error;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0007B5B4 File Offset: 0x0007A9B4
		private static StorageType GetPrecedenceType(BinaryNode.DataTypePrecedence code)
		{
			switch (code)
			{
			case BinaryNode.DataTypePrecedence.SqlBinary:
				return StorageType.SqlBinary;
			default:
				return StorageType.Empty;
			case BinaryNode.DataTypePrecedence.Char:
				return StorageType.Char;
			case BinaryNode.DataTypePrecedence.String:
				return StorageType.String;
			case BinaryNode.DataTypePrecedence.SqlString:
				return StorageType.SqlString;
			case BinaryNode.DataTypePrecedence.SqlGuid:
				return StorageType.SqlGuid;
			case BinaryNode.DataTypePrecedence.Boolean:
				return StorageType.Boolean;
			case BinaryNode.DataTypePrecedence.SqlBoolean:
				return StorageType.SqlBoolean;
			case BinaryNode.DataTypePrecedence.SByte:
				return StorageType.SByte;
			case BinaryNode.DataTypePrecedence.SqlByte:
				return StorageType.SqlByte;
			case BinaryNode.DataTypePrecedence.Byte:
				return StorageType.Byte;
			case BinaryNode.DataTypePrecedence.Int16:
				return StorageType.Int16;
			case BinaryNode.DataTypePrecedence.SqlInt16:
				return StorageType.SqlInt16;
			case BinaryNode.DataTypePrecedence.UInt16:
				return StorageType.UInt16;
			case BinaryNode.DataTypePrecedence.Int32:
				return StorageType.Int32;
			case BinaryNode.DataTypePrecedence.SqlInt32:
				return StorageType.SqlInt32;
			case BinaryNode.DataTypePrecedence.UInt32:
				return StorageType.UInt32;
			case BinaryNode.DataTypePrecedence.Int64:
				return StorageType.Int64;
			case BinaryNode.DataTypePrecedence.SqlInt64:
				return StorageType.SqlInt64;
			case BinaryNode.DataTypePrecedence.UInt64:
				return StorageType.UInt64;
			case BinaryNode.DataTypePrecedence.SqlMoney:
				return StorageType.SqlMoney;
			case BinaryNode.DataTypePrecedence.Decimal:
				return StorageType.Decimal;
			case BinaryNode.DataTypePrecedence.SqlDecimal:
				return StorageType.SqlDecimal;
			case BinaryNode.DataTypePrecedence.Single:
				return StorageType.Single;
			case BinaryNode.DataTypePrecedence.SqlSingle:
				return StorageType.SqlSingle;
			case BinaryNode.DataTypePrecedence.Double:
				return StorageType.Double;
			case BinaryNode.DataTypePrecedence.SqlDouble:
				return StorageType.SqlDouble;
			case BinaryNode.DataTypePrecedence.TimeSpan:
				return StorageType.TimeSpan;
			case BinaryNode.DataTypePrecedence.DateTime:
				return StorageType.DateTime;
			case BinaryNode.DataTypePrecedence.DateTimeOffset:
				return StorageType.DateTimeOffset;
			case BinaryNode.DataTypePrecedence.SqlDateTime:
				return StorageType.SqlDateTime;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0007B6B0 File Offset: 0x0007AAB0
		private bool IsMixed(StorageType left, StorageType right)
		{
			return (ExpressionNode.IsSigned(left) && ExpressionNode.IsUnsigned(right)) || (ExpressionNode.IsUnsigned(left) && ExpressionNode.IsSigned(right));
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0007B6E0 File Offset: 0x0007AAE0
		private bool IsMixedSql(StorageType left, StorageType right)
		{
			return (ExpressionNode.IsSignedSql(left) && ExpressionNode.IsUnsignedSql(right)) || (ExpressionNode.IsUnsignedSql(left) && ExpressionNode.IsSignedSql(right));
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0007B710 File Offset: 0x0007AB10
		internal StorageType ResultType(StorageType left, StorageType right, bool lc, bool rc, int op)
		{
			if (left == StorageType.Guid && right == StorageType.Guid && Operators.IsRelational(op))
			{
				return left;
			}
			if (left == StorageType.String && right == StorageType.Guid && Operators.IsRelational(op))
			{
				return left;
			}
			if (left == StorageType.Guid && right == StorageType.String && Operators.IsRelational(op))
			{
				return right;
			}
			int precedence = (int)this.GetPrecedence(left);
			if (precedence == 0)
			{
				return StorageType.Empty;
			}
			int precedence2 = (int)this.GetPrecedence(right);
			if (precedence2 == 0)
			{
				return StorageType.Empty;
			}
			if (Operators.IsLogical(op))
			{
				if (left == StorageType.Boolean && right == StorageType.Boolean)
				{
					return StorageType.Boolean;
				}
				return StorageType.Empty;
			}
			else if (left == StorageType.DateTimeOffset || right == StorageType.DateTimeOffset)
			{
				if (Operators.IsRelational(op) && left == StorageType.DateTimeOffset && right == StorageType.DateTimeOffset)
				{
					return StorageType.DateTimeOffset;
				}
				return StorageType.Empty;
			}
			else
			{
				if (op == 15 && (left == StorageType.String || right == StorageType.String))
				{
					return StorageType.String;
				}
				BinaryNode.DataTypePrecedence dataTypePrecedence = (BinaryNode.DataTypePrecedence)Math.Max(precedence, precedence2);
				StorageType precedenceType = BinaryNode.GetPrecedenceType(dataTypePrecedence);
				if (Operators.IsArithmetical(op) && precedenceType != StorageType.String && precedenceType != StorageType.Char)
				{
					if (!ExpressionNode.IsNumeric(left))
					{
						return StorageType.Empty;
					}
					if (!ExpressionNode.IsNumeric(right))
					{
						return StorageType.Empty;
					}
				}
				if (op == 18 && ExpressionNode.IsInteger(precedenceType))
				{
					return StorageType.Double;
				}
				if (this.IsMixed(left, right))
				{
					if (lc && !rc)
					{
						return right;
					}
					if (!lc && rc)
					{
						return left;
					}
					if (ExpressionNode.IsUnsigned(precedenceType))
					{
						if (dataTypePrecedence >= BinaryNode.DataTypePrecedence.UInt64)
						{
							throw ExprException.AmbiguousBinop(op, DataStorage.GetTypeStorage(left), DataStorage.GetTypeStorage(right));
						}
						precedenceType = BinaryNode.GetPrecedenceType(dataTypePrecedence + 1);
					}
				}
				return precedenceType;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0007B854 File Offset: 0x0007AC54
		internal StorageType ResultSqlType(StorageType left, StorageType right, bool lc, bool rc, int op)
		{
			int precedence = (int)this.GetPrecedence(left);
			if (precedence == 0)
			{
				return StorageType.Empty;
			}
			int precedence2 = (int)this.GetPrecedence(right);
			if (precedence2 == 0)
			{
				return StorageType.Empty;
			}
			if (Operators.IsLogical(op))
			{
				if ((left != StorageType.Boolean && left != StorageType.SqlBoolean) || (right != StorageType.Boolean && right != StorageType.SqlBoolean))
				{
					return StorageType.Empty;
				}
				if (left == StorageType.Boolean && right == StorageType.Boolean)
				{
					return StorageType.Boolean;
				}
				return StorageType.SqlBoolean;
			}
			else
			{
				if (op == 15)
				{
					if (left == StorageType.SqlString || right == StorageType.SqlString)
					{
						return StorageType.SqlString;
					}
					if (left == StorageType.String || right == StorageType.String)
					{
						return StorageType.String;
					}
				}
				if ((left == StorageType.SqlBinary && right != StorageType.SqlBinary) || (left != StorageType.SqlBinary && right == StorageType.SqlBinary))
				{
					return StorageType.Empty;
				}
				if ((left == StorageType.SqlGuid && right != StorageType.SqlGuid) || (left != StorageType.SqlGuid && right == StorageType.SqlGuid))
				{
					return StorageType.Empty;
				}
				if (precedence > 19 && precedence2 < 20)
				{
					return StorageType.Empty;
				}
				if (precedence < 20 && precedence2 > 19)
				{
					return StorageType.Empty;
				}
				if (precedence > 19)
				{
					if (op == 15 || op == 16)
					{
						if (left == StorageType.TimeSpan)
						{
							return right;
						}
						if (right == StorageType.TimeSpan)
						{
							return left;
						}
						return StorageType.Empty;
					}
					else
					{
						if (!Operators.IsRelational(op))
						{
							return StorageType.Empty;
						}
						return left;
					}
				}
				else
				{
					BinaryNode.DataTypePrecedence dataTypePrecedence = (BinaryNode.DataTypePrecedence)Math.Max(precedence, precedence2);
					StorageType storageType = BinaryNode.GetPrecedenceType(dataTypePrecedence);
					storageType = BinaryNode.GetPrecedenceType((BinaryNode.DataTypePrecedence)this.SqlResultType((int)dataTypePrecedence));
					if (Operators.IsArithmetical(op) && storageType != StorageType.String && storageType != StorageType.Char && storageType != StorageType.SqlString)
					{
						if (!ExpressionNode.IsNumericSql(left))
						{
							return StorageType.Empty;
						}
						if (!ExpressionNode.IsNumericSql(right))
						{
							return StorageType.Empty;
						}
					}
					if (op == 18 && ExpressionNode.IsIntegerSql(storageType))
					{
						return StorageType.SqlDouble;
					}
					if (storageType == StorageType.SqlMoney && left != StorageType.SqlMoney && right != StorageType.SqlMoney)
					{
						storageType = StorageType.SqlDecimal;
					}
					if (this.IsMixedSql(left, right) && ExpressionNode.IsUnsignedSql(storageType))
					{
						if (dataTypePrecedence >= BinaryNode.DataTypePrecedence.UInt64)
						{
							throw ExprException.AmbiguousBinop(op, DataStorage.GetTypeStorage(left), DataStorage.GetTypeStorage(right));
						}
						storageType = BinaryNode.GetPrecedenceType(dataTypePrecedence + 1);
					}
					return storageType;
				}
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0007B9D8 File Offset: 0x0007ADD8
		private int SqlResultType(int typeCode)
		{
			switch (typeCode)
			{
			case -8:
				return -7;
			case -7:
			case -6:
			case -4:
			case -3:
			case -1:
			case 0:
			case 2:
			case 5:
			case 8:
			case 11:
			case 13:
			case 15:
			case 17:
			case 19:
				break;
			case -5:
				return -4;
			case -2:
				return -1;
			case 1:
				return 2;
			case 3:
			case 4:
				return 5;
			case 6:
			case 7:
				return 8;
			case 9:
			case 10:
				return 11;
			case 12:
				return 13;
			case 14:
				return 15;
			case 16:
				return 17;
			case 18:
				return 19;
			case 20:
				return 21;
			default:
				if (typeCode == 23)
				{
					return 24;
				}
				break;
			}
			return typeCode;
		}

		// Token: 0x04000489 RID: 1161
		internal int op;

		// Token: 0x0400048A RID: 1162
		internal ExpressionNode left;

		// Token: 0x0400048B RID: 1163
		internal ExpressionNode right;

		// Token: 0x02000350 RID: 848
		private enum DataTypePrecedence
		{
			// Token: 0x04001EC0 RID: 7872
			SqlDateTime = 25,
			// Token: 0x04001EC1 RID: 7873
			DateTimeOffset = 24,
			// Token: 0x04001EC2 RID: 7874
			DateTime = 23,
			// Token: 0x04001EC3 RID: 7875
			TimeSpan = 20,
			// Token: 0x04001EC4 RID: 7876
			SqlDouble = 19,
			// Token: 0x04001EC5 RID: 7877
			Double = 18,
			// Token: 0x04001EC6 RID: 7878
			SqlSingle = 17,
			// Token: 0x04001EC7 RID: 7879
			Single = 16,
			// Token: 0x04001EC8 RID: 7880
			SqlDecimal = 15,
			// Token: 0x04001EC9 RID: 7881
			Decimal = 14,
			// Token: 0x04001ECA RID: 7882
			SqlMoney = 13,
			// Token: 0x04001ECB RID: 7883
			UInt64 = 12,
			// Token: 0x04001ECC RID: 7884
			SqlInt64 = 11,
			// Token: 0x04001ECD RID: 7885
			Int64 = 10,
			// Token: 0x04001ECE RID: 7886
			UInt32 = 9,
			// Token: 0x04001ECF RID: 7887
			SqlInt32 = 8,
			// Token: 0x04001ED0 RID: 7888
			Int32 = 7,
			// Token: 0x04001ED1 RID: 7889
			UInt16 = 6,
			// Token: 0x04001ED2 RID: 7890
			SqlInt16 = 5,
			// Token: 0x04001ED3 RID: 7891
			Int16 = 4,
			// Token: 0x04001ED4 RID: 7892
			Byte = 3,
			// Token: 0x04001ED5 RID: 7893
			SqlByte = 2,
			// Token: 0x04001ED6 RID: 7894
			SByte = 1,
			// Token: 0x04001ED7 RID: 7895
			Error = 0,
			// Token: 0x04001ED8 RID: 7896
			SqlBoolean = -1,
			// Token: 0x04001ED9 RID: 7897
			Boolean = -2,
			// Token: 0x04001EDA RID: 7898
			SqlGuid = -3,
			// Token: 0x04001EDB RID: 7899
			SqlString = -4,
			// Token: 0x04001EDC RID: 7900
			String = -5,
			// Token: 0x04001EDD RID: 7901
			SqlXml = -6,
			// Token: 0x04001EDE RID: 7902
			SqlChars = -7,
			// Token: 0x04001EDF RID: 7903
			Char = -8,
			// Token: 0x04001EE0 RID: 7904
			SqlBytes = -9,
			// Token: 0x04001EE1 RID: 7905
			SqlBinary = -10
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020001B2 RID: 434
	internal sealed class FunctionNode : ExpressionNode
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x00256B78 File Offset: 0x00255F78
		internal FunctionNode(DataTable table, string name) : base(table)
		{
			this._capturedLimiter = TypeLimiter.Capture();
			this.name = name;
			for (int i = 0; i < FunctionNode.funcs.Length; i++)
			{
				if (string.Compare(FunctionNode.funcs[i].name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.info = i;
					break;
				}
			}
			if (this.info < 0)
			{
				throw ExprException.UndefinedFunction(this.name);
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00256BF8 File Offset: 0x00255FF8
		internal void AddArgument(ExpressionNode argument)
		{
			if (!FunctionNode.funcs[this.info].IsVariantArgumentList && this.argumentCount >= FunctionNode.funcs[this.info].argumentCount)
			{
				throw ExprException.FunctionArgumentCount(this.name);
			}
			if (this.arguments == null)
			{
				this.arguments = new ExpressionNode[1];
			}
			else if (this.argumentCount == this.arguments.Length)
			{
				ExpressionNode[] destinationArray = new ExpressionNode[this.argumentCount * 2];
				Array.Copy(this.arguments, 0, destinationArray, 0, this.argumentCount);
				this.arguments = destinationArray;
			}
			this.arguments[this.argumentCount++] = argument;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00256CA8 File Offset: 0x002560A8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this.Check();
			if (FunctionNode.funcs[this.info].id != FunctionId.Convert)
			{
				for (int i = 0; i < this.argumentCount; i++)
				{
					this.arguments[i].Bind(table, list);
				}
				return;
			}
			if (this.argumentCount != 2)
			{
				throw ExprException.FunctionArgumentCount(this.name);
			}
			this.arguments[0].Bind(table, list);
			if (this.arguments[1].GetType() == typeof(NameNode))
			{
				NameNode nameNode = (NameNode)this.arguments[1];
				this.arguments[1] = new ConstNode(table, ValueType.Str, nameNode.name);
			}
			this.arguments[1].Bind(table, list);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00256D68 File Offset: 0x00256168
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00256D88 File Offset: 0x00256188
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			object[] array = new object[this.argumentCount];
			if (FunctionNode.funcs[this.info].id == FunctionId.Convert)
			{
				if (this.argumentCount != 2)
				{
					throw ExprException.FunctionArgumentCount(this.name);
				}
				array[0] = this.arguments[0].Eval(row, version);
				array[1] = this.GetDataType(this.arguments[1]);
			}
			else if (FunctionNode.funcs[this.info].id != FunctionId.Iif)
			{
				for (int i = 0; i < this.argumentCount; i++)
				{
					array[i] = this.arguments[i].Eval(row, version);
					if (FunctionNode.funcs[this.info].IsValidateArguments)
					{
						if (array[i] == DBNull.Value || typeof(object) == FunctionNode.funcs[this.info].parameters[i])
						{
							return DBNull.Value;
						}
						if (array[i].GetType() != FunctionNode.funcs[this.info].parameters[i])
						{
							if (FunctionNode.funcs[this.info].parameters[i] == typeof(int) && ExpressionNode.IsInteger(DataStorage.GetStorageType(array[i].GetType())))
							{
								array[i] = Convert.ToInt32(array[i], base.FormatProvider);
							}
							else
							{
								if (FunctionNode.funcs[this.info].id != FunctionId.Trim && FunctionNode.funcs[this.info].id != FunctionId.Substring && FunctionNode.funcs[this.info].id != FunctionId.Len)
								{
									throw ExprException.ArgumentType(FunctionNode.funcs[this.info].name, i + 1, FunctionNode.funcs[this.info].parameters[i]);
								}
								if (typeof(string) != array[i].GetType() && typeof(SqlString) != array[i].GetType())
								{
									throw ExprException.ArgumentType(FunctionNode.funcs[this.info].name, i + 1, FunctionNode.funcs[this.info].parameters[i]);
								}
							}
						}
					}
				}
			}
			return this.EvalFunction(FunctionNode.funcs[this.info].id, array, row, version);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00256FC8 File Offset: 0x002563C8
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00256FE8 File Offset: 0x002563E8
		internal override bool IsConstant()
		{
			bool flag = true;
			for (int i = 0; i < this.argumentCount; i++)
			{
				flag = (flag && this.arguments[i].IsConstant());
			}
			return flag;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00257028 File Offset: 0x00256428
		internal override bool IsTableConstant()
		{
			for (int i = 0; i < this.argumentCount; i++)
			{
				if (!this.arguments[i].IsTableConstant())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00257058 File Offset: 0x00256458
		internal override bool HasLocalAggregate()
		{
			for (int i = 0; i < this.argumentCount; i++)
			{
				if (this.arguments[i].HasLocalAggregate())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00257088 File Offset: 0x00256488
		internal override bool HasRemoteAggregate()
		{
			for (int i = 0; i < this.argumentCount; i++)
			{
				if (this.arguments[i].HasRemoteAggregate())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x002570B8 File Offset: 0x002564B8
		internal override bool DependsOn(DataColumn column)
		{
			for (int i = 0; i < this.argumentCount; i++)
			{
				if (this.arguments[i].DependsOn(column))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x002570F8 File Offset: 0x002564F8
		internal override ExpressionNode Optimize()
		{
			for (int i = 0; i < this.argumentCount; i++)
			{
				this.arguments[i] = this.arguments[i].Optimize();
			}
			if (FunctionNode.funcs[this.info].id == FunctionId.In)
			{
				if (!this.IsConstant())
				{
					throw ExprException.NonConstantArgument();
				}
			}
			else if (this.IsConstant())
			{
				return new ConstNode(base.table, ValueType.Object, this.Eval(), false);
			}
			return this;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00257178 File Offset: 0x00256578
		private Type GetDataType(ExpressionNode node)
		{
			Type type = node.GetType();
			string text = null;
			if (type == typeof(NameNode))
			{
				text = ((NameNode)node).name;
			}
			if (type == typeof(ConstNode))
			{
				text = ((ConstNode)node).val.ToString();
			}
			if (text == null)
			{
				throw ExprException.ArgumentType(FunctionNode.funcs[this.info].name, 2, typeof(Type));
			}
			Type type2 = Type.GetType(text);
			if (type2 == null)
			{
				throw ExprException.InvalidType(text);
			}
			TypeLimiter.EnsureTypeIsAllowed(type2, this._capturedLimiter);
			return type2;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00257208 File Offset: 0x00256608
		private object EvalFunction(FunctionId id, object[] argumentValues, DataRow row, DataRowVersion version)
		{
			switch (id)
			{
			case FunctionId.Charindex:
				if (DataStorage.IsObjectNull(argumentValues[0]) || DataStorage.IsObjectNull(argumentValues[1]))
				{
					return DBNull.Value;
				}
				if (argumentValues[0] is SqlString)
				{
					argumentValues[0] = ((SqlString)argumentValues[0]).Value;
				}
				if (argumentValues[1] is SqlString)
				{
					argumentValues[1] = ((SqlString)argumentValues[1]).Value;
				}
				return ((string)argumentValues[1]).IndexOf((string)argumentValues[0], StringComparison.Ordinal);
			case FunctionId.Difference:
				break;
			case FunctionId.Len:
				if (argumentValues[0] is SqlString)
				{
					if (((SqlString)argumentValues[0]).IsNull)
					{
						return DBNull.Value;
					}
					argumentValues[0] = ((SqlString)argumentValues[0]).Value;
				}
				return ((string)argumentValues[0]).Length;
			default:
				switch (id)
				{
				case FunctionId.Substring:
				{
					int num = (int)argumentValues[1] - 1;
					int num2 = (int)argumentValues[2];
					if (num < 0)
					{
						throw ExprException.FunctionArgumentOutOfRange("index", "Substring");
					}
					if (num2 < 0)
					{
						throw ExprException.FunctionArgumentOutOfRange("length", "Substring");
					}
					if (num2 == 0)
					{
						return "";
					}
					if (argumentValues[0] is SqlString)
					{
						argumentValues[0] = ((SqlString)argumentValues[0]).Value;
					}
					int length = ((string)argumentValues[0]).Length;
					if (num > length)
					{
						return DBNull.Value;
					}
					if (num + num2 > length)
					{
						num2 = length - num;
					}
					return ((string)argumentValues[0]).Substring(num, num2);
				}
				case FunctionId.IsNull:
					if (DataStorage.IsObjectNull(argumentValues[0]))
					{
						return argumentValues[1];
					}
					return argumentValues[0];
				case FunctionId.Iif:
				{
					object value = this.arguments[0].Eval(row, version);
					if (DataExpression.ToBoolean(value))
					{
						return this.arguments[1].Eval(row, version);
					}
					return this.arguments[2].Eval(row, version);
				}
				case FunctionId.Convert:
				{
					if (this.argumentCount != 2)
					{
						throw ExprException.FunctionArgumentCount(this.name);
					}
					if (argumentValues[0] == DBNull.Value)
					{
						return DBNull.Value;
					}
					Type type = (Type)argumentValues[1];
					StorageType storageType = DataStorage.GetStorageType(type);
					StorageType storageType2 = DataStorage.GetStorageType(argumentValues[0].GetType());
					if (storageType == StorageType.DateTimeOffset && storageType2 == StorageType.String)
					{
						return SqlConvert.ConvertStringToDateTimeOffset((string)argumentValues[0], base.FormatProvider);
					}
					if (StorageType.Object == storageType)
					{
						return argumentValues[0];
					}
					if (storageType == StorageType.Guid && storageType2 == StorageType.String)
					{
						return new Guid((string)argumentValues[0]);
					}
					if (!ExpressionNode.IsFloatSql(storageType2) || !ExpressionNode.IsIntegerSql(storageType))
					{
						return SqlConvert.ChangeType2(argumentValues[0], storageType, type, base.FormatProvider);
					}
					if (StorageType.Single == storageType2)
					{
						return SqlConvert.ChangeType2((float)SqlConvert.ChangeType2(argumentValues[0], StorageType.Single, typeof(float), base.FormatProvider), storageType, type, base.FormatProvider);
					}
					if (StorageType.Double == storageType2)
					{
						return SqlConvert.ChangeType2((double)SqlConvert.ChangeType2(argumentValues[0], StorageType.Double, typeof(double), base.FormatProvider), storageType, type, base.FormatProvider);
					}
					if (StorageType.Decimal == storageType2)
					{
						return SqlConvert.ChangeType2((decimal)SqlConvert.ChangeType2(argumentValues[0], StorageType.Decimal, typeof(decimal), base.FormatProvider), storageType, type, base.FormatProvider);
					}
					return SqlConvert.ChangeType2(argumentValues[0], storageType, type, base.FormatProvider);
				}
				case FunctionId.cInt:
					return Convert.ToInt32(argumentValues[0], base.FormatProvider);
				case FunctionId.cBool:
				{
					StorageType storageType2 = DataStorage.GetStorageType(argumentValues[0].GetType());
					StorageType storageType3 = storageType2;
					if (storageType3 <= StorageType.Int32)
					{
						if (storageType3 == StorageType.Boolean)
						{
							return (bool)argumentValues[0];
						}
						if (storageType3 == StorageType.Int32)
						{
							return (int)argumentValues[0] != 0;
						}
					}
					else
					{
						if (storageType3 == StorageType.Double)
						{
							return (double)argumentValues[0] != 0.0;
						}
						if (storageType3 == StorageType.String)
						{
							return bool.Parse((string)argumentValues[0]);
						}
					}
					throw ExprException.DatatypeConvertion(argumentValues[0].GetType(), typeof(bool));
				}
				case FunctionId.cDate:
					return Convert.ToDateTime(argumentValues[0], base.FormatProvider);
				case FunctionId.cDbl:
					return Convert.ToDouble(argumentValues[0], base.FormatProvider);
				case FunctionId.cStr:
					return Convert.ToString(argumentValues[0], base.FormatProvider);
				case FunctionId.Abs:
				{
					StorageType storageType2 = DataStorage.GetStorageType(argumentValues[0].GetType());
					if (ExpressionNode.IsInteger(storageType2))
					{
						return Math.Abs((long)argumentValues[0]);
					}
					if (ExpressionNode.IsNumeric(storageType2))
					{
						return Math.Abs((double)argumentValues[0]);
					}
					throw ExprException.ArgumentTypeInteger(FunctionNode.funcs[this.info].name, 1);
				}
				case FunctionId.In:
					throw ExprException.NYI(FunctionNode.funcs[this.info].name);
				case FunctionId.Trim:
					if (DataStorage.IsObjectNull(argumentValues[0]))
					{
						return DBNull.Value;
					}
					if (argumentValues[0] is SqlString)
					{
						argumentValues[0] = ((SqlString)argumentValues[0]).Value;
					}
					return ((string)argumentValues[0]).Trim();
				case FunctionId.DateTimeOffset:
					if (argumentValues[0] == DBNull.Value || argumentValues[1] == DBNull.Value || argumentValues[2] == DBNull.Value)
					{
						return DBNull.Value;
					}
					switch (((DateTime)argumentValues[0]).Kind)
					{
					case DateTimeKind.Utc:
						if ((int)argumentValues[1] != 0 && (int)argumentValues[2] != 0)
						{
							throw ExprException.MismatchKindandTimeSpan();
						}
						break;
					case DateTimeKind.Local:
						if (DateTimeOffset.Now.Offset.Hours != (int)argumentValues[1] && DateTimeOffset.Now.Offset.Minutes != (int)argumentValues[2])
						{
							throw ExprException.MismatchKindandTimeSpan();
						}
						break;
					}
					if ((int)argumentValues[1] < -14 || (int)argumentValues[1] > 14)
					{
						throw ExprException.InvalidHoursArgument();
					}
					if ((int)argumentValues[2] < -59 || (int)argumentValues[2] > 59)
					{
						throw ExprException.InvalidMinutesArgument();
					}
					if ((int)argumentValues[1] == 14 && (int)argumentValues[2] > 0)
					{
						throw ExprException.InvalidTimeZoneRange();
					}
					if ((int)argumentValues[1] == -14 && (int)argumentValues[2] < 0)
					{
						throw ExprException.InvalidTimeZoneRange();
					}
					return new DateTimeOffset((DateTime)argumentValues[0], new TimeSpan((int)argumentValues[1], (int)argumentValues[2], 0));
				}
				break;
			}
			throw ExprException.UndefinedFunction(FunctionNode.funcs[this.info].name);
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x002578A8 File Offset: 0x00256CA8
		internal FunctionId Aggregate
		{
			get
			{
				if (this.IsAggregate)
				{
					return FunctionNode.funcs[this.info].id;
				}
				return FunctionId.none;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x002578D8 File Offset: 0x00256CD8
		internal bool IsAggregate
		{
			get
			{
				return FunctionNode.funcs[this.info].id == FunctionId.Sum || FunctionNode.funcs[this.info].id == FunctionId.Avg || FunctionNode.funcs[this.info].id == FunctionId.Min || FunctionNode.funcs[this.info].id == FunctionId.Max || FunctionNode.funcs[this.info].id == FunctionId.Count || FunctionNode.funcs[this.info].id == FunctionId.StDev || FunctionNode.funcs[this.info].id == FunctionId.Var;
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00257988 File Offset: 0x00256D88
		internal void Check()
		{
			Function function = FunctionNode.funcs[this.info];
			if (this.info < 0)
			{
				throw ExprException.UndefinedFunction(this.name);
			}
			if (FunctionNode.funcs[this.info].IsVariantArgumentList)
			{
				if (this.argumentCount < FunctionNode.funcs[this.info].argumentCount)
				{
					if (FunctionNode.funcs[this.info].id == FunctionId.In)
					{
						throw ExprException.InWithoutList();
					}
					throw ExprException.FunctionArgumentCount(this.name);
				}
			}
			else if (this.argumentCount != FunctionNode.funcs[this.info].argumentCount)
			{
				throw ExprException.FunctionArgumentCount(this.name);
			}
		}

		// Token: 0x04000DB1 RID: 3505
		internal const int initialCapacity = 1;

		// Token: 0x04000DB2 RID: 3506
		internal readonly string name;

		// Token: 0x04000DB3 RID: 3507
		internal readonly int info = -1;

		// Token: 0x04000DB4 RID: 3508
		internal int argumentCount;

		// Token: 0x04000DB5 RID: 3509
		internal ExpressionNode[] arguments;

		// Token: 0x04000DB6 RID: 3510
		private readonly TypeLimiter _capturedLimiter;

		// Token: 0x04000DB7 RID: 3511
		private static readonly Function[] funcs = new Function[]
		{
			new Function("Abs", FunctionId.Abs, typeof(object), true, false, 1, typeof(object), null, null),
			new Function("IIf", FunctionId.Iif, typeof(object), false, false, 3, typeof(object), typeof(object), typeof(object)),
			new Function("In", FunctionId.In, typeof(bool), false, true, 1, null, null, null),
			new Function("IsNull", FunctionId.IsNull, typeof(object), false, false, 2, typeof(object), typeof(object), null),
			new Function("Len", FunctionId.Len, typeof(int), true, false, 1, typeof(string), null, null),
			new Function("Substring", FunctionId.Substring, typeof(string), true, false, 3, typeof(string), typeof(int), typeof(int)),
			new Function("Trim", FunctionId.Trim, typeof(string), true, false, 1, typeof(string), null, null),
			new Function("Convert", FunctionId.Convert, typeof(object), false, true, 1, typeof(object), null, null),
			new Function("DateTimeOffset", FunctionId.DateTimeOffset, typeof(DateTimeOffset), false, true, 3, typeof(DateTime), typeof(int), typeof(int)),
			new Function("Max", FunctionId.Max, typeof(object), false, false, 1, null, null, null),
			new Function("Min", FunctionId.Min, typeof(object), false, false, 1, null, null, null),
			new Function("Sum", FunctionId.Sum, typeof(object), false, false, 1, null, null, null),
			new Function("Count", FunctionId.Count, typeof(object), false, false, 1, null, null, null),
			new Function("Var", FunctionId.Var, typeof(object), false, false, 1, null, null, null),
			new Function("StDev", FunctionId.StDev, typeof(object), false, false, 1, null, null, null),
			new Function("Avg", FunctionId.Avg, typeof(object), false, false, 1, null, null, null)
		};
	}
}

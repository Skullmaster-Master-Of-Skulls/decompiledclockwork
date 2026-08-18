using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000F4 RID: 244
	internal sealed class FunctionNode : ExpressionNode
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x0007EC4C File Offset: 0x0007E04C
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

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0007ECC0 File Offset: 0x0007E0C0
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
			ExpressionNode[] array = this.arguments;
			int num = this.argumentCount;
			this.argumentCount = num + 1;
			array[num] = argument;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0007ED70 File Offset: 0x0007E170
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

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0007EE38 File Offset: 0x0007E238
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0007EE54 File Offset: 0x0007E254
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

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0007F0A8 File Offset: 0x0007E4A8
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0007F0C0 File Offset: 0x0007E4C0
		internal override bool IsConstant()
		{
			bool flag = true;
			for (int i = 0; i < this.argumentCount; i++)
			{
				flag = (flag && this.arguments[i].IsConstant());
			}
			return flag;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0007F0F8 File Offset: 0x0007E4F8
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

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0007F128 File Offset: 0x0007E528
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

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0007F158 File Offset: 0x0007E558
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

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0007F188 File Offset: 0x0007E588
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

		// Token: 0x06000FEA RID: 4074 RVA: 0x0007F1BC File Offset: 0x0007E5BC
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

		// Token: 0x06000FEB RID: 4075 RVA: 0x0007F230 File Offset: 0x0007E630
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

		// Token: 0x06000FEC RID: 4076 RVA: 0x0007F2D0 File Offset: 0x0007E6D0
		private object EvalFunction(FunctionId id, object[] argumentValues, DataRow row, DataRowVersion version)
		{
			if (id != FunctionId.Charindex)
			{
				if (id != FunctionId.Len)
				{
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
						if (storageType2 <= StorageType.Int32)
						{
							if (storageType2 == StorageType.Boolean)
							{
								return (bool)argumentValues[0];
							}
							if (storageType2 == StorageType.Int32)
							{
								return (int)argumentValues[0] != 0;
							}
						}
						else
						{
							if (storageType2 == StorageType.Double)
							{
								return (double)argumentValues[0] != 0.0;
							}
							if (storageType2 == StorageType.String)
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
					throw ExprException.UndefinedFunction(FunctionNode.funcs[this.info].name);
				}
				if (argumentValues[0] is SqlString)
				{
					if (((SqlString)argumentValues[0]).IsNull)
					{
						return DBNull.Value;
					}
					argumentValues[0] = ((SqlString)argumentValues[0]).Value;
				}
				return ((string)argumentValues[0]).Length;
			}
			else
			{
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
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0007F958 File Offset: 0x0007ED58
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0007F980 File Offset: 0x0007ED80
		internal bool IsAggregate
		{
			get
			{
				return FunctionNode.funcs[this.info].id == FunctionId.Sum || FunctionNode.funcs[this.info].id == FunctionId.Avg || FunctionNode.funcs[this.info].id == FunctionId.Min || FunctionNode.funcs[this.info].id == FunctionId.Max || FunctionNode.funcs[this.info].id == FunctionId.Count || FunctionNode.funcs[this.info].id == FunctionId.StDev || FunctionNode.funcs[this.info].id == FunctionId.Var;
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0007FA28 File Offset: 0x0007EE28
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

		// Token: 0x040004E0 RID: 1248
		internal readonly string name;

		// Token: 0x040004E1 RID: 1249
		internal readonly int info = -1;

		// Token: 0x040004E2 RID: 1250
		internal int argumentCount;

		// Token: 0x040004E3 RID: 1251
		internal const int initialCapacity = 1;

		// Token: 0x040004E4 RID: 1252
		internal ExpressionNode[] arguments;

		// Token: 0x040004E5 RID: 1253
		private readonly TypeLimiter _capturedLimiter;

		// Token: 0x040004E6 RID: 1254
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

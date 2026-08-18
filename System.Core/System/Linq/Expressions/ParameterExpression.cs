using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000263 RID: 611
	[DebuggerTypeProxy(typeof(Expression.ParameterExpressionProxy))]
	[__DynamicallyInvokable]
	public class ParameterExpression : Expression
	{
		// Token: 0x06001604 RID: 5636 RVA: 0x000491CD File Offset: 0x000473CD
		internal ParameterExpression(string name)
		{
			this._name = name;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000491DC File Offset: 0x000473DC
		internal static ParameterExpression Make(Type type, string name, bool isByRef)
		{
			if (isByRef)
			{
				return new ByRefParameterExpression(type, name);
			}
			if (!type.IsEnum)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Object:
					if (type == typeof(object))
					{
						return new ParameterExpression(name);
					}
					if (type == typeof(Exception))
					{
						return new PrimitiveParameterExpression<Exception>(name);
					}
					if (type == typeof(object[]))
					{
						return new PrimitiveParameterExpression<object[]>(name);
					}
					break;
				case TypeCode.DBNull:
					return new PrimitiveParameterExpression<DBNull>(name);
				case TypeCode.Boolean:
					return new PrimitiveParameterExpression<bool>(name);
				case TypeCode.Char:
					return new PrimitiveParameterExpression<char>(name);
				case TypeCode.SByte:
					return new PrimitiveParameterExpression<sbyte>(name);
				case TypeCode.Byte:
					return new PrimitiveParameterExpression<byte>(name);
				case TypeCode.Int16:
					return new PrimitiveParameterExpression<short>(name);
				case TypeCode.UInt16:
					return new PrimitiveParameterExpression<ushort>(name);
				case TypeCode.Int32:
					return new PrimitiveParameterExpression<int>(name);
				case TypeCode.UInt32:
					return new PrimitiveParameterExpression<uint>(name);
				case TypeCode.Int64:
					return new PrimitiveParameterExpression<long>(name);
				case TypeCode.UInt64:
					return new PrimitiveParameterExpression<ulong>(name);
				case TypeCode.Single:
					return new PrimitiveParameterExpression<float>(name);
				case TypeCode.Double:
					return new PrimitiveParameterExpression<double>(name);
				case TypeCode.Decimal:
					return new PrimitiveParameterExpression<decimal>(name);
				case TypeCode.DateTime:
					return new PrimitiveParameterExpression<DateTime>(name);
				case TypeCode.String:
					return new PrimitiveParameterExpression<string>(name);
				}
			}
			return new TypedParameterExpression(type, name);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0004931D File Offset: 0x0004751D
		[__DynamicallyInvokable]
		public override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00049329 File Offset: 0x00047529
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Parameter;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0004932D File Offset: 0x0004752D
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00049335 File Offset: 0x00047535
		[__DynamicallyInvokable]
		public bool IsByRef
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetIsByRef();
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0004933D File Offset: 0x0004753D
		internal virtual bool GetIsByRef()
		{
			return false;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00049340 File Offset: 0x00047540
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x04000A4B RID: 2635
		private readonly string _name;
	}
}

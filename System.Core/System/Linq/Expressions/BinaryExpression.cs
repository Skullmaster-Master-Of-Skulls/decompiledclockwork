using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200020F RID: 527
	[DebuggerTypeProxy(typeof(Expression.BinaryExpressionProxy))]
	[__DynamicallyInvokable]
	public class BinaryExpression : Expression
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x0003C024 File Offset: 0x0003A224
		internal BinaryExpression(Expression left, Expression right)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0003C03A File Offset: 0x0003A23A
		[__DynamicallyInvokable]
		public override bool CanReduce
		{
			[__DynamicallyInvokable]
			get
			{
				return BinaryExpression.IsOpAssignment(this.NodeType);
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003C047 File Offset: 0x0003A247
		private static bool IsOpAssignment(ExpressionType op)
		{
			return op - ExpressionType.AddAssign <= 13;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0003C054 File Offset: 0x0003A254
		[__DynamicallyInvokable]
		public Expression Right
		{
			[__DynamicallyInvokable]
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0003C05C File Offset: 0x0003A25C
		[__DynamicallyInvokable]
		public Expression Left
		{
			[__DynamicallyInvokable]
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0003C064 File Offset: 0x0003A264
		[__DynamicallyInvokable]
		public MethodInfo Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethod();
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003C06C File Offset: 0x0003A26C
		internal virtual MethodInfo GetMethod()
		{
			return null;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0003C070 File Offset: 0x0003A270
		[__DynamicallyInvokable]
		public BinaryExpression Update(Expression left, LambdaExpression conversion, Expression right)
		{
			if (left == this.Left && right == this.Right && conversion == this.Conversion)
			{
				return this;
			}
			if (!this.IsReferenceComparison)
			{
				return Expression.MakeBinary(this.NodeType, left, right, this.IsLiftedToNull, this.Method, conversion);
			}
			if (this.NodeType == ExpressionType.Equal)
			{
				return Expression.ReferenceEqual(left, right);
			}
			return Expression.ReferenceNotEqual(left, right);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0003C0D8 File Offset: 0x0003A2D8
		[__DynamicallyInvokable]
		public override Expression Reduce()
		{
			if (!BinaryExpression.IsOpAssignment(this.NodeType))
			{
				return this;
			}
			ExpressionType nodeType = this._left.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType != ExpressionType.Index)
			{
				return this.ReduceVariable();
			}
			return this.ReduceIndex();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003C120 File Offset: 0x0003A320
		private static ExpressionType GetBinaryOpFromAssignmentOp(ExpressionType op)
		{
			switch (op)
			{
			case ExpressionType.AddAssign:
				return ExpressionType.Add;
			case ExpressionType.AndAssign:
				return ExpressionType.And;
			case ExpressionType.DivideAssign:
				return ExpressionType.Divide;
			case ExpressionType.ExclusiveOrAssign:
				return ExpressionType.ExclusiveOr;
			case ExpressionType.LeftShiftAssign:
				return ExpressionType.LeftShift;
			case ExpressionType.ModuloAssign:
				return ExpressionType.Modulo;
			case ExpressionType.MultiplyAssign:
				return ExpressionType.Multiply;
			case ExpressionType.OrAssign:
				return ExpressionType.Or;
			case ExpressionType.PowerAssign:
				return ExpressionType.Power;
			case ExpressionType.RightShiftAssign:
				return ExpressionType.RightShift;
			case ExpressionType.SubtractAssign:
				return ExpressionType.Subtract;
			case ExpressionType.AddAssignChecked:
				return ExpressionType.AddChecked;
			case ExpressionType.MultiplyAssignChecked:
				return ExpressionType.MultiplyChecked;
			case ExpressionType.SubtractAssignChecked:
				return ExpressionType.SubtractChecked;
			default:
				throw Error.InvalidOperation("op");
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
		private Expression ReduceVariable()
		{
			ExpressionType binaryOpFromAssignmentOp = BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType);
			Expression expression = Expression.MakeBinary(binaryOpFromAssignmentOp, this._left, this._right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression = Expression.Invoke(conversion, new Expression[]
				{
					expression
				});
			}
			return Expression.Assign(this._left, expression);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003C200 File Offset: 0x0003A400
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this._left;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Variable(memberExpression.Expression.Type, "temp1");
			Expression expression = Expression.Assign(parameterExpression, memberExpression.Expression);
			ExpressionType binaryOpFromAssignmentOp = BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType);
			Expression expression2 = Expression.MakeBinary(binaryOpFromAssignmentOp, Expression.MakeMemberAccess(parameterExpression, memberExpression.Member), this._right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression2 = Expression.Invoke(conversion, new Expression[]
				{
					expression2
				});
			}
			ParameterExpression parameterExpression2 = Expression.Variable(expression2.Type, "temp2");
			expression2 = Expression.Assign(parameterExpression2, expression2);
			Expression expression3 = Expression.Assign(Expression.MakeMemberAccess(parameterExpression, memberExpression.Member), parameterExpression2);
			Expression expression4 = parameterExpression2;
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}, new Expression[]
			{
				expression,
				expression2,
				expression3,
				expression4
			});
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0003C2FC File Offset: 0x0003A4FC
		private Expression ReduceIndex()
		{
			IndexExpression indexExpression = (IndexExpression)this._left;
			List<ParameterExpression> list = new List<ParameterExpression>(indexExpression.Arguments.Count + 2);
			List<Expression> list2 = new List<Expression>(indexExpression.Arguments.Count + 3);
			ParameterExpression parameterExpression = Expression.Variable(indexExpression.Object.Type, "tempObj");
			list.Add(parameterExpression);
			list2.Add(Expression.Assign(parameterExpression, indexExpression.Object));
			List<Expression> list3 = new List<Expression>(indexExpression.Arguments.Count);
			foreach (Expression expression in indexExpression.Arguments)
			{
				ParameterExpression parameterExpression2 = Expression.Variable(expression.Type, "tempArg" + list3.Count.ToString());
				list.Add(parameterExpression2);
				list3.Add(parameterExpression2);
				list2.Add(Expression.Assign(parameterExpression2, expression));
			}
			IndexExpression left = Expression.MakeIndex(parameterExpression, indexExpression.Indexer, list3);
			ExpressionType binaryOpFromAssignmentOp = BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType);
			Expression expression2 = Expression.MakeBinary(binaryOpFromAssignmentOp, left, this._right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression2 = Expression.Invoke(conversion, new Expression[]
				{
					expression2
				});
			}
			ParameterExpression parameterExpression3 = Expression.Variable(expression2.Type, "tempValue");
			list.Add(parameterExpression3);
			list2.Add(Expression.Assign(parameterExpression3, expression2));
			list2.Add(Expression.Assign(left, parameterExpression3));
			return Expression.Block(list, list2);
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0003C498 File Offset: 0x0003A698
		[__DynamicallyInvokable]
		public LambdaExpression Conversion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetConversion();
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0003C4A0 File Offset: 0x0003A6A0
		internal virtual LambdaExpression GetConversion()
		{
			return null;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0003C4A4 File Offset: 0x0003A6A4
		[__DynamicallyInvokable]
		public bool IsLifted
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.NodeType == ExpressionType.Coalesce || this.NodeType == ExpressionType.Assign)
				{
					return false;
				}
				if (this._left.Type.IsNullableType())
				{
					MethodInfo method = this.GetMethod();
					return method == null || !TypeUtils.AreEquivalent(method.GetParametersCached()[0].ParameterType.GetNonRefType(), this._left.Type);
				}
				return false;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0003C511 File Offset: 0x0003A711
		[__DynamicallyInvokable]
		public bool IsLiftedToNull
		{
			[__DynamicallyInvokable]
			get
			{
				return this.IsLifted && this.Type.IsNullableType();
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003C528 File Offset: 0x0003A728
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBinary(this);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003C534 File Offset: 0x0003A734
		internal static Expression Create(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion)
		{
			if (nodeType == ExpressionType.Assign)
			{
				return new AssignBinaryExpression(left, right);
			}
			if (conversion != null)
			{
				return new CoalesceConversionBinaryExpression(left, right, conversion);
			}
			if (method != null)
			{
				return new MethodBinaryExpression(nodeType, left, right, type, method);
			}
			if (type == typeof(bool))
			{
				return new LogicalBinaryExpression(nodeType, left, right);
			}
			return new SimpleBinaryExpression(nodeType, left, right, type);
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0003C598 File Offset: 0x0003A798
		internal bool IsLiftedLogical
		{
			get
			{
				Type type = this._left.Type;
				Type type2 = this._right.Type;
				MethodInfo method = this.GetMethod();
				ExpressionType nodeType = this.NodeType;
				return (nodeType == ExpressionType.AndAlso || nodeType == ExpressionType.OrElse) && TypeUtils.AreEquivalent(type2, type) && type.IsNullableType() && method != null && TypeUtils.AreEquivalent(method.ReturnType, type.GetNonNullableType());
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0003C604 File Offset: 0x0003A804
		internal bool IsReferenceComparison
		{
			get
			{
				Type type = this._left.Type;
				Type type2 = this._right.Type;
				MethodInfo method = this.GetMethod();
				ExpressionType nodeType = this.NodeType;
				return (nodeType == ExpressionType.Equal || nodeType == ExpressionType.NotEqual) && method == null && !type.IsValueType && !type2.IsValueType;
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0003C660 File Offset: 0x0003A860
		internal Expression ReduceUserdefinedLifted()
		{
			ParameterExpression parameterExpression = Expression.Parameter(this._left.Type, "left");
			ParameterExpression parameterExpression2 = Expression.Parameter(this.Right.Type, "right");
			string name = (this.NodeType == ExpressionType.AndAlso) ? "op_False" : "op_True";
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(this.Method.DeclaringType, name);
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Assign(parameterExpression, this._left),
				Expression.Condition(Expression.Property(parameterExpression, "HasValue"), Expression.Condition(Expression.Call(booleanOperator, Expression.Call(parameterExpression, "GetValueOrDefault", null, new Expression[0])), parameterExpression, Expression.Block(new ParameterExpression[]
				{
					parameterExpression2
				}, new Expression[]
				{
					Expression.Assign(parameterExpression2, this._right),
					Expression.Condition(Expression.Property(parameterExpression2, "HasValue"), Expression.Convert(Expression.Call(this.Method, Expression.Call(parameterExpression, "GetValueOrDefault", null, new Expression[0]), Expression.Call(parameterExpression2, "GetValueOrDefault", null, new Expression[0])), this.Type), Expression.Constant(null, this.Type))
				})), Expression.Constant(null, this.Type))
			});
		}

		// Token: 0x04000957 RID: 2391
		private readonly Expression _left;

		// Token: 0x04000958 RID: 2392
		private readonly Expression _right;
	}
}

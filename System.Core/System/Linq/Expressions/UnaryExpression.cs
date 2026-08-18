using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200026F RID: 623
	[DebuggerTypeProxy(typeof(Expression.UnaryExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class UnaryExpression : Expression
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x00049AAC File Offset: 0x00047CAC
		internal UnaryExpression(ExpressionType nodeType, Expression expression, Type type, MethodInfo method)
		{
			this._operand = expression;
			this._method = method;
			this._nodeType = nodeType;
			this._type = type;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00049AD1 File Offset: 0x00047CD1
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00049AD9 File Offset: 0x00047CD9
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._nodeType;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00049AE1 File Offset: 0x00047CE1
		[__DynamicallyInvokable]
		public Expression Operand
		{
			[__DynamicallyInvokable]
			get
			{
				return this._operand;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00049AE9 File Offset: 0x00047CE9
		[__DynamicallyInvokable]
		public MethodInfo Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this._method;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00049AF4 File Offset: 0x00047CF4
		[__DynamicallyInvokable]
		public bool IsLifted
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.NodeType == ExpressionType.TypeAs || this.NodeType == ExpressionType.Quote || this.NodeType == ExpressionType.Throw)
				{
					return false;
				}
				bool flag = this._operand.Type.IsNullableType();
				bool flag2 = this.Type.IsNullableType();
				if (this._method != null)
				{
					return (flag && !TypeUtils.AreEquivalent(this._method.GetParametersCached()[0].ParameterType, this._operand.Type)) || (flag2 && !TypeUtils.AreEquivalent(this._method.ReturnType, this.Type));
				}
				return flag || flag2;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00049B97 File Offset: 0x00047D97
		[__DynamicallyInvokable]
		public bool IsLiftedToNull
		{
			[__DynamicallyInvokable]
			get
			{
				return this.IsLifted && this.Type.IsNullableType();
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00049BAE File Offset: 0x00047DAE
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitUnary(this);
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00049BB8 File Offset: 0x00047DB8
		[__DynamicallyInvokable]
		public override bool CanReduce
		{
			[__DynamicallyInvokable]
			get
			{
				ExpressionType nodeType = this._nodeType;
				return nodeType - ExpressionType.PreIncrementAssign <= 3;
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00049BD8 File Offset: 0x00047DD8
		[__DynamicallyInvokable]
		public override Expression Reduce()
		{
			if (!this.CanReduce)
			{
				return this;
			}
			ExpressionType nodeType = this._operand.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType == ExpressionType.Index)
			{
				return this.ReduceIndex();
			}
			return this.ReduceVariable();
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00049C19 File Offset: 0x00047E19
		private bool IsPrefix
		{
			get
			{
				return this._nodeType == ExpressionType.PreIncrementAssign || this._nodeType == ExpressionType.PreDecrementAssign;
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00049C34 File Offset: 0x00047E34
		private UnaryExpression FunctionalOp(Expression operand)
		{
			ExpressionType nodeType;
			if (this._nodeType == ExpressionType.PreIncrementAssign || this._nodeType == ExpressionType.PostIncrementAssign)
			{
				nodeType = ExpressionType.Increment;
			}
			else
			{
				nodeType = ExpressionType.Decrement;
			}
			return new UnaryExpression(nodeType, operand, operand.Type, this._method);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00049C70 File Offset: 0x00047E70
		private Expression ReduceVariable()
		{
			if (this.IsPrefix)
			{
				return Expression.Assign(this._operand, this.FunctionalOp(this._operand));
			}
			ParameterExpression parameterExpression = Expression.Parameter(this._operand.Type, null);
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Assign(parameterExpression, this._operand),
				Expression.Assign(this._operand, this.FunctionalOp(parameterExpression)),
				parameterExpression
			});
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00049CEC File Offset: 0x00047EEC
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this._operand;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Parameter(memberExpression.Expression.Type, null);
			BinaryExpression binaryExpression = Expression.Assign(parameterExpression, memberExpression.Expression);
			memberExpression = Expression.MakeMemberAccess(parameterExpression, memberExpression.Member);
			if (this.IsPrefix)
			{
				return Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					binaryExpression,
					Expression.Assign(memberExpression, this.FunctionalOp(memberExpression))
				});
			}
			ParameterExpression parameterExpression2 = Expression.Parameter(memberExpression.Type, null);
			return Expression.Block(new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}, new Expression[]
			{
				binaryExpression,
				Expression.Assign(parameterExpression2, memberExpression),
				Expression.Assign(memberExpression, this.FunctionalOp(parameterExpression2)),
				parameterExpression2
			});
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00049DBC File Offset: 0x00047FBC
		private Expression ReduceIndex()
		{
			bool isPrefix = this.IsPrefix;
			IndexExpression indexExpression = (IndexExpression)this._operand;
			int count = indexExpression.Arguments.Count;
			Expression[] array = new Expression[count + (isPrefix ? 2 : 4)];
			ParameterExpression[] array2 = new ParameterExpression[count + (isPrefix ? 1 : 2)];
			ParameterExpression[] array3 = new ParameterExpression[count];
			int i = 0;
			array2[i] = Expression.Parameter(indexExpression.Object.Type, null);
			array[i] = Expression.Assign(array2[i], indexExpression.Object);
			for (i++; i <= count; i++)
			{
				Expression expression = indexExpression.Arguments[i - 1];
				array3[i - 1] = (array2[i] = Expression.Parameter(expression.Type, null));
				array[i] = Expression.Assign(array2[i], expression);
			}
			Expression instance = array2[0];
			PropertyInfo indexer = indexExpression.Indexer;
			Expression[] list = array3;
			indexExpression = Expression.MakeIndex(instance, indexer, new TrueReadOnlyCollection<Expression>(list));
			if (!isPrefix)
			{
				ParameterExpression parameterExpression = array2[i] = Expression.Parameter(indexExpression.Type, null);
				array[i] = Expression.Assign(array2[i], indexExpression);
				i++;
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(parameterExpression));
				array[i++] = parameterExpression;
			}
			else
			{
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(indexExpression));
			}
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(array2), new TrueReadOnlyCollection<Expression>(array));
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00049F25 File Offset: 0x00048125
		[__DynamicallyInvokable]
		public UnaryExpression Update(Expression operand)
		{
			if (operand == this.Operand)
			{
				return this;
			}
			return Expression.MakeUnary(this.NodeType, operand, this.Type, this.Method);
		}

		// Token: 0x04000A63 RID: 2659
		private readonly Expression _operand;

		// Token: 0x04000A64 RID: 2660
		private readonly MethodInfo _method;

		// Token: 0x04000A65 RID: 2661
		private readonly ExpressionType _nodeType;

		// Token: 0x04000A66 RID: 2662
		private readonly Type _type;
	}
}
